using UnityEngine;

public class BtnCommenceRitual : MonoBehaviour
{
    public void BtnCommence()
    {
        AlertSystem.Prompt(Text.Get("RIT-COMM"), () =>
        {
            RitualController.CommenceRitual();
        });
    }
}