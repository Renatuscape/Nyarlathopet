using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI titleCultName;
    public TextMeshProUGUI titleLeaderName;
    public TextMeshProUGUI txtBtnContinue;
    public TextMeshProUGUI txtBtnNewGame;
    public TextMeshProUGUI txtBtnSettings;

    void Awake()
    {
        UpdateDisplayData();

        txtBtnContinue.text = Text.Get("BtnMM-CONT");
        txtBtnNewGame.text = Text.Get("BtnMM-NEWG");
        txtBtnSettings.text = Text.Get("BtnMM-SETT");
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
