using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RitualManager : MonoBehaviour
{
    public Canvas ritualCanvas;
    public HumanUiPrefab activeCultist;
    public ItemUiPrefab activeItem;

    public List<HumanUiPrefab> cultistSlots;
    public List<ItemUiPrefab> itemSlots;

    public TextMeshProUGUI ritualValues;
    [SerializeField]
    private Horror ritualState;

    private void Start()
    {
        RitualController.manager = this;
        ritualCanvas.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateDisplayedRitualValues();
        ResetRitual();
    }

    public void ResetRitual()
    {
        ritualState = new();
        activeCultist = null;
        activeItem = null;
    }

    internal void SacrificeCultist()
    {
        if (activeCultist == null ) { return; }

        RitualSacrificeHelper.SacrificeCultist(ritualState, activeCultist.human, out var report);

        AlertSystem.Force(report, () =>
        {
            // Check if any cultists have gone insane and remove them from cult
        });

        UpdateDisplayedRitualValues();
        // Add cultist prefab refresh
    }

    void UpdateDisplayedRitualValues()
    {
        ritualValues.text = $"{ritualState.intrigue:D2}\n" +
            $"{ritualState.abstraction:D2}\n" +
            $"{ritualState.strength:D2}\n" +
            $"{ritualState.magick:D2}\n" +
            $"{ritualState.rage:D2}";
    }
}

public static class ListHelper
{

}