using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiPrefab: MonoBehaviour
{
    public Item item;
    public Image image;
    public Button button;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemStats;

    public void SetDisplay(Item item)
    {
        this.item = item;
        UpdateDisplayInfo();
    }

    void UpdateDisplayInfo()
    {
        itemName.text = item.name;
        itemStats.text = $"LOR:{item.lore:D2} STR:{item.strength:D2} MGC:{item.magick:D2}";
    }
}