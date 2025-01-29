using System.Collections;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public static PlayerData dummyData; // Refer and make changes to this object instead of Player.Data in the gameplay scene
    public RitualManager ritualManager;
    public static int EndeavourPoints { get; private set; }
    public const int MaxEndeavourPoints = 3;
    public static bool isPetFed;
    public static Human mutineer;

    public HotbarController hotbarController;

    private void Awake()
    {
        Initialise();
        ritualManager.Initialise();
    }

    void Initialise()
    {
        instance = this;
        UpdateDummyFromOrigin();
        StartNewRound();
    }

    void UpdateDummyFromOrigin()
    {
        if (Player.Data != null)
        {
            Report.Write(name, "Player data was successfully found. Copying to dummy data.");
            dummyData = Player.Data.DeepCopy();
        }
        else // Game is in test mode. Create test data
        {
            Report.Write(name, "Player data was null. Creating dummy data for test mode.");
            Player.CreateDummyData();
            dummyData = Player.Data.DeepCopy();
        }
    }

    void UpdateOriginFromDummy()
    {
        Player.SetPlayerData(dummyData);
    }

    void RunEndRoundRoutine()
    {
        Report.Write(name, "Executing end round routine.");

        dummyData.rounds++;
        UpdateOriginFromDummy(); // Update player data. Will be used both for GameOver and next round

        Report.Write(name, "Checking if game is over.");

        if (CheckIfGameOver())
        {
            RunGameOverRoutine();
        }
        else
        {
            EventController.InitiateNormalSequence();
        }
    }

    void RunGameOverRoutine()
    {
        Report.Write(name, "Executing game over routine.");
        SceneLoader.LoadSceneAndExecute("EventScene", () => EventController.InitiateEndGameSequence());
    }
    void CheckNewRoundConditions()
    {
        Report.Write("GameplayManager", "Checking if game is over.");

        if (CheckIfGameOver())
        {
            RunGameOverRoutine();
        }
        else
        {
            StartNewRound();
        }
    }


    void StartNewRound()
    {
        Report.Write("GameplayManager", "Starting new round.");
        StartCoroutine(StartNewRoundAsync());
    }

    bool CheckIfGameOver()
    {
        if (!IsLeaderSane() && Player.Data.cultMembers.Count < 1)
        {
            Report.Write("GameplayManager", "Cult leader is insane, and there are no cultists left to assume their position.");
            return true;
        }

        return false;
    }

    bool IsLeaderSane()
    {
        if (Player.Data?.cultLeader?.sanity < 1)
        {
            Report.Write("GameplayManager", "Cult leader has gone insane with a sanity of " + Player.Data.cultLeader.sanity);
            return false;
        }
        Report.Write("GameplayManager", "Cult leader remains sane with a sanity of " + Player.Data.cultLeader.sanity);
        return true;
    }

    IEnumerator StartNewRoundAsync()
    {
        yield return SaveManager.SaveDataAsync();
        EndeavourPoints = MaxEndeavourPoints;
        isPetFed = false;
        hotbarController.RefreshForNewRound();
    }

    void SubtractEP(int amount)
    {
        EndeavourPoints -= amount;
        hotbarController.RefreshAfterEndeavour();

        if (EndeavourPoints <= 0)
        {
            RunEndRoundRoutine();
        }
    }

    public static void EndRound() // Are there instances besides EP loss that need to end a round?
    {
        instance.RunEndRoundRoutine();
    }

    public static void AttemptNewRound()
    {
        instance.CheckNewRoundConditions();
    }

    public static void SubtractEndeavourPoints(int amount)
    {
        instance.SubtractEP(amount);
    }

    public static bool CheckIsPetActive()
    {
        if (instance == null || dummyData.currentPet == null || dummyData.currentPet.id == "-1")
        {
            return false;
        }
        return true;
    }
}
