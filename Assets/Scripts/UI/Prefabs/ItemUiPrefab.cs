using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUiPrefab: MonoBehaviour
{
    public Item item;
    public Image image;
    public Button button;
    public GameObject highlight;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemStats;

    public void SetDisplay(Item item)
    {
        this.item = item;
        UpdateDisplayInfo();
    }

    public void Highlight(bool isEnabled)
    {
        highlight.SetActive(isEnabled);
    }

    void UpdateDisplayInfo()
    {
        itemName.text = item.name;
        itemStats.text = $"LOR:{item.lore:D2} STR:{item.strength:D2} MGC:{item.magick:D2}";
    }
}