using UnityEngine;
using Random = UnityEngine.Random;
public static class ExplorationManager
{
    public static void SeekArtefacts(Location location)
    {
        // Add costs here

        Item item = ItemFactory.GenerateItemFromLocation(location);
        GameplayManager.dummyData.inventory.Add(item);

        string alertMessage = $"{Repository.GetText("EXP-A0")} {item.name}.";
        int networkGain = Random.Range(0, item.level + 1);
        int notorietyGain = Random.Range(0, item.level + 1);

        if (networkGain > 0)
        {
            alertMessage += $"\n{Repository.GetText("EXP-A1")} " + networkGain;
            GameplayManager.dummyData.network += networkGain;
        }

        if (notorietyGain > 0)
        {
            alertMessage += $"\n{Repository.GetText("EXP-A2")} " + notorietyGain;
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
            AlertSystem.Print(Repository.GetText("EXP-T0"));
        }
        else
        {
            int maxReduction = Mathf.FloorToInt(GameplayManager.dummyData.notoriety * 0.75f);
            int reduction = Random.Range(1, maxReduction) + GameplayManager.dummyData.level;
            string alertMessage = $"{Repository.GetText("EXP-T1")} {reduction}.";

            GameplayManager.dummyData.notoriety -= reduction;
            int startingNetwork = GameplayManager.dummyData.network;
            int currentNetwork = startingNetwork -= reduction;

            while (currentNetwork < 0)
            {
                currentNetwork += 5;

                if (GameplayManager.dummyData.cultMembers.Count > 1)
                {
                    Human cultist = GameplayManager.dummyData.cultMembers[Random.Range(0, GameplayManager.dummyData.cultMembers.Count)];
                    alertMessage += $"\n{cultist.name} {Repository.GetText("EXP-T2")}";
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

            alertMessage += $"\n{Repository.GetText("EXP-T3")} {startingNetwork - currentNetwork}";

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

        if (currentLocation.hasMoney)
        {
            cultist.funds = cultist.funds * 3;
        }

        GameplayManager.dummyData.cultMembers.Add(cultist);

        AlertSystem.Force($"{cultist.name} {Repository.GetText("EXP-R0")}", () =>
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
