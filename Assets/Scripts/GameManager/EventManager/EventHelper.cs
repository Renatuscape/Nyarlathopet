using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
public static class EventHelper
{
    public static void PromptGameOverOptions(string textAlert)
    {
        Player.SetPlayerData(null);
        AlertSystem.Choice(textAlert, new(){
            (Repository.GetText("OPTION-NEWGAME"), () => {SceneManager.LoadScene("NewGame");}),
            (Repository.GetText("OPTION-RETRY"), () => {SceneManager.LoadScene("MainMenu");}),
            (Repository.GetText("OPTION-QUIT"), () => {
                DebugManager.WriteDebugSessionLog(true);
                Application.Quit();
                })
        });
    }

    public static void ChooseNewLeader(Action doAfterChoice)
    {
        var members = GameplayManager.dummyData.cultMembers.OrderByDescending(m => m.sanity).ToList();
        List<(string, Action)> options = new();

        for (int i = 0; i < AlertSystem.MaxOptions && i < members.Count - 1; i++) {
            int capturedIndex = i;
            options.Add((members[capturedIndex].name, () => ElectLeader(members[capturedIndex])));
        }

        AlertSystem.Choice(Repository.GetText("EVENT-LEADER0"), options);

        void ElectLeader(Human cultist)
        {
            GameplayManager.dummyData.cultLeader = cultist;
            GameplayManager.dummyData.cultMembers.Remove(cultist);
            doAfterChoice.Invoke();
        }
    }

    public static void NewCultistJoined(Action doAfterChoice)
    {
        int level = Random.Range(1, GameplayManager.dummyData.level);
        var newCultist = CultistFactory.GetCultist(level);
        newCultist.origin = Repository.GetLocationNameByLevel(level);
        GameplayManager.dummyData.cultMembers.Add(newCultist);

        AlertSystem.Force(newCultist.name + " " + Repository.GetText("EVENT-NEWCULTIST"), () =>
        {
            doAfterChoice.Invoke();
        });
    }
}