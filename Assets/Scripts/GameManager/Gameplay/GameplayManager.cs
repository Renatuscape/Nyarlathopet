using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    public static PlayerData dummyData; // Refer and make changes to this object instead of Player.Data in the gameplay scene
    public PlayerData dummyDisplay;
    public static int endeavourPoints;

    public HotbarController hotbarController;

    private void Start()
    {
        Initialise();
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
            dummyData = Player.Data.DeepCopy();
            dummyDisplay = dummyData;
        }
        else // Game is in test mode. Create test data
        {
            dummyData = new();
            dummyData.CreateDummyData();
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

            // Destroy player data
            Player.SetPlayerData(null);

            // Load GameOver scene

            // Placeholder until GameOver scene is ready
            AlertSystem.Force("YOU ARE DEAD.", () =>
            {
                SceneManager.LoadScene("NewGame");
            });
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
        if (CheckIfLeaderSane() && Player.Data.cultMembers.Count < 1)
        {
            return true;
        }

        return false;
    }

    bool CheckIfLeaderSane()
    {
        if (Player.Data?.cultLeader?.sanity < 1)
        {
            return false;
        }

        return true;
    }

    IEnumerator StartNewRoundAsync()
    {
        yield return SaveManager.SaveDataAsync();
        endeavourPoints = 3;
        hotbarController.RefreshForNewRound();
    }

    public static void EndRound()
    {
        instance.RunEndRoundRoutine();
    }
}
