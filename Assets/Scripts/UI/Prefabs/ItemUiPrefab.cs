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
        itemStats.text = $"{Tags.Get("LOR")}:{item.lore:D2} {Tags.Get("STR")}:{item.strength:D2} {Tags.Get("OCL")}:{item.occultism:D2}";
    }
}