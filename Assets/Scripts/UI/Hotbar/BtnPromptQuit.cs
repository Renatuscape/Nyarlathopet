using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnQuitApplicaiton : MonoBehaviour
{
    public void BtnPromptQuit()
    {
        AlertSystem.Prompt("Quit game?\nProgress is saved at the start of a new month. Any unsaved progress will be lost.",
            () =>
            {
                DebugManager.WriteDebugSessionLog(true);
                Application.Quit();
            });
    }
}
