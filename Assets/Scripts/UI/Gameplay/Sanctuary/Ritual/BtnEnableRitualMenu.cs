using UnityEngine;

public class BtnEnableRitualMenu: MonoBehaviour
{
    public void BtnStartRitual()
    {
        Report.Write(name, "Opening ritual menu.");
        RitualController.EnableRitualMenu();
    }
}