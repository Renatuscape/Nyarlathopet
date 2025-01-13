using UnityEngine;

public class BtnAbortRitual : MonoBehaviour
{
    public void BtnAbort()
    {
        AlertSystem.Prompt("ABANDON PROGRESS AND ABORT RITUAL?\nAll amassed power will be lost. Any sacrifices made will be for naught.", () =>
        {
            RitualController.AbortRitual();
        });
    }
}
