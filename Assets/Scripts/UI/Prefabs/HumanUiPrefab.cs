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
        fundsText.text = $"Funds:{human.funds:D2}";
        statText.text = $"SAN:{human.sanity:D2} LOR:{human.lore:D2}\n" +
                        $"STR:{human.strength:D2} MGC:{human.magick:D2}";
    }
}
