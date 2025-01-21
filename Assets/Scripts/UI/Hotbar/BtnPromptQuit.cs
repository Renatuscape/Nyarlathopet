using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuitApplicaiton : MonoBehaviour
{
    public void BtnPromptQuit()
    {
        AlertSystem.Prompt(Repository.GetText("ALERT-QUIT"),
            () =>
            {
                DebugManager.WriteDebugSessionLog(true);
                Application.Quit();
            });
    }
}
