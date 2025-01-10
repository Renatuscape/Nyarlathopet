using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnNewGame : MonoBehaviour
{
    public void StartNewGame()
    {
        AlertSystem.Prompt("START OVER?\n\nCurrent save data will be erased upon finalising game customisation.", () =>
        {
            Report.Write("MainMenu BtnNewGame", "Loading NewGame scene.");
            SceneManager.LoadScene("NewGame");
        });
    }
}
