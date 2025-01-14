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
    public PageinationPrefab cultistPageination;
    public PageinationPrefab itemPageination;

    public TextMeshProUGUI ritualValues;
    [SerializeField]
    private Horror ritualState;
    int cultistIndex;
    int itemIndex;

    private void Start()
    {
        RitualController.manager = this;
        ritualCanvas.gameObject.SetActive(false);
    }

    public void EnableRitualMenu()
    {
        Report.Write(name, "Setting up ritual menu display.");
        UpdateDisplayedRitualValues();
        UpdateCultists();
        UpdateItems();
        ResetRitual();
        ritualCanvas.gameObject.SetActive(true);
    }

    public void ResetRitual()
    {
        ritualState = new();
        activeCultist = null;
        activeItem = null;
    }

    public void SacrificeItem()
    {
        if (activeItem == null) { return; }

        RitualSacrificeHelper.SacrificeItem(ritualState, activeItem.item, out var report);

        AlertSystem.Force(report, () =>
        {
            UpdateItems();
        });

        UpdateDisplayedRitualValues();
    }

    public void SacrificeCultist()
    {
        if (activeCultist == null) { return; }

        RitualSacrificeHelper.SacrificeCultist(ritualState, activeCultist.human, out var report);

        AlertSystem.Force(report, () =>
        {
            CheckForInsaneCultists();
            UpdateCultists();
        });

        UpdateDisplayedRitualValues();
    }

    void CheckForInsaneCultists()
    {
        List<Human> insaneCultists = new();
        GameplayManager.dummyData.cultMembers.ForEach(c =>
        {
            if (c.sanity <= 0) { insaneCultists.Add(c); }
        });

        if (insaneCultists.Count == 1)
        {
            AlertSystem.Print(insaneCultists[0] + " has gone insane.");
        }
        else if (insaneCultists.Count > 1)
        {
            string alert = "Several cultists have gone insane. You lost ";
            insaneCultists.ForEach(m => alert += m.name + ", ");

            alert = alert.Trim(' ').Trim(',');

            AlertSystem.Print(alert);
        }

        insaneCultists.ForEach(c => GameplayManager.dummyData.cultMembers.Remove(c));
    }

    void UpdateDisplayedRitualValues()
    {
        ritualValues.text = $"{ritualState.intrigue:D2}\n" +
            $"{ritualState.abstraction:D2}\n" +
            $"{ritualState.strength:D2}\n" +
            $"{ritualState.magick:D2}\n" +
            $"{ritualState.rage:D2}";
    }

    void UpdateCultists()
    {
        Report.Write(name, "Updating displayed cultists.");
        ListHelper.DisplayHumansFromIndex(cultistIndex, GameplayManager.dummyData.cultMembers, cultistSlots);
    }

    void UpdateItems()
    {
        ListHelper.DisplayItemsFromIndex(cultistIndex, GameplayManager.dummyData?.inventory ?? new(), itemSlots);
    }
}
