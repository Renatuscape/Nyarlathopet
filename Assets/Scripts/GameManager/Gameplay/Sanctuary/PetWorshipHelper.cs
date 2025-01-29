using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
public static class PetWorshipHelper
{
    public static void Initialise()
    {
        // Choose a prayer
        var prayers = GameplayManager.dummyData.inventory.Where(i => i.type == ItemType.Prayer).OrderByDescending(i => i.magick + i.strength + i.lore).ToList();

        if (prayers.Count < 1)
        {
            AlertSystem.Print(Repository.GetText("PET-W0"));
            return;
        }

        List<(string, Action)> choices = new();

        for (int i = 0; i < prayers.Count && i < AlertSystem.MaxOptions; i++)
        {
            var capturedIndex = i;
            choices.Add((prayers[capturedIndex].name, () => ChooseFormOfWorship(prayers[capturedIndex])));
        }

        AlertSystem.Choice(Repository.GetText("PET-W1"), choices, false);
    }

    static void ChooseFormOfWorship(Item prayer)
    {
        // Options - How would you like to worship?
        // A public sermon - increase network and notoriety by a lot, may attract new members
        // A members-only sermon - calms rage, increases network and notoriety slightly
        // Worship in private - calms rage a great deal, increases sanity

        List<(string, Action)> choices = new()
        {
            (Repository.GetText("PET-W5"), () => PerformPrivateWorship(prayer))
        };

        if (GameplayManager.dummyData.cultMembers.Count > 0)
        {
            choices.Add((Repository.GetText("PET-W4"), () => PerformClosedSermon(prayer)));
        }

        choices.Add((Repository.GetText("PET-W3"), () => PerformPublicSermon(prayer)));
        AlertSystem.Choice(Repository.GetText("PET-W2"), choices, false);
    }

    static void PerformPublicSermon(Item prayer)
    {
        int networkIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.magick) * 2);
        int notorietyIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.strength) * 2);

        int networkChange = DummyDataUpdater.UpdateNetwork(networkIncrease);
        int notorietyChange = DummyDataUpdater.UpdateNotoriety(notorietyIncrease);
        Human newMember = null;

        GameplayManager.dummyData.inventory.Remove(prayer);

        if (Random.Range(0, 100) < prayer.lore + prayer.strength + prayer.magick)
        {
            newMember = CultistFactory.GetCultist(Random.Range(Math.Max(1, GameplayManager.dummyData.level / 2), GameplayManager.dummyData.level));
        }

        AlertSystem.Print($"{Repository.GetText("PET-W3B")}\n\n{WriteChangeReport(0, 0, networkChange, notorietyChange, newMember)}");
    }

    static void PerformClosedSermon(Item prayer)
    {
        int networkIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.magick));
        int notorietyIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.strength));
        int rageReduction = Random.Range(Math.Max(1, prayer.lore / 2), Math.Max(2, prayer.lore));

        int networkChange = DummyDataUpdater.UpdateNetwork(networkIncrease);
        int notorietyChange = DummyDataUpdater.UpdateNotoriety(notorietyIncrease);
        int rageChange = DummyDataUpdater.UpdateRage(rageReduction, true);

        GameplayManager.dummyData.inventory.Remove(prayer);

        AlertSystem.Print($"{Repository.GetText("PET-W4B")} {GameplayManager.dummyData.currentPet.name}\n\n{WriteChangeReport(rageChange, 0, networkChange, notorietyChange, null)}");
    }

    static void PerformPrivateWorship(Item prayer)
    {
        int rageReduction = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.magick) * 2);

        int sanIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.magick) * 2);
        
        int rageChange = DummyDataUpdater.UpdateRage(rageReduction, true);
        int sanChange = DummyDataUpdater.UpdateSanity(sanIncrease);

        GameplayManager.dummyData.inventory.Remove(prayer);

        AlertSystem.Print($"{Repository.GetText("PET-W5B")}\n\n{WriteChangeReport(rageChange, sanChange, 0, 0, null)}");
    }

    static string WriteChangeReport(int rage, int sanity, int network, int notoriety, Human newMember)
    {
        string report = "";

        if (rage != 0) {
            report += " RGE" + rage;
        }

        if (sanity != 0)
        {
            report += " SAN+" + sanity;
        }

        if (network != 0)
        {
            report += " NTWK+" + network;
        }

        if (notoriety != 0)
        {
            report += " NOTR+" + notoriety;
        }
        if (newMember != null)
        {
            report += "\n" + newMember.name + " " + Repository.GetText("EXP-R0");
        }

        report = report.Trim(' ');

        return report;
    }
}