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
        manager.ritualCanvas.gameObject.SetActive(false);
        manager.ResetRitual();
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
