// When the player has spent AP, these commands execute the correct stat and inventory changes
using UnityEngine;

public static class ExplorationController
{
    public static void SeekArtefactsForNetwork(Location location)
    {

    }

    public static void SeekArtefactsForCultists(Location location)
    {

    }

    public static void SeekArtefactsForSanity(Location location)
    {

    }

    public static void RecruitMembersForNetwork(Location location)
    {

    }

    public static void RecruitMembersForSanity(Location location)
    {
        CalculateSanityLoss(location);
    }

    public static void ThwartEnemiesForNetwork(Location location)
    {

    }

    public static void ThwartEnemiesForCultists(Location location)
    {

    }

    static int CalculateSanityLoss(Location location)
    {
        int averageLevel = Mathf.FloorToInt(Player.Data.level + location.level * 0.5f);

        int sanityCost = Random.Range(0, averageLevel + 1);

        if (location.isRisky)
        {
            sanityCost = Mathf.FloorToInt(sanityCost * 1.5f);
        }

        return sanityCost;
    }
}
