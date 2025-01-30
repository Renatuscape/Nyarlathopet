// Class works as a layer between buttons and RitualManager, allowing the manager to be easily called from elsewhere in the scene
using System.Collections.Generic;

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
            AlertSystem.Prompt(Text.Get("RIT-ABORT"), () =>
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

    public static void OfferArtefact()
    {
        manager.OfferArtefact();
    }

    public static void SacrificeCultist()
    {
        manager.SacrificeCultist();
    }

    public static bool CheckIfCultistSelected(out Human selectedCultist)
    {
        selectedCultist = manager.activeCultist?.human;

        return manager.activeCultist != null;
    }

    public static bool CheckIfItemSelected(out Item selectedArtefact)
    {
        selectedArtefact = manager.activeItem?.item;

        return manager.activeItem != null;
    }

    public static void CommenceRitual()
    {
        manager.CommenceRitual();
    }

    public static void RemoveInstaneCultists(out string report)
    {
        List<Human> insaneCultists = new();

        GameplayManager.dummyData.cultMembers.ForEach(c =>
        {
            if (c.sanity <= 0) { insaneCultists.Add(c); }
        });

        if (insaneCultists.Count == 1)
        {
            report = insaneCultists[0].name + " has gone insane.";
        }
        else if (insaneCultists.Count > 1)
        {
            string alert = "Several cultists have gone insane. You lost ";
            insaneCultists.ForEach(m => alert += m.name + ", ");

            report = alert.Trim(' ').Trim(',');
        }
        else
        {
            report = "";
        }

        insaneCultists.ForEach(c => GameplayManager.dummyData.cultMembers.Remove(c));
    }
}
