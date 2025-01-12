using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LedgerVault : MonoBehaviour
{
    public List<ItemUiPrefab> itemPrefabs;
    public TextMeshProUGUI placeholderText;
    public TextMeshProUGUI pageNumber;
    public GameObject pageinator;
    public Button btnLeft;
    public Button btnRight;

    public List<Item> items;
    public int itemIndex;

    private void Start()
    {
        itemIndex = 0;

        btnLeft.onClick.AddListener(() => BtnLeft());
        btnRight.onClick.AddListener(() => BtnRight());
    }

    private void OnEnable()
    {
        items = GameplayManager.dummyData?.inventory ?? new List<Item>();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        UpdatePageNumber();

        if (items.Count == 0)
        {
            pageinator.gameObject.SetActive(false);
            placeholderText.gameObject.SetActive(true);
            itemPrefabs.ForEach(slot => slot.gameObject.SetActive(false));
        }
        else
        {
            placeholderText.gameObject.SetActive(false);

            if (items.Count < itemPrefabs.Count)
            {
                PrintWithoutPageinator();
            }
            else
            {
                PrintWithPageinator();
            }
        }
    }

    void PrintWithoutPageinator()
    {
        pageinator.gameObject.SetActive(false);
        PrintPage(0);
    }

    void PrintWithPageinator()
    {
        pageinator.gameObject.SetActive(true);
        PrintPage(itemIndex < items.Count ? itemIndex : 0);
    }

    void PrintPage(int pageStartIndex)
    {
        for (int slotIndex = 0; slotIndex < itemPrefabs.Count; slotIndex++)
        {
            int currentItemIndex = pageStartIndex + slotIndex;
            if (currentItemIndex < items.Count)
            {
                itemPrefabs[slotIndex].SetDisplay(items[currentItemIndex]);
                itemPrefabs[slotIndex].gameObject.SetActive(true);
            }
            else
            {
                itemPrefabs[slotIndex].gameObject.SetActive(false);
            }
        }
    }

    void BtnLeft()
    {
        itemIndex -= 4;

        if (itemIndex < 0)
        {
            itemIndex = ((items.Count - 1) / itemPrefabs.Count) * itemPrefabs.Count;
        }

        UpdatePageNumber();
        PrintPage(itemIndex);
    }

    void BtnRight()
    {
        itemIndex += itemPrefabs.Count;

        if (itemIndex >= items.Count)
        {
            itemIndex = 0;
        }

        UpdatePageNumber();
        PrintPage(itemIndex);
    }

    void UpdatePageNumber()
    {
        int currentPage = (itemIndex / itemPrefabs.Count) + 1;
        int totalPages = (items.Count + itemPrefabs.Count - 1) / itemPrefabs.Count;
        pageNumber.text = currentPage + "/" + totalPages;
    }
}
