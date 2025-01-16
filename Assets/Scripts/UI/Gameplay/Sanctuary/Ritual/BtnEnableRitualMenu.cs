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
            AlertSystem.Print($"The summoning ritual must commence at the beginning of a new month, when the moon and stars are correct.\nReturn when you have {GameplayManager.MaxEndeavourPoints} EP.");
        }
    }
}