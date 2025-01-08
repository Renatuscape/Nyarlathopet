using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI titleCultName;
    public TextMeshProUGUI tagLevel;
    public TextMeshProUGUI tagMasks;
    public TextMeshProUGUI tagMembers;
    public TextMeshProUGUI tagAge;
    public TextMeshProUGUI tagCultFunds;
    public TextMeshProUGUI titleLeaderName;
    public TextMeshProUGUI tagMagick;
    public TextMeshProUGUI tagStrength;
    public TextMeshProUGUI tagLore;
    public TextMeshProUGUI tagFunds;
    public TextMeshProUGUI tagSanity;

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
        tagLevel.text = "Level " + Player.Data.level + " cult";
        tagMasks.text = Player.Data.discoveredCreatures.Count + " masks discovered";
        tagMembers.text = Player.Data.cultMembers.Count + " members";
        tagAge.text = Player.Data.rounds + " months old";
        tagCultFunds.text = Player.Data.funds + " vault funds";

        // Leader Statistics
        titleLeaderName.text = Player.Data.cultLeader.name;
        tagMagick.text = Player.Data.cultLeader.magick + " magick power";
        tagStrength.text = Player.Data.cultLeader.strength + " physical strength";
        tagLore.text = Player.Data.cultLeader.lore + " arcane lore";
        tagFunds.text = Player.Data.cultLeader.funds + " personal funds";
        tagSanity.text = Player.Data.cultLeader.sanity + " remaining sanity";
    }
}
