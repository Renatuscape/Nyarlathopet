using UnityEngine;
using UnityEngine.SceneManagement;

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
}