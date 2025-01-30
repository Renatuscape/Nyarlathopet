using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class EventData
{
    public Event type;
    public int priority;
    public Func<bool> condition;
    public Action action;

    public void Execute()
    {
        Report.Write(type.ToString(), "Executing action.");
        action.Invoke();
    }

    public bool CheckCondition()
    {
        return condition == null || condition.Invoke();
    }
}

public static class EventLibrary
{
    static readonly Dictionary<Event, EventData> events = new()
    {
        {
            Event.NoLeader,
            new()
            {
                type = Event.NoLeader,
                priority = 0,
                condition = () =>
                {
                    return (GameplayManager.dummyData.cultLeader == null || GameplayManager.dummyData.cultLeader.sanity < 1) && GameplayManager.dummyData.cultMembers.Count > 0;
                },
                action = () =>
                {
                    EventHelper.ChooseNewLeader(() => HandleEndEvent("NoLeader"));
                }
            }
        },
        {
            Event.CultMutiny,
            new()
            {
                type = Event.CultMutiny,
                priority = 1,
                condition = () =>
                {
                    return GameplayManager.mutineer != null
                    && GameplayManager.dummyData.cultLeader != null
                    && GameplayManager.dummyData.cultMembers.Count > 0;
                },
                action = () =>
                {
                    AlertSystem.Force("Cultist mutiny placeholder", () =>
                    {
                        HandleEndEvent("CultMutiny");
                    });
                }
            }
        },
        {
            Event.PetNotFed,
            new()
            {
                type = Event.PetNotFed,
                priority = 10,
                condition = () =>
                {
                    return GameplayManager.CheckIsPetActive() && !GameplayManager.isPetFed;
                },
                action = () =>
                {
                    AlertSystem.Force("Pet not fed placeholder", () =>
                    {
                        HandleEndEvent("PetNotFed");
                    });
                }
            }
        },
        {
            Event.PetOnRampage,
            new()
            {
                type = Event.PetOnRampage,
                priority = 80,
                condition = () =>
                {
                    if (GameplayManager.CheckIsPetActive())
                    {
                        int rage = GameplayManager.dummyData.currentPet.rage;
                        int rageThreshold = 150 - rage - GameplayManager.dummyData.level; // Danger of rampage increases with player level

                        return Random.Range(0, 100) > rageThreshold;
                    }

                    return false;
                },
                action = () =>
                {
                    AlertSystem.Force("Pet on rampage placeholder", () =>
                    {
                       HandleEndEvent("PetOnRampage");
                    });
                }
            }
        },
        {
            Event.CultRaided,
            new()
            {
                type = Event.CultRaided,
                priority = 100, // Lowest priority
                condition = () =>
                {
                    int raidThreshold = 125 - GameplayManager.dummyData.notoriety;
                    return Random.Range(0, 100) > raidThreshold;
                },
                action = () =>
                {
                    AlertSystem.Force("Cult raided placeholder", () =>
                    {
                        HandleEndEvent("CultRaided");
                    });
                }
            }
        },
        {
            Event.PetNotSummoned,
            new()
            {
                type = Event.PetNotSummoned,
                priority = 50,
                condition = () =>
                {
                    return !GameplayManager.CheckIsPetActive()
                    && GameplayManager.dummyData.cultMembers.Count > 0; // No point if there are no members to be mad
                },
                action = () =>
                {
                    AlertSystem.Force("Pet not summoned placeholder", () =>
                    {
                        HandleEndEvent("PetNotSummoned");
                    });
                }
            }
        },
        {
            Event.NewCultistJoined,
            new()
            {
                type = Event.NewCultistJoined,
                priority = 51,
                condition = () =>
                {
                    if (GameplayManager.CheckIsPetActive()) // New members won't join if there is no pet summoned
                    {
                        int threshold = 50 + (GameplayManager.dummyData.cultMembers.Count * 5 ) - GameplayManager.dummyData.notoriety;

                        return Random.Range(0, 100) > threshold;
                    }

                    return false;
                },
                action = () =>
                {
                    EventHelper.NewCultistJoined(() => HandleEndEvent("NewCultistJoined"));
                }
            }
        },
        {
            Event.NewMonth,
            new()
            {
                type = Event.NewMonth,
                priority = int.MaxValue, // Always play this last
                condition = () =>
                {
                    return !(GameplayManager.dummyData.cultLeader == null && GameplayManager.dummyData.cultMembers.Count < 1);
                },
                action = () =>
                {
                    AlertSystem.Force(Text.Get("ROUND-CLEAR0"),() => {
                        HandleEndEvent(Event.NewMonth.ToString());
                    });
                }
            }
        },
        {
            Event.GameOverDead,
            new()
            {
                type = Event.GameOverDead,
                priority = int.MinValue,
                condition = () =>
                {
                    return GameplayManager.dummyData.cultLeader == null && GameplayManager.dummyData.cultMembers.Count < 1;
                },
                action = () =>
                {
                    Report.Write("GameOverDead", "Opening game over prompt.");
                    EventHelper.PromptGameOverOptions(Text.Get("END-DEAD"));
                }
            }
        },
        {
            Event.GameOverInsane,
            new()
            {
                type = Event.GameOverInsane,
                priority = int.MinValue,
                condition = () =>
                {
                    return GameplayManager.dummyData.cultLeader.sanity < 1 && GameplayManager.dummyData.cultMembers.Count < 1;
                },
                action = () =>
                {
                    Report.Write("GameOverInsane", "Opening game over prompt.");
                    EventHelper.PromptGameOverOptions(Text.Get("END-INSANE"));
                }
            }
        }
    };

    static void HandleEndEvent(string eventName, bool stopQueue = false)
    {
        Report.Write("EventLibrary", "Completed " + eventName);
        EventController.ExecuteNextInQueue();
    }

    public static List<EventData> GetEventQueue(PlayerData data)
    {
        List<EventData> qualifyingEvents = events.Where(e => e.Value.CheckCondition()).Select(e => e.Value).ToList();

        return qualifyingEvents;
    }

    public static EventData GetEndGameEvent()
    {
        Report.Write("EventLibrary", "Returning game over event.");
        if (events[Event.GameOverInsane].CheckCondition())
        {
            return events[Event.GameOverInsane];
        }
        else
        {
            return events[Event.GameOverDead];
        }
    }
}

public enum Event
{
    NewMonth,
    PetNotFed,
    PetOnRampage,
    PetNotSummoned,
    NoLeader,
    CultRaided,
    CultMutiny,
    GameOverInsane,
    GameOverDead,
    NewCultistJoined
}
