using System;
using System.Collections.Generic;
using System.Linq;

public static class EventController
{
    static List<EventData> eventQueue = new();

    public static void AddEventToQueue(EventData eventData)
    {
        Report.Write("EventManager", "Adding event to queue: " + eventData);
        eventQueue.Add(eventData);
    }

    public static void ForceEvent(EventData eventData, bool eraseQueue)
    {
        if (eraseQueue)
        {
            eventQueue.Clear();
        }

        LoadEventSceneAndExecute(eventData.action);
    }

    public static void InitiateNormalSequence()
    {
        // Build events from current dummyData in GameplayManager


        LoadEventSceneAndExecute(() => ExecuteNextInQueue());
    }

    public static void ExecuteNextInQueue()
    {
        if (eventQueue.Count < 1)
        {
            Report.Write("EventManager", "No events left in queue. Loading gameplay scene and attempting new round.");
            SceneLoader.LoadSceneAndExecute("Gameplay", () => GameplayManager.AttemptNewRound());
            return;
        }

        EventData eventData = EventPicker();

        Report.Write("EventManager", "Executing next in queue: " + eventData.name);
        eventData.Execute();
    }

    static void LoadEventSceneAndExecute(Action doAfterLoad)
    {
        SceneLoader.LoadSceneAndExecute("EventScene", doAfterLoad);
    }

    static EventData EventPicker(bool removeEventFromQueue = true)
    {
        EventData eventData;

        if (eventQueue.Count < 1)
        {
            return null;
        }

        if (eventQueue.Count == 1)
        {
            eventData = eventQueue[0];
        }
        else
        {
            // Choose most pressing event left in queue
            eventData = eventQueue.OrderBy(e => e.priority).First();
        }

        if (removeEventFromQueue)
        {
            eventQueue.Remove(eventData);
        }

        return eventData;
    }
}
