using UnityEngine;
using Random = UnityEngine.Random;
public static class ExplorationManager
{
    public static void SeekArtefacts(Location location)
    {
        // Add costs here

        Item item = ItemFactory.GenerateItemFromLocation(location);
        GameplayManager.dummyData.inventory.Add(item);

        string alertMessage = $"You obtained {item.name}.";
        int networkGain = Random.Range(0, item.level + 1);
        int notorietyGain = Random.Range(0, item.level + 1);

        if (networkGain > 0)
        {
            alertMessage += $"\nYour endeavours improved your network by " + networkGain;
            GameplayManager.dummyData.network += networkGain;
        }

        if (notorietyGain > 0)
        {
            alertMessage += $"\nThose who would seek to stop you have taken notice. Notoriety increased by " + notorietyGain;
            GameplayManager.dummyData.notoriety += notorietyGain;
        }

        AlertSystem.Force(alertMessage, () =>
        {
            HandleEndeavourPoints();
        });
    }

    public static void ThwartEnemies()
    {
        // Reduce notoriety against a random cost of cultists and network

        if (GameplayManager.dummyData.notoriety < 1)
        {
            AlertSystem.Print("You do not yet have any notoriety, and therefore no enemies.");
        }
        else
        {
            int maxReduction = Mathf.FloorToInt(GameplayManager.dummyData.notoriety * 0.75f);
            int reduction = Random.Range(1, maxReduction) + GameplayManager.dummyData.level;
            string alertMessage = $"You reduced your notoriety by {reduction}.";

            GameplayManager.dummyData.notoriety -= reduction;
            int startingNetwork = GameplayManager.dummyData.network;
            int currentNetwork = startingNetwork -= reduction;

            while (currentNetwork < 0)
            {
                currentNetwork += 5;

                if (GameplayManager.dummyData.cultMembers.Count > 1)
                {
                    Human cultist = GameplayManager.dummyData.cultMembers[Random.Range(0, GameplayManager.dummyData.cultMembers.Count)];
                    alertMessage += $"\n{cultist.name} was lost to the enemy.";
                    GameplayManager.dummyData.cultMembers.Remove(cultist);
                }
                else
                {

                }
            }

            if (currentNetwork > startingNetwork)
            {
                currentNetwork = startingNetwork - 1;
            }

            alertMessage += $"\nYour network decreased by " + (startingNetwork - currentNetwork);

            AlertSystem.Force(alertMessage, () =>
            {
                HandleEndeavourPoints();
            });
        }
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
