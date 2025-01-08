using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnContinue : MonoBehaviour
{
    public void ContinueGameplay()
    {
        Report.Write("MainMenu BtnContinue", "Loading Gameplay scene.");
        SceneManager.LoadScene("Gameplay");
    }
}
