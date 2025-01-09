using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Dynamic class for giving the player text alerts and confirmation prompts. Ensure that an AlertSystem object exists in the scene so static methods can be called
public class AlertSystem : MonoBehaviour
{
    static AlertSystem instance;
    public Canvas alertCanvas;
    public TextMeshProUGUI alertMesh;
    public Button btnConfirmAction;
    public Button btnCloseAlert;

    private void Start()
    {
        alertCanvas.gameObject.SetActive(false);
        instance = this;
        btnCloseAlert.onClick.AddListener(CloseAlert);
    }

    void PrintAlert(string alertText)
    {
        alertMesh.text = alertText;
        alertCanvas.gameObject.SetActive(true);
    }

    void PrintActionPrompt(string alertText, Action action)
    {
        alertMesh.text = alertText;
        alertCanvas.gameObject.SetActive(true);

        btnConfirmAction.onClick.RemoveAllListeners();
        btnConfirmAction.onClick.AddListener(() =>
        {
            Report.Write(name, "Attempting to invoke action assigned to Confirm button: " + action.Method?.Name ?? "unnamed");
            CloseAlert();
            action.Invoke();
        });
    }

    void CloseAlert()
    {
        alertCanvas.gameObject.SetActive(false);
    }

    public static void Print(string alertText)
    {
        if (instance == null)
        {
            Report.Write("AlertSystem static", "Attempted to call AlertSystem while instance was null.");
            return;
        }
        instance.PrintAlert(alertText);
    }

    public static void Prompt(string alertText, Action action)
    {
        if (instance == null)
        {
            Report.Write("AlertSystem static", "Attempted to call AlertSystem while instance was null.");
            return;
        }
        instance.PrintActionPrompt(alertText, action);
    }
}
