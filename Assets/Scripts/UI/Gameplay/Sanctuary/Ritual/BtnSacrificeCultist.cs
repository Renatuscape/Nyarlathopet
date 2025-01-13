using UnityEngine;

public class BtnSacrificeCultist : MonoBehaviour
{
    public void BtnSacrifice()
    {
        if (RitualController.CheckIfCultistSelected(out var cultist))
        {
            AlertSystem.Prompt($"Sacrifice {cultist.name} to Nyarlathotep?\nThis action cannot be undone, and may drive witnesses to madness.", () =>
            {
                RitualController.SacrificeCultist();
            });
        }
        else
        {
            AlertSystem.Print("Choose whom you wish to sacrifice first.");
        }
    }
}