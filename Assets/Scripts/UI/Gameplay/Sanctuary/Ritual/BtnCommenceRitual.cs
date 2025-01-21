using UnityEngine;

public class BtnCommenceRitual : MonoBehaviour
{
    public void BtnCommence()
    {
        AlertSystem.Prompt(Repository.GetText("RIT-COMM"), () =>
        {
            RitualController.CommenceRitual();
        });
    }
}