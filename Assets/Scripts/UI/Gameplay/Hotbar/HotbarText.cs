using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HotbarText : MonoBehaviour
{
    public TextMeshProUGUI displayText;

    public void UpdateDisplay()
    {
        displayText.text = GetFormattedString();
    }

    string GetFormattedString()
    {
        return "EP:" + GameplayManager.endeavourPoints + " " + DateCalculator.GetGameDate(Player.Data?.rounds ?? 0);
    }
}
