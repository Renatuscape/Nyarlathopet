using System;
using System.Collections.Generic;
using System.Linq;

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
            (Text.Get("PET-C3"), () => CommuneInPrivate(tome))
        };

        if (GameplayManager.dummyData.cultMembers.Count > 0)
        {
            choices.Add(("PET-C4", () => CommuneCultist(tome)));
        }

        if (GameplayManager.dummyData.cultMembers.Count > 1)
        {
            choices.Add(("PET-C5", () => CommuneAll(tome)));
        }

        AlertSystem.Choice(Text.Get("PET-C2"), choices, false);
    }

    static void CommuneInPrivate(Item tome)
    {
        Report.Write("PetCommuneHelper", "Communing in private.");

        // Choose stat to exchange
    }

    static void CommuneCultist(Item tome)
    {
        Report.Write("PetCommuneHelper", "Prompting cultist election for communing.");

        // Choose cultist
            // Choose stat to exchange
    }

    static void CommuneAll(Item tome)
    {
        Report.Write("PetCommuneHelper", "Communing with all.");

        // Increase SAN and RGE
    }
}