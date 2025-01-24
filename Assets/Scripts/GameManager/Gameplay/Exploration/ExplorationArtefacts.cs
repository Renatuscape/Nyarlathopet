using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public static class ExplorationArtefacts
{
    public static void PromptArtefactOptions(Location location)
    {
        AlertSystem.Choice(Repository.GetText("EXP-A"), GetOptions(location), false); // Choice is not forced
    }

    static void GetArtefactForSanity(Location location)
    {

    }

    static void GetArtefactForCultist(Location location)
    {
        List<Human> cultists = GameplayManager.dummyData.cultMembers;
        List<string> deadCultists = new();
        int danger = location.isRisky ? 70 : 35;
        int network = 0;
        int notoriety = 0;
        Item foundItem = null;

        bool checkPassed = false;

        while (true)
        {
            if (cultists.Count < 1)
            {
                break;
            }

            checkPassed = Random.Range(0, 100) > danger;

            if (checkPassed)
            {
                break;
            }
            else
            {
                var randomCultist = cultists[Random.Range(0, cultists.Count)];
                deadCultists.Add(randomCultist.name);
                cultists.Remove(randomCultist);
            }
        }

        if (checkPassed)
        {
            int itemBonus = deadCultists.Count > 0 ? (deadCultists.Count * 2 + GameplayManager.dummyData.level) / 2 : 0;
            foundItem = AddNewArtefact(location, itemBonus);

            network = Random.Range(0, 100) > 65 ? Math.Max(1, GameplayManager.UpdateNetwork((location.level + GameplayManager.dummyData.level) / 2)) : 0;
        }

        notoriety = deadCultists.Count > 0 && Random.Range(0, 100) > 50 ? GameplayManager.UpdateNotoriety((deadCultists.Count * 2 + GameplayManager.dummyData.level) / 2) : 0;


        AlertSystem.Force(GetReport(foundItem, network, notoriety, 0, 0, deadCultists), () =>
        {
            HandleEndeavourPoints(1);
        });
    }

    static void GetArtefactForNetwork(Location location)
    {

    }

    static void GetArtefactForFunds(Location location)
    {

    }

    public static Item AddNewArtefact(Location location, int bonusPoints)
    {
        Item item = ItemFactory.GenerateItemFromLocation(location, bonusPoints);
        GameplayManager.dummyData.inventory.Add(item);
        return item;
    }

    static List<(string text, Action action)> GetOptions(Location location)
    {
        List<(string text, Action action)> options = new()
        {
            (Repository.GetText("EXP-ABTN0"), () => GetArtefactForSanity(location))
        };

        if (GameplayManager.dummyData.cultMembers.Count > 0)
        {
            options.Add((Repository.GetText("EXP-ABTN1"), () => GetArtefactForCultist(location)));
        }

        if (GameplayManager.dummyData.network > 3)
        {
            options.Add((Repository.GetText("EXP-ABTN2"), () => GetArtefactForNetwork(location)));
        }

        if (GameplayManager.dummyData.funds > 50)
        {
            options.Add((Repository.GetText("EXP-ABTN3"), () => GetArtefactForFunds(location)));
        }

        return options;
    }

    static string GetReport(Item item, int network, int notoriety, int sanity, int funds, List<string> deadCultists)
    {
        string report = Repository.GetText("EXP-AR0") + "\n\n";

        report += (item == null ? Repository.GetText("EXP-AR-FAIL") : Repository.GetText($"EXP-AR-SUCCESS") + " " + item.name + ".") + "\n";

        if (network != 0)
        {
            report += Repository.GetText("EXP-A1") + network + "\n";
        }

        if (notoriety != 0)
        {
            report += Repository.GetText("EXP-A2") + notoriety + "\n";
        }

        if (sanity != 0)
        {
            report += Repository.GetText("EXP-A3") + sanity + "\n";
        }

        if (funds != 0)
        {
            report += Repository.GetText("EXP-A4") + funds + "\n";
        }

        if (deadCultists != null && deadCultists.Count > 0)
        {

            report += Repository.GetText("EXP-A5") + " " + FormatNameList(deadCultists) + ".";
        }

        return report;
    }
    static string FormatNameList(List<string> names)
    {
        if (names.Count == 1)
            return names[0];

        if (names.Count == 2)
            return $"{names[0]} and {names[1]}";

        // For 3 or more names with Oxford comma
        var namesCopy = new List<string>(names);
        string lastNames = $"{namesCopy[^2]}, and {namesCopy[^1]}";
        namesCopy.RemoveRange(namesCopy.Count - 2, 2);

        return string.Join(", ", namesCopy) + ", " + lastNames;
    }

    static void HandleEndeavourPoints(int cost)
    {
        ExplorationManager.HandleEndeavourPoints(cost);
    }
}