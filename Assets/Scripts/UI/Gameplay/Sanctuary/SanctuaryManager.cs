using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SanctuaryManager : MonoBehaviour
{
    public static SanctuaryManager instance;
    public TextMeshProUGUI petName;
    public TextMeshProUGUI petStats;

    public Button btnFeed;
    public Button btnWorship;
    public Button btnCommune;
    public Button btnBeginRitual;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
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
        petStats.text = pet.GetStatPrintOut(false, true);
        petName.gameObject.SetActive(true);
    }

    void SetupWithoutPet()
    {
        petName.gameObject.SetActive(false);
        
        petStats.text = Repository.GetText("SAN-READY");

        btnFeed.gameObject.SetActive(false);
        btnWorship.gameObject.SetActive(false);
        btnCommune.gameObject.SetActive(false);
        btnBeginRitual.gameObject.SetActive(true);
    }
}
