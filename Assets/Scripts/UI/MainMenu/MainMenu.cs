using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI titleCultName;
    public TextMeshProUGUI titleLeaderName;

    void Start()
    {
        UpdateDisplayData();
    }

    void UpdateDisplayData()
    {
        playerData = Player.Data;

        if (Player.Data == null)
        {
            Report.Write(name, "ERROR! Player data was null. Could not update player data display.");
            return;
        }

        // Cult Statistics
        titleCultName.text = Player.Data.cultName;

        // Leader Statistics
        titleLeaderName.text = Player.Data.cultLeader.name;
    }
}
