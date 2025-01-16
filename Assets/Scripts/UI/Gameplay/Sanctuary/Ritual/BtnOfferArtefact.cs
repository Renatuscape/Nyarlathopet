using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOfferArtefact : MonoBehaviour
{
    public void BtnOffer()
    {
        if (RitualController.CheckIfItemSelected(out var artefact))
        {
            if (artefact.type != ItemType.Artefact)
            {
                AlertSystem.Print("Only artefacts may be offered during the ritual.");
            }
            else
            {
                AlertSystem.Prompt($"Offer {artefact.name} to Nyarlathotep?\nThe artefact will be lost for good.", () =>
                {
                    RitualController.OfferArtefact();
                });
            }
        }
        else if (GameplayManager.dummyData.inventory.Count > 0)
        {
            AlertSystem.Print("Choose an artefact to offer.");
        }
        else
        {
            AlertSystem.Print("You have no artefacts to offer.");
        }
    }
}
