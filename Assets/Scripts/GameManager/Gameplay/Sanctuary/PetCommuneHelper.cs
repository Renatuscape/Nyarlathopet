using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class PetCommuneHelper
{
    // Options - how would you like to commune with your pet?
    // Commune in private - Increase own stats or level, reducing specified pet stat (increases rage), requires tome
    // Volunteer a cultist - Reduce a specified pet stat, increasing cultist stat (increases rage), requires tome
    // Everyone will commune - spend a tome to increase everyone's sanity a little, increasing rage, requires tome

    public static void Initialise()
    {
        // Choose a prayer
        var tomes = GameplayManager.dummyData.inventory.Where(i => i.type == ItemType.Tome).OrderByDescending(i => i.occultism + i.strength + i.lore).ToList();

        if (tomes.Count < 1)
        {
            AlertSystem.Print(Text.Get("PET-C0"));
            return;
        }

        List<(string, Action)> choices = new();

        for (int i = 0; i < tomes.Count && i < AlertSystem.MaxOptions; i++)
        {
            var capturedIndex = i;
            choices.Add((tomes[capturedIndex].Print(), () => ChooseFormOfCommuning(tomes[capturedIndex])));
        }

        AlertSystem.Choice(Text.Get("PET-C1"), choices, false);
    }

    static void ChooseFormOfCommuning(Item tome)
    {
        List<(string, Action)> choices = new()
        {
            (Text.Get("PET-C5"), () => CommuneInPrivate(tome))
        };

        if (GameplayManager.dummyData.cultMembers.Count > 0)
        {
            choices.Add((Text.Get("PET-C6"), () => CommuneCultist(tome)));
        }

        if (GameplayManager.dummyData.cultMembers.Count > 1)
        {
            choices.Add((Text.Get("PET-C7"), () => CommuneAll(tome)));
        }

        AlertSystem.Choice(Text.Get("PET-C2") + " " + GameplayManager.dummyData.currentPet.name + "?", choices, false);
    }

    static void CommuneAll(Item tome)
    {
        Report.Write("PetCommuneHelper", "Communing with all.");

        // Increase SAN and RGE
        int levelAverage = Math.Max(1, (GameplayManager.dummyData.currentPet.GetLevel() + tome.level) / 2);
        int sanIncrease = Math.Max(1, Random.Range(levelAverage / 3, levelAverage));
        int rageIncrease = Random.Range(1, Math.Max(2, sanIncrease * 2));

        CreatureStats creatureChanges = new() { rage = rageIncrease };
        MundaneStats cultistChanges = new() { sanity = sanIncrease };

        GameplayManager.dummyData.currentPet.ApplyStatChanges(creatureChanges);
        GameplayManager.dummyData.cultLeader.ApplyStatChanges(cultistChanges);

        foreach (var cultist in GameplayManager.dummyData.cultMembers)
        {
            cultist.ApplyStatChanges(cultistChanges);
        }

        AlertSystem.Print(GetReport(Tags.Get("ALL-C"), creatureChanges, cultistChanges));
        RemoveTome(tome);
    }

    static void CommuneInPrivate(Item tome)
    {
        Report.Write("PetCommuneHelper", "Communing in private.");
        ChooseStatToExchange(tome, GameplayManager.dummyData.cultLeader);
    }

    static void CommuneCultist(Item tome)
    {
        Report.Write("PetCommuneHelper", "Prompting cultist election for communing.");

        // Choose cultist
        // Choose stat to exchange
        AlertSystem.Print(Text.Get("ERR-NI"));
    }

    static void ChooseStatToExchange(Item tome, Human cultist)
    {
        var pet = GameplayManager.dummyData.currentPet;
        List<(string, Action)> options = new();
        int points = Math.Max(1, (tome.occultism + tome.strength + tome.lore) / 3);

        // Choose stat
        if (pet.mythos > 0) {
            options.Add((Tags.Get("Mth"), () => ExecuteSingleCommune(tome, cultist, new() { mythos = Math.Clamp(points, 1, pet.mythos) })));
        }
        if (pet.intrigue > 0)
        {
            options.Add((Tags.Get("Int"), () => ExecuteSingleCommune(tome, cultist, new() { intrigue = Math.Clamp(points, 1, pet.intrigue) })));
        }
        if (pet.magick > 0)
        {
            options.Add((Tags.Get("Mgk"), () => ExecuteSingleCommune(tome, cultist, new() { magick = Math.Clamp(points, 1, pet.magick) })));
        }
        if (pet.abstraction > 0)
        {
            options.Add((Tags.Get("Abs"), () => ExecuteSingleCommune(tome, cultist, new() { abstraction = Math.Clamp(points, 1, pet.abstraction) })));
        }
        if (pet.strength > 0)
        {
            options.Add((Tags.Get("Str"), () => ExecuteSingleCommune(tome, cultist, new() { strength = Math.Clamp(points, 1, pet.strength) })));
        }

        // Rage is not an option because it cannot be reduced by communing

        if (options.Count < 1)
        {
            AlertSystem.Print(Text.Get("PET-C4"));
        }
        else
        {
            AlertSystem.Choice(Text.Get("PET-C3"), options, false);
        }
    }

    static void ExecuteSingleCommune(Item tome, Human cultist, CreatureStats creatureChanges)
    {
        MundaneStats cultistChanges = new();

        cultistChanges.occultism = creatureChanges.magick + creatureChanges.abstraction;
        cultistChanges.lore = creatureChanges.intrigue + creatureChanges.mythos;
        cultistChanges.strength = creatureChanges.strength;

        creatureChanges.rage = Random.Range(1, GameplayManager.dummyData.level * 2);
        creatureChanges.magick = -creatureChanges.magick;
        creatureChanges.abstraction = -creatureChanges.abstraction;
        creatureChanges.intrigue = -creatureChanges.intrigue;
        creatureChanges.mythos = -creatureChanges.mythos;
        creatureChanges.strength = -creatureChanges.strength;

        cultist.ApplyStatChanges(cultistChanges);
        GameplayManager.dummyData.currentPet.ApplyStatChanges(creatureChanges);

        RemoveTome(tome);
        AlertSystem.Print(GetReport(cultist.name, creatureChanges, cultistChanges));
    }

    static string GetReport(string communerName, CreatureStats creatureChanges, MundaneStats cultistChanges)
    {
        string report = $"{GameplayManager.dummyData.currentPet.name} {StatConverter.CreateCreatureStatChangeReport(creatureChanges, false)}\n\n";

        report += $"{communerName} {StatConverter.CreateMundaneStatChangeReport(cultistChanges, false)}";
        return report;

    }

    static void RemoveTome(Item tome)
    {
        GameplayManager.dummyData.inventory.Remove(tome);
    }
}