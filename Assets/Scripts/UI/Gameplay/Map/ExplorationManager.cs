using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public static class ExplorationManager
{
    public static void SeekArtefacts(Location location)
    {
        // Add costs here

        Item item = ItemFactory.GenerateItemFromLocation(location);
        GameplayManager.dummyData.inventory.Add(item);

        AlertSystem.Force($"You obtained {item.name}", () =>
        {
            HandleEndeavourPoints();
        });
    }
    public static void ThwartEnemies()
    {
        // Reduce notoriety against a random cost of cultists and network

        HandleEndeavourPoints();
    }

    internal static void RecruitMembers(Location currentLocation)
    {
        // Add costs here
        // Add chance of increased network

        Human cultist = CultistFactory.GetCultist(currentLocation.level);
        cultist.origin = currentLocation.name;
        GameplayManager.dummyData.cultMembers.Add(cultist);

        AlertSystem.Force($"{cultist.name} joined your cult.", () =>
        {
            HandleEndeavourPoints();
        });
    }

    static void HandleEndeavourPoints()
    {
        // In the future, it should be possible to spend multiple endeavour points
        GameplayManager.SubtractEndeavourPoints(1);
    }
}
