using System;

public static class CultistFactory
{
    private static readonly Random random = new();
    public static Human GetCultist(int level)
    {
        if (level <= 0)
        {
            level = 1;
        }

        Report.Write("CultistFactory", $"Attempting to generate a cultist of level {level}.");

        Human cultist = new()
        {
            name = RandomNameGenerator.GetRandomHumanName(),
            type = CreatureType.Cultist,
            origin = "???"
        };

        MundaneStats stats = new();
        stats.funds = random.Next(0, (level * 5) + 1);
        stats.sanity = random.Next(5, (level * 10) + 1);

        int skillPoints = level * 2;

        while (skillPoints > 0)
        {
            int randomStat = random.Next(0, 3);
            if (randomStat == 0)
            {
                stats.occultism++;
            }
            else if (randomStat == 1)
            {
                stats.strength++;
            }
            else
            {
                stats.lore++;
            }

            skillPoints--;
        }

        cultist.ApplyStatChanges(stats);
        return cultist;
    }
}
