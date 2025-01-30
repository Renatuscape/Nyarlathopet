using UnityEngine;

public class BtnEnableRitualMenu : MonoBehaviour
{
    public void BtnStartRitual()
    {
        if (GameplayManager.EndeavourPoints == GameplayManager.MaxEndeavourPoints)
        {
            Report.Write(name, "Opening ritual menu.");
            RitualController.EnableRitualMenu();
        }
        else
        {
            AlertSystem.Print($"{Text.Get("RIT-ERR")} {GameplayManager.MaxEndeavourPoints} EP.");
        }
    }
}