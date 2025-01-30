﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StatConverter
{
    public static string CreateCreatureStatChangeReport(CreatureStats stats, bool includeUnchanged)
    {
        string report = "";

        if (includeUnchanged || stats.mythos != 0)
        {
            report += $"MTH{GetValueWithOperator(stats.mythos)} ";
        }
        if (includeUnchanged || stats.intrigue != 0)
        {
            report += $"ITR{GetValueWithOperator(stats.intrigue)} ";
        }
        if (includeUnchanged || stats.magick != 0)
        {
            report += $"MGC{GetValueWithOperator(stats.magick)} ";
        }
        if (includeUnchanged || stats.abstraction != 0)
        {
            report += $"ABS{GetValueWithOperator(stats.abstraction)} ";
        }
        if (includeUnchanged || stats.strength != 0)
        {
            report += $"STR{GetValueWithOperator(stats.strength)} ";
        }
        if (includeUnchanged || stats.rage != 0)
        {
            report += $"RGE{GetValueWithOperator(stats.rage)} ";
        }

        return report.Trim(' ');

        string GetValueWithOperator(int value)
        {
            return (value < 0 ? "-" : "+") + value;
        }
    }

    public static CreatureStats ConvertItemToCreatureStats(Item item, float skillPointMultiplier = 0.5f)
    {
        MundaneStats stats = new MundaneStats() { lore = item.lore, occultism = item.occultism, strength = item.strength };
        return ConvertMundaneToCreatureStats(stats, skillPointMultiplier);
    }
    public static CreatureStats ConvertHumanToCreatureStats(Human human, float skillPointMultiplier = 0.5f)
    {
        MundaneStats stats = new MundaneStats() { lore = human.lore, occultism = human.occultism, strength = human.strength};
        return ConvertMundaneToCreatureStats(stats, skillPointMultiplier);
    }

    static CreatureStats ConvertMundaneToCreatureStats(MundaneStats mundane, float skillPointMultiplier = 0.5f)
    {
        CreatureStats statChanges = new();

        int skillPoints = mundane.occultism + mundane.strength + mundane.lore;
        int cultistLevel = Math.Max(1, Mathf.FloorToInt(skillPoints * skillPointMultiplier));

        bool isSuccessful = false;

        while (!isSuccessful) {
            MundaneStats mundaneCopy = new() { lore = mundane.lore, strength = mundane.strength, occultism = mundane.occultism };
            isSuccessful = AttemptStatConversion(cultistLevel, mundaneCopy, statChanges);
        }

        return statChanges;
    }

    static bool AttemptStatConversion(int cultistLevel, MundaneStats mundane, CreatureStats statChanges)
    {
        // STAT INCREASES
        for (int i = 0; i < cultistLevel; i++)
        {
            int stat = Random.Range(0, 3);
            int statIncrease = 1;

            if (stat == 0 && mundane.lore > 0)
            {
                mundane.lore--;

                if (Random.Range(0, 2) == 0) { statChanges.mythos += statIncrease; }
                else { statChanges.intrigue += statIncrease; }
            }
            else if (stat == 1 && mundane.occultism > 0)
            {
                mundane.occultism--;

                if (Random.Range(0, 2) == 0) { statChanges.magick += statIncrease; }
                else { statChanges.abstraction += statIncrease; }
            }
            else if (stat == 2 && mundane.strength > 0)
            {
                mundane.strength--;

                if (Random.Range(0, 3) != 0) { statChanges.strength += statIncrease; }
                else { statChanges.rage += statIncrease; }
            }
        }

        return !(statChanges.mythos == 0 && statChanges.strength == 0 && statChanges.intrigue == 0 && statChanges.abstraction == 0 && statChanges.magick == 0 && statChanges.rage == 0);
    }
}
