using UnityEngine;

public static class RitualSacrificeHelper
{
    public static void SacrificeCultist(Horror ritualState, Human cultist, out string report)
    {

        GameplayManager.dummyData.cultMembers.Remove(cultist);
        GameplayManager.dummyData.funds += cultist.funds;

        int skillPoints = cultist.magick + cultist.strength + cultist.lore;
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

        if (ritualState.strength < cultist.magick || cultist.magick > 0)
        {
            if (Random.Range(0, 99) > 49)
            {
                ritualState.magick += cultist.magick;
                statReport += "MGC+" + cultist.magick + " ";
            }
            else
            {
                ritualState.strength -= cultist.magick;
                statReport += "STR-" + cultist.magick + " ";
            }
        }

        if (ritualState.intrigue < cultist.strength || cultist.strength > 0)
        {
            if (Random.Range(0, 99) > 49)
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
            if (ritualState.abstraction < cultist.lore || Random.Range(0, 99) > 49)
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
        GameplayManager.dummyData.cultLeader.sanity -= sanityLossLeader;

        int totalMemberSanityLoss = 0;

        foreach (var member in GameplayManager.dummyData.cultMembers)
        {
            int sanLoss = Random.Range(0, cultistLevel);
            member.sanity -= sanLoss;
            totalMemberSanityLoss += sanLoss;
        }

        // Display results
        report = $"The sacrifice of {cultist.name} reduces your sanity by {sanityLossLeader}.{(totalMemberSanityLoss > 0 ? "\nYour cultists have also suffered." : "")}\n{statReport}";
    }
}