using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
public static class PetWorshipHelper
{
    public static void Initialise()
    {
        // Choose a prayer
        var prayers = GameplayManager.dummyData.inventory.Where(i => i.type == ItemType.Prayer).OrderByDescending(i => i.occultism + i.strength + i.lore).ToList();

        if (prayers.Count < 1)
        {
            AlertSystem.Print(Text.Get("PET-W0"));
            return;
        }

        List<(string, Action)> choices = new();

        for (int i = 0; i < prayers.Count && i < AlertSystem.MaxOptions; i++)
        {
            var capturedIndex = i;
            choices.Add((prayers[capturedIndex].name, () => ChooseFormOfWorship(prayers[capturedIndex])));
        }

        AlertSystem.Choice(Text.Get("PET-W1"), choices, false);
    }

    static void ChooseFormOfWorship(Item prayer)
    {
        // Options - How would you like to worship?
        // A public sermon - increase network and notoriety by a lot, may attract new members
        // A members-only sermon - calms rage, increases network and notoriety slightly
        // Worship in private - calms rage a great deal, increases sanity

        List<(string, Action)> choices = new()
        {
            (Text.Get("PET-W5"), () => PerformPrivateWorship(prayer))
        };

        if (GameplayManager.dummyData.cultMembers.Count > 0)
        {
            choices.Add((Text.Get("PET-W4"), () => PerformClosedSermon(prayer)));
        }

        choices.Add((Text.Get("PET-W3"), () => PerformPublicSermon(prayer)));
        AlertSystem.Choice(Text.Get("PET-W2"), choices, false);
    }

    static void PerformPublicSermon(Item prayer)
    {
        int networkIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.occultism) * 2);
        int notorietyIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.strength) * 2);

        int networkChange = DummyDataUpdater.UpdateNetwork(networkIncrease);
        int notorietyChange = DummyDataUpdater.UpdateNotoriety(notorietyIncrease);
        Human newMember = null;

        GameplayManager.dummyData.inventory.Remove(prayer);

        if (Random.Range(0, 100) < prayer.lore + prayer.strength + prayer.occultism)
        {
            newMember = CultistFactory.GetCultist(Random.Range(Math.Max(1, GameplayManager.dummyData.level / 2), GameplayManager.dummyData.level));
        }

        AlertSystem.Print($"{Text.Get("PET-W3B")}\n\n{WriteChangeReport(0, 0, networkChange, notorietyChange, newMember)}");
    }

    static void PerformClosedSermon(Item prayer)
    {
        int networkIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.occultism));
        int notorietyIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.strength));
        int rageReduction = Random.Range(Math.Max(1, prayer.lore / 2), Math.Max(2, prayer.lore));

        int networkChange = DummyDataUpdater.UpdateNetwork(networkIncrease);
        int notorietyChange = DummyDataUpdater.UpdateNotoriety(notorietyIncrease);
        int rageChange = DummyDataUpdater.UpdateRage(rageReduction, true);

        GameplayManager.dummyData.inventory.Remove(prayer);

        AlertSystem.Print($"{Text.Get("PET-W4B")} {GameplayManager.dummyData.currentPet.name}\n\n{WriteChangeReport(rageChange, 0, networkChange, notorietyChange, null)}");
    }

    static void PerformPrivateWorship(Item prayer)
    {
        int rageReduction = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.occultism) * 2);

        int sanIncrease = Random.Range(Math.Max(2, prayer.lore / 2), Math.Max(2, prayer.lore + prayer.occultism) * 2);
        
        int rageChange = DummyDataUpdater.UpdateRage(rageReduction, true);
        int sanChange = DummyDataUpdater.UpdateLeaderSanity(sanIncrease);

        GameplayManager.dummyData.inventory.Remove(prayer);

        AlertSystem.Print($"{Text.Get("PET-W5B")}\n\n{WriteChangeReport(rageChange, sanChange, 0, 0, null)}");
    }

    static string WriteChangeReport(int rage, int sanity, int network, int notoriety, Human newMember)
    {
        string report = "";

        if (rage != 0) {
            report += $" {Tags.Get("RGE")}" + rage;
        }

        if (sanity != 0)
        {
            report += $" {Tags.Get("SAN")}+" + sanity;
        }

        if (network != 0)
        {
            report += $" {Tags.Get("NWK")}+" + network;
        }

        if (notoriety != 0)
        {
            report += $" {Tags.Get("NTR")}+" + notoriety;
        }
        if (newMember != null)
        {
            report += "\n" + newMember.name + " " + Text.Get("EXP-R0");
        }

        report = report.Trim(' ');

        return report;
    }
}
