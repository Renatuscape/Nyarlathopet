using System;

public static class DummyDataUpdater
{

    public static int UpdateRage(int points, bool decrease)
    {
        if (!GameplayManager.CheckIsPetActive()) { return 0; }

        if (decrease && points > 0)
        {
            points = -points;
        }

        int original = GameplayManager.dummyData.currentPet.rage;

        GameplayManager.dummyData.currentPet.ApplyStatChanges(new() { rage = points });
        return GameplayManager.dummyData.currentPet.rage - original;
    }

    public static int UpdateLeaderSanity(int points)
    {
        if (GameplayManager.dummyData.cultLeader == null) { return 0; }

        int original = GameplayManager.dummyData.cultLeader.sanity;
        GameplayManager.dummyData.cultLeader.ApplyStatChanges(new() { sanity = points });
        return GameplayManager.dummyData.cultLeader.sanity;
    }

    public static int UpdateNotoriety(int points)
    {
        return UpdateStat(ref GameplayManager.dummyData.notoriety, points);
    }

    public static int UpdateNetwork(int points)
    {
        return UpdateStat(ref GameplayManager.dummyData.network, points);
    }

    static int UpdateStat(ref int stat, int points)
    {
        int original = stat;
        stat = Math.Clamp(original + points, 0, 99);
        return stat - original;
    }
}