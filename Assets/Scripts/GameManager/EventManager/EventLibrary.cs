using System;
using System.Collections.Generic;
using System.Linq;

public class EventData
{
    public string name;
    public int priority;
    public int minimumCultists;
    public Action action;

    public void Execute()
    {
        Report.Write(name, "Executing action.");
        action.Invoke();
    }
}

public static class EventLibrary
{
    public static readonly List<EventData> events = new()
    {
        new()
        {
            name = "CultistMutiny",
            priority = 0, // Highest priority
            minimumCultists = 1,
            action = () =>
            {
                HandleEndEvent("EnemyRaid");
            }
        },
        new()
        {
            name = "ChooseNewLeader",
            priority = 1, // Second highest priority
            minimumCultists = 0, // Game over if this initiates with 0 cultists
            action = () =>
            {
                AlertSystem.Force("Placeholder for Choose New Leader event.", () =>
                {
                    if (GameplayManager.dummyData.cultMembers.Count < 1)
                    {
                        Report.Write("EventLibrary", "No cult members to replace old leader. Game is over.");
                    }
                    else
                    {
                        GameplayManager.dummyData.cultLeader = GameplayManager.dummyData.cultMembers.First();
                    }

                    HandleEndEvent("ChooseNewLeader");
                });
            }
        },
        new()
        {
            name = "PetOnRampage",
            priority = 80,
            minimumCultists = 0,
            action = () =>
            {
                HandleEndEvent("PetOnRampage");
            }
        },
        new()
        {
            name = "EnemyRaid",
            priority = 100, // Lowest priority
            minimumCultists = 0,
            action = () =>
            {
                HandleEndEvent("EnemyRaid");
            }
        },
        new()
        {
            name = "PetNotFed",
            priority = 10,
            minimumCultists = 0,
            action = () =>
            {
                HandleEndEvent("PetNotFed");
            }
        },
        new()
        {
            name = "NoCurrentPet",
            priority = 50,
            minimumCultists = 0,
            action = () =>
            {
                HandleEndEvent("NoCurrentPet");
            }
        }
    };

    static void HandleEndEvent(string eventName, bool stopQueue = false)
    {
        Report.Write("EventLibrary", "Completed " + eventName);
        EventController.ExecuteNextInQueue();
    }
}