using UnityEngine.SceneManagement;

public static class EventHelper
{
    public static void PromptGameOverOptions(string textAlert)
    {
        Player.SetPlayerData(null);
        AlertSystem.Choice(textAlert, new(){
            ("", () => {SceneManager.LoadScene("NewGame");})
        });
    }
}