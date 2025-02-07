using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanUiPrefab : MonoBehaviour
{
    public Human human;
    public Image image;
    public Button button;
    public GameObject highlight;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI originText;
    public TextMeshProUGUI fundsText;
    public TextMeshProUGUI statText;

    public void SetDisplay(Human human)
    {
        this.human = human;
        UpdateDisplayInfo();
    }

    public void Highlight(bool isEnabled)
    {
        highlight.SetActive(isEnabled);
    }

    void UpdateDisplayInfo()
    {
        nameText.text = human.name;
        originText.text = human.origin;
        fundsText.text = $"{Tags.Get("FNS")}:{human.funds:D2}";
        statText.text = $"{Tags.Get("SAN")}:{human.sanity:D2} {Tags.Get("LOR")}:{human.lore:D2}\n" +
                        $"{Tags.Get("STR")}:{human.strength:D2} {Tags.Get("OCL")}:{human.occultism:D2}";
    }
}
