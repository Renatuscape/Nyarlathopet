using UnityEngine;

public class BtnCommenceRitual : MonoBehaviour
{
    public void BtnCommence()
    {
        AlertSystem.Prompt("Commence the summoning with these offerings and sacrifices?", () =>
        {
            RitualController.CommenceRitual();
        });
    }
}