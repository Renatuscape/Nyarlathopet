using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnNewGame : MonoBehaviour
{
    public void StartNewGame()
    {
        Report.Write("MainMenu BtnNewGame", "Loading NewGame scene.");
        SceneManager.LoadScene("NewGame");
    }
}
