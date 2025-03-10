using UnityEngine;
using Random = UnityEngine.Random;
public static class ExplorationManager
{

    public static void ThwartEnemies()
    {
        // Reduce notoriety against a random cost of cultists and network

        if (GameplayManager.dummyData.notoriety < 1)
        {
            AlertSystem.Print(Text.Get("EXP-T0"));
        }
        else
        {
            int maxReduction = Mathf.FloorToInt(GameplayManager.dummyData.notoriety * 0.75f);
            int reduction = Random.Range(1, maxReduction) + GameplayManager.dummyData.level;
            string alertMessage = $"{Text.Get("EXP-T1")} {reduction}.";

            GameplayManager.dummyData.notoriety -= reduction;
            int startingNetwork = GameplayManager.dummyData.network;
            int currentNetwork = startingNetwork -= reduction;

            while (currentNetwork < 0)
            {
                currentNetwork += 5;

                if (GameplayManager.dummyData.cultMembers.Count > 1)
                {
                    Human cultist = GameplayManager.dummyData.cultMembers[Random.Range(0, GameplayManager.dummyData.cultMembers.Count)];
                    alertMessage += $"\n{cultist.name} {Text.Get("EXP-T2")}";
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

            alertMessage += $"\n{Text.Get("EXP-T3")} {startingNetwork - currentNetwork}";

            AlertSystem.Force(alertMessage, () =>
            {
                HandleEndeavourPoints(1);
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
            cultist.ApplyStatChanges(new() { funds = cultist.funds * 2});
        }

        GameplayManager.dummyData.cultMembers.Add(cultist);

        AlertSystem.Force($"{cultist.name} {Text.Get("EXP-R0")}", () =>
        {
            HandleEndeavourPoints(1);
        });
    }

    public static void HandleEndeavourPoints(int points)
    {
        // In the future, it should be possible to spend multiple endeavour points
        GameplayManager.SubtractEndeavourPoints(points);
    }
}
