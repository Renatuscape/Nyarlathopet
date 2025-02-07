using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopOutController : MonoBehaviour
{
    public Button btnOpenStats;
    public Button btnStatPanel;
    public TextMeshProUGUI statValues;
    public TextMeshProUGUI statLabels;

    private void Awake()
    {
        CloseStatPanel();
        btnOpenStats.onClick.AddListener(() => OpenStatPanel());
        btnStatPanel.onClick.AddListener(() => CloseStatPanel());


    }
    public void UpdateStatDisplay()
    {
        var d = GameplayManager.dummyData;
        var l = d.cultLeader;
        statLabels.text =
            $"{Tags.Get("Ldr").ToUpper()}" +
            $"\n{Tags.Get("SAN")}:" +
            $"\n{Tags.Get("OCL")}:" +
            $"\n{Tags.Get("LOR")}:" +
            $"\n{Tags.Get("STR")}:" +
            $"\n" +
            $"\n{Tags.Get("Cult").ToUpper()}" +
            $"\n{Tags.Get("NWK")}:" +
            $"\n{Tags.Get("NTR")}:" +
            $"\n{Tags.Get("MBS")}:" +
            $"\n{Tags.Get("FNS")}:";

        statValues.text = 
            $"\n{l.sanity:D2}" +
            $"\n{l.occultism:D2}" +
            $"\n{l.lore:D2}" +
            $"\n{l.strength:D2}" +
            $"\n\n" +
            $"\n{d.network:D2}" +
            $"\n{d.notoriety:D2}" +
            $"\n{d.cultMembers.Count:D2}" +
            $"\n{d.funds:D5}";
    }

    public void OpenStatPanel()
    {
        UpdateStatDisplay();
        btnStatPanel.gameObject.SetActive(true);
    }

    public void CloseStatPanel()
    {
        btnStatPanel.gameObject.SetActive(false);
    }
}
