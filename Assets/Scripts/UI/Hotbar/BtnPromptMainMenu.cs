using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnPromptMainMenu : MonoBehaviour
{
    public void BtnPromptMain()
    {
        AlertSystem.Prompt("Return to main menu?\nProgress on the current month will be lost.",
            () =>
            {
                SceneManager.LoadScene("MainMenu");
            });
    }
}
