using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanctuaryManager : MonoBehaviour
{
    public TextMeshProUGUI petName;
    public TextMeshProUGUI petStats;

    public Button btnFeed;
    public Button btnWorship;
    public Button btnCommune;
    public Button btnBeginRitual;

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(GameplayManager.dummyData?.currentPet?.name))
        {
            SetupWithPet(GameplayManager.dummyData.currentPet);
        }
        else
        {
            SetupWithoutPet();
        }
    }

    void SetupWithPet(Horror pet)
    {
        btnFeed.gameObject.SetActive(true);
        btnWorship.gameObject.SetActive(true);
        btnCommune.gameObject.SetActive(true);
        btnBeginRitual.gameObject.SetActive(false);

        petName.text = pet.name;
        petStats.text = $"{pet.rage:D2} Rage\n\n\n" +
                        $"{pet.intrigue:D2} Intrigue\n\n" +
                        $"{pet.abstraction:D2} Abstraction\n\n" +
                        $"{pet.strength:D2} Strength\n\n" +
                        $"{pet.magick:D2} Magick\n\n";
        petName.gameObject.SetActive(true);
    }

    void SetupWithoutPet()
    {
        petName.gameObject.SetActive(false);
        
        petStats.text = "The sanctuary is ready for a summoning.";

        btnFeed.gameObject.SetActive(false);
        btnWorship.gameObject.SetActive(false);
        btnCommune.gameObject.SetActive(false);
        btnBeginRitual.gameObject.SetActive(true);
    }
}
