using System;

public static class CultistFactory
{
    private static readonly Random random = new();
    public static Human GetCultist(int level)
    {
        Human cultist = new()
        {
            name = HumanNameGenerator.GetRandomName(),
            type = CreatureType.Cultist,
            funds = random.Next(0, (level * 5) + 1)
        };

        return cultist;
    }
}
