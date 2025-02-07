using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUiLabelmaker : MonoBehaviour
{
    public TextMeshProUGUI txtHBSanctuary;
    public TextMeshProUGUI txtHBLedger;
    public TextMeshProUGUI txtHBMap;

    public TextMeshProUGUI txtSancFeed;
    public TextMeshProUGUI txtSancWorship;
    public TextMeshProUGUI txtSancCommune;
    public TextMeshProUGUI txtSancBeginRitual;

    public TextMeshProUGUI txtMapSeekArtefacts;
    public TextMeshProUGUI txtMapRecruitMembers;
    public TextMeshProUGUI txtMapThrwartEnemies;

    void Awake()
    {
        txtHBSanctuary.text = Text.Get("BtnHB-SANC");
        txtHBLedger.text = Text.Get("BtnHB-LEDG");
        txtHBMap.text = Text.Get("BtnHB-MAPM");

        txtSancFeed.text = Text.Get("BtnSNC-FEED");
        txtSancWorship.text = Text.Get("BtnSNC-WRSH");
        txtSancCommune.text = Text.Get("BtnSNC-COMM");
        txtSancBeginRitual.text = Text.Get("BtnSNC-BRIT");

        txtMapSeekArtefacts.text = Text.Get("BtnMAP-SARF");
        txtMapRecruitMembers.text = Text.Get("BtnMAP-RECM");
        txtMapThrwartEnemies.text = Text.Get("BtnMAP-TWRT");
    }
}
