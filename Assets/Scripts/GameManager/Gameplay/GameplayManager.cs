using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public static PlayerData dummyData; // Refer and make changes to this object instead of Player.Data in the gameplay scene
    public PlayerData dummyDisplay;
    public RitualManager ritualManager;
    public static int EndeavourPoints { get; private set; }
    public const int MaxEndeavourPoints = 3;
    public static bool isPetFed;

    public HotbarController hotbarController;

    private void Start()
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
            dummyDisplay = dummyData;
        }
        else // Game is in test mode. Create test data
        {
            Report.Write(name, "Player data was null. Creating dummy data for test mode.");
            Player.CreateDummyData();
            dummyData = Player.Data.DeepCopy();
            dummyDisplay = dummyData;
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
            Report.Write(name, "Executing game over routine.");

            // Load GameOver scene

            // Placeholder until GameOver scene is ready
            {
                Player.SetPlayerData(null);
                AlertSystem.Force("YOU ARE DEAD.", () =>
                {
                    SceneManager.LoadScene("NewGame");
                });
            }
        }
        // Check if a promotion is needed to replace the old leader
        else
        {
            // Exchange for an event scene once round logic has been properly implemented
            AlertSystem.Force($"YOU SURVIVED THE MONTH.\n{DateCalculator.GetGameDate(dummyData.rounds)}", () =>
            {
                StartNewRound();
            });
        }
    }

    void StartNewRound()
    {
        Report.Write(name, "Starting new round.");
        StartCoroutine(StartNewRoundAsync());
    }

    bool CheckIfGameOver()
    {
        if (!IsLeaderSane() && Player.Data.cultMembers.Count < 1)
        {
            Report.Write(name, "Cult leader is insane, and there are no cultists left to assume their position.");
            return true;
        }

        return false;
    }

    bool IsLeaderSane()
    {
        if (Player.Data?.cultLeader?.sanity < 1)
        {
            Report.Write(name, "Cult leader has gone insane with a sanity of " + Player.Data.cultLeader.sanity);
            return false;
        }
        Report.Write(name, "Cult leader remains sane with a sanity of " + Player.Data.cultLeader.sanity);
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

    public static void SubtractEndeavourPoints(int amount)
    {
        instance.SubtractEP(amount);
    }
}
