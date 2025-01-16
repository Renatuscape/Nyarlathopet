using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LedgerStatus : MonoBehaviour
{
    public TextMeshProUGUI cultStats;
    public TextMeshProUGUI leaderName;
    public TextMeshProUGUI leaderOrigin;
    public TextMeshProUGUI leaderStats;

    private void OnEnable()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        PlayerData d = GameplayManager.dummyData ?? new();

        cultStats.text = $"{d.level:D2}\n" +
            $"{d.bookOfMasks.Count:D2}\n" +
            $"{d.cultMembers.Count:D2}\n" +
            $"{d.network:D2}\n" +
            $"{d.notoriety:D2}\n" +
            $"{d.rounds:D2}\n";

        leaderName.text = d.cultLeader?.name ?? "Leaderless";
        leaderOrigin.text = d.cultLeader?.origin ?? "";

        leaderStats.text = d.cultLeader != null ? $"{d.cultLeader.sanity:D2}\n" +
            $"{d.cultLeader.lore:D2}\n" +
            $"{d.cultLeader.magick:D2}\n" +
            $"{d.cultLeader.strength:D2}\n"
            : "00\n00\n00\n00";
    }
}
