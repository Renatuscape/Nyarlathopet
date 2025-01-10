using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour
{
    public HotbarText hotbarText;
    public Button btnSanctuary;
    public Button btnLedger;
    public Button btnMap;

    public Canvas canvasSanctuary;
    public Canvas canvasLedger;
    public Canvas canvasMap;

    int currentView = 0;

    void Start()
    {
        StartCoroutine(Initialise());
    }

    IEnumerator Initialise()
    {
        while (GameplayManager.instance == null)
        {
            yield return new WaitForFixedUpdate();
        }

        ToggleView(currentView);

        btnSanctuary.onClick.AddListener(() => ToggleView(0));
        btnLedger.onClick.AddListener(() => ToggleView(1));
        btnMap.onClick.AddListener(() => ToggleView(2));

        hotbarText.UpdateDisplay();
    }

    public void RefreshForNewRound()
    {
        ToggleView(-1);

        hotbarText.UpdateDisplay();

        ToggleView(0);
    }

    public void RefreshAfterEndeavour()
    {
        hotbarText.UpdateDisplay();
    }

    void ToggleView(int view)
    {
        canvasSanctuary.gameObject.SetActive(view == 0);
        btnSanctuary.interactable = view != 0;

        canvasLedger.gameObject.SetActive(view == 1);
        btnLedger.interactable = view != 1;

        canvasMap.gameObject.SetActive(view == 2);
        btnMap.interactable = view != 2;
    }
}
