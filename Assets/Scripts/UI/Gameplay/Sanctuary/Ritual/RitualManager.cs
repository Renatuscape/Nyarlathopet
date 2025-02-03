using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Main functionality of the ritual menu, holds references in the hierarchy and initialises the other components in the scene
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
    public TextMeshProUGUI cultistPlaceholderText;
    public TextMeshProUGUI itemPlaceholderText;

    [SerializeField]
    private CreatureStats ritualState;
    public int cultistIndex;
    public int itemIndex;
    public int sacrificesMade;

    public void Initialise()
    {
        RitualController.manager = this;
        ritualCanvas.gameObject.SetActive(false);
        var d = GameplayManager.dummyData;

        if (GameplayManager.dummyData == null)
        {
            Report.Write(name, "Dummy data was null. This component should not load before GameplayManager.");
        }

        // CULTIST PAGEINATION
        cultistPageination.btnLeft.onClick.AddListener(() =>
        {
            BtnLeft(ref cultistIndex, d.cultMembers.Count - 1, cultistSlots.Count);
            UpdateCultists();
            cultistPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(cultistIndex, d.cultMembers.Count, cultistSlots.Count);
        });
        cultistPageination.btnRight.onClick.AddListener(() =>
        {
            BtnRight(ref cultistIndex, d.cultMembers.Count - 1, cultistSlots.Count);
            UpdateCultists();
            cultistPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(cultistIndex, d.cultMembers.Count, cultistSlots.Count);
        });

        // ITEM PAGEINATION
        itemPageination.btnLeft.onClick.AddListener(() =>
        {
            BtnLeft(ref itemIndex, d.inventory.Count - 1, itemSlots.Count);
            UpdateItems();
            itemPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(itemIndex, d.inventory.Count, itemSlots.Count);
        });
        itemPageination.btnRight.onClick.AddListener(() =>
        {
            BtnRight(ref itemIndex, d.inventory.Count - 1, itemSlots.Count);
            UpdateItems();
            itemPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(itemIndex, d.inventory.Count, itemSlots.Count);
        });

        // Set up highlight functions
        cultistSlots.ForEach(slot => slot.button.onClick.AddListener(() => SelectCultist(slot)));
        itemSlots.ForEach(slot => slot.button.onClick.AddListener(() => SelectItem(slot)));
    }

    void SelectCultist(HumanUiPrefab selectedPrefab)
    {
        if (selectedPrefab == activeCultist)
        {
            selectedPrefab.Highlight(false);
            activeCultist = null;
        }
        else
        {
            selectedPrefab.Highlight(true);
            activeCultist = selectedPrefab;
        }

        cultistSlots.ForEach(slot =>
        {
            if (slot != selectedPrefab)
            {
                slot.Highlight(false);
            }
        });
    }

    void SelectItem(ItemUiPrefab selectedPrefab)
    {
        if (selectedPrefab == activeItem)
        {
            selectedPrefab.Highlight(false);
            activeItem = null;
        }
        else
        {
            selectedPrefab.Highlight(true);
            activeItem = selectedPrefab;
        }

        itemSlots.ForEach(slot =>
        {
            if (slot != selectedPrefab)
            {
                slot.Highlight(false);
            }
        });
    }

    void BtnRight(ref int index, int maxIndex, int increment)
    {
        Report.Write(name, $"Right click: index {index}, maxIndex {maxIndex}, increment {increment}");
        index += increment;
        if (index > maxIndex)
        {
            index = 0;
        }
    }

    void BtnLeft(ref int index, int maxIndex, int increment)
    {
        index -= increment;
        if (index < 0)
        {
            index = (maxIndex / increment) * increment;
        }
    }

    public void EnableRitualMenu()
    {
        Report.Write(name, "Setting up ritual menu display.");

        cultistIndex = 0;
        itemIndex = 0;
        UpdateDisplayedRitualValues();
        UpdateCultists();
        UpdateItems();
        ResetRitual();

        cultistPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(cultistIndex, GameplayManager.dummyData.cultMembers.Count, cultistSlots.Count);
        itemPageination.pageText.text = ListHelper.GetCurrentAndTotalPagesString(itemIndex, GameplayManager.dummyData.inventory.Count, itemSlots.Count);

        ritualCanvas.gameObject.SetActive(true);
    }

    public void ResetRitual()
    {
        ritualState = new();
        activeCultist = null;
        activeItem = null;
        sacrificesMade = 0;
    }

    public void OfferArtefact()
    {
        if (activeItem == null) { return; }
        sacrificesMade++;

        RitualSacrificeHelper.OfferArtefact(ritualState, activeItem.item, out var report);

        AlertSystem.Force(report, () =>
        {
            UpdateItems();
        });

        UpdateDisplayedRitualValues();
    }

    public void SacrificeCultist()
    {
        if (activeCultist == null) { return; }
        sacrificesMade++;

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
        RitualController.RemoveInstaneCultists(out var report);

        if (!string.IsNullOrEmpty(report))
        {
            AlertSystem.Print(report);
        }
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
        ListHelper.DisplayHumansFromIndex(cultistIndex, GameplayManager.dummyData.cultMembers, cultistSlots, out var isListEmpty);
        activeCultist = null;
        cultistSlots.ForEach(slot => slot.Highlight(false));

        if (isListEmpty)
        {
            cultistPageination.gameObject.SetActive(false);
            cultistPlaceholderText.gameObject.SetActive(true);
        }
        else if (GameplayManager.dummyData.cultMembers.Count <= cultistSlots.Count)
        {
            cultistPageination.gameObject.SetActive(false);
            cultistPlaceholderText.gameObject.SetActive(false);
        }
        else
        {
            cultistPageination.gameObject.SetActive(true);
            cultistPlaceholderText.gameObject.SetActive(false);
        }
    }

    void UpdateItems()
    {
        ListHelper.DisplayArtefactsFromIndex(itemIndex, GameplayManager.dummyData.inventory, itemSlots, out var isListEmpty);
        activeItem = null;
        itemSlots.ForEach(slot => slot.Highlight(false));

        if (isListEmpty)
        {
            itemPageination.gameObject.SetActive(false);
            itemPlaceholderText.gameObject.SetActive(true);
        }
        else if (GameplayManager.dummyData.inventory.Count <= itemSlots.Count)
        {
            itemPageination.gameObject.SetActive(false);
            itemPlaceholderText.gameObject.SetActive(false);
        }
        else
        {
            itemPageination.gameObject.SetActive(true);
            itemPlaceholderText.gameObject.SetActive(false);
        }
    }

    public void CommenceRitual()
    {
        RitualSummonHelper.CommenceRitual(ritualState);
        ritualCanvas.gameObject.SetActive(false);
        ResetRitual();
    }
}
