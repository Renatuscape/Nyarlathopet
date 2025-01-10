using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SanityTools
{
    public static int GetSanityLossByLocation(Location location)
    {
        int averageLevel = Mathf.FloorToInt(Player.Data.level + location.level * 0.5f);

        int sanityCost = Random.Range(0, averageLevel + 1);

        if (location.isRisky)
        {
            sanityCost = Mathf.FloorToInt(sanityCost * 1.5f);
        }

        return sanityCost;
    }

    public static int GetSanityLossByHorror(Horror horror)
    {
        int lossAdjustedForLore = horror.sanityLoss + Player.Data.cultLeader.lore;

        return lossAdjustedForLore;
    }
}
