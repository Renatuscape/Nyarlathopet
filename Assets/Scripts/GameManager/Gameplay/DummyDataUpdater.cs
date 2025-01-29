using System;

public static class DummyDataUpdater
{
    public static int UpdateNotoriety(int points)
    {
        return UpdateStat(ref GameplayManager.dummyData.notoriety, points);
    }

    public static int UpdateNetwork(int points)
    {
        return UpdateStat(ref GameplayManager.dummyData.network, points);
    }

    public static int UpdateRage(int points, bool decrease)
    {
        if (!GameplayManager.CheckIsPetActive()) { return 0; }

        if (decrease && points > 0)
        {
            points = -points;
        }

        return UpdateStat(ref GameplayManager.dummyData.currentPet.rage, points);
    }

    public static int UpdateSanity(int points)
    {
        if (GameplayManager.dummyData.cultLeader == null) { return 0; }
        return UpdateStat(ref GameplayManager.dummyData.cultLeader.sanity, points);
    }

    static int UpdateStat(ref int stat, int points)
    {
        int original = stat;
        stat = Math.Clamp(original + points, 0, 99);
        return stat - original;
    }
}