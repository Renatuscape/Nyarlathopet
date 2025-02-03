using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class RitualSacrificeHelper
{
    public static void OfferArtefact(CreatureStats horrorState, Item item, out string report)
    {
        report = $"{Text.Get("SACR0")} " + item.name + ".\n";
        GameplayManager.dummyData.inventory.Remove(item);
        int magickIncrease = 0;
        int abstractionIncrease = 0;
        int strengthIncrease = 0;
        int intrigueIncrease = 0;
        int rageIncrease = 0; 

        int itemLevel = Math.Max(1, Mathf.FloorToInt((item.lore + item.strength + item.occultism) * 0.5f));

        // Find the highest stat value
        int maxStat = Math.Max(item.lore, Math.Max(item.strength, item.occultism));

        // Helper method to count how many stats equal the max
        int CountMaxStats() => (item.lore == maxStat ? 1 : 0)
                            + (item.strength == maxStat ? 1 : 0)
                            + (item.occultism == maxStat ? 1 : 0);

        for (int i = 0; i < itemLevel; i++)
        {
            int maxStatCount = CountMaxStats();

            if (maxStatCount == 3) // All stats are equal
            {
                intrigueIncrease++;
                strengthIncrease++;
                magickIncrease++;
                continue;
            }

            if (maxStatCount == 2) // Two highest stats are equal
            {
                if (item.lore == item.strength && maxStat == item.lore)
                {
                    intrigueIncrease++;
                    strengthIncrease++;
                }
                else if (item.lore == item.occultism && maxStat == item.lore)
                {
                    intrigueIncrease++;
                    magickIncrease++;
                }
                else if (item.occultism == item.strength && maxStat == item.occultism)
                {
                    magickIncrease++;
                    strengthIncrease++;
                }
                continue;
            }

            // Single highest stat
            if (item.lore == maxStat)
            {
                intrigueIncrease++;
                rageIncrease--;
            }
            else if (item.occultism == maxStat)
            {
                magickIncrease++;
                abstractionIncrease++;
            }
            else if (item.strength == maxStat)
            {
                strengthIncrease++;
                rageIncrease++;
            }
        }

        horrorState.magick += magickIncrease;
        horrorState.strength += strengthIncrease;
        horrorState.rage += rageIncrease;
        horrorState.intrigue += intrigueIncrease;
        horrorState.abstraction += abstractionIncrease;

        report += $"MGC+{magickIncrease} ABS+{abstractionIncrease} ITR+{intrigueIncrease} STR+{strengthIncrease} RGE{(rageIncrease >= 0 ? "+" : "-")}{rageIncrease}";
    }

    public static void SacrificeCultist(CreatureStats ritualState, Human cultist, out string report)
    {
        int reductionChance = 25;
        GameplayManager.dummyData.cultMembers.Remove(cultist);
        GameplayManager.dummyData.funds += cultist.funds;

        int skillPoints = cultist.occultism + cultist.strength + cultist.lore;
        int cultistLevel = Mathf.FloorToInt(skillPoints * 0.5f);

        // STAT INCREASES
        string statReport = "Funds+" + cultist.funds + " ";

        if (cultist.sanity < cultistLevel)
        {
            int value = Random.Range(1, cultistLevel);
            ritualState.abstraction += value;
            statReport += "ABS+" + value + " ";
        }
        else
        {
            int value = Random.Range(1, cultistLevel);
            ritualState.rage += value;
            statReport += "RGE+" + value + " ";
        }

        if (ritualState.strength < cultist.occultism || cultist.occultism > 0)
        {
            if (Random.Range(0, 99) > reductionChance)
            {
                ritualState.magick += cultist.occultism;
                statReport += "MGC+" + cultist.occultism + " ";
            }
            else
            {
                ritualState.strength -= cultist.occultism;
                statReport += "STR-" + cultist.occultism + " ";
            }
        }

        if (ritualState.intrigue < cultist.strength || cultist.strength > 0)
        {
            if (Random.Range(0, 99) > reductionChance)
            {
                ritualState.strength += cultist.strength;
                statReport += "STR+" + cultist.strength + " ";
            }
            else
            {
                ritualState.intrigue -= cultist.strength;
                statReport += "ITR-" + cultist.strength + " ";
            }
        }

        if (cultist.lore > 0)
        {
            if (ritualState.abstraction < cultist.lore || Random.Range(0, 99) > reductionChance)
            {
                ritualState.intrigue += cultist.lore;
                statReport += "ITR+" + cultist.lore;
            }
            else
            {
                ritualState.abstraction -= cultist.lore;
                statReport += "ABS-" + cultist.lore;
            }
        }

        statReport = statReport.TrimEnd(' ');

        // SANITY LOSS
        int sanityLossLeader = Random.Range(0, cultistLevel);
        DummyDataUpdater.UpdateLeaderSanity(-sanityLossLeader);

        int totalMemberSanityLoss = 0;

        foreach (var member in GameplayManager.dummyData.cultMembers)
        {
            int sanLoss = Random.Range(0, cultistLevel);
            member.ApplyStatChanges(new() { sanity = -sanLoss});
            totalMemberSanityLoss += sanLoss;
        }

        // Display results
        report = $"{cultist.name} {Text.Get("SACR1")} {sanityLossLeader}.{(totalMemberSanityLoss > 0 ? $"\n{Text.Get("SACR2")}" : "")}\n{statReport}";
    }
}