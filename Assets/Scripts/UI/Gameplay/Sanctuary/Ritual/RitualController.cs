using System;

public static class RitualController
{
    public static RitualManager manager;

    public static void EnableRitualMenu()
    {
        manager.EnableRitualMenu();
    }

    public static void AbortRitual()
    {
        if (manager.sacrificesMade > 0)
        {
            Report.Write("RitualController", manager.sacrificesMade + " sacrifices were already made. Prompt before exiting.");
            AlertSystem.Prompt("ABANDON PROGRESS AND ABORT RITUAL?\nAll amassed power will be lost. Any sacrifices made will be for naught.", () =>
            {
                manager.ritualCanvas.gameObject.SetActive(false);
                manager.ResetRitual();
            });
        }
        else
        {
            Report.Write("RitualController", manager.sacrificesMade + " sacrifices made. Exiting without prompt.");
            manager.ritualCanvas.gameObject.SetActive(false);
            manager.ResetRitual();
        }
    }

    public static void SacrificeArtefact(Item item)
    {
        GameplayManager.dummyData.inventory.Remove(item);
    }

    public static void SacrificeCultist()
    {
        manager.SacrificeCultist();
    }

    internal static bool CheckIfCultistSelected(out Human selectedCultist)
    {
        selectedCultist = manager.activeCultist?.human;

        return manager.activeCultist != null;
    }
}
