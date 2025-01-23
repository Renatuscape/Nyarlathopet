using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Used by other classes to display text alerts and confirmation prompts. Ensure that an AlertSystem object exists in the scene so static methods can be called
public class AlertSystem : MonoBehaviour
{
    static AlertSystem instance;
    public Canvas alertCanvas;
    public TextMeshProUGUI alertMesh;
    public Button btnConfirmAction;
    public Button btnCloseAlert;

    public GameObject alertContainer;
    public GameObject choiceContainer;
    public List<ChoiceButton> choiceButtons;
    public TextMeshProUGUI choiceMesh;
    public Button btnCloseChoice;

    private void Awake()
    {
        alertCanvas.gameObject.SetActive(false);
        instance = this;
        btnCloseAlert.onClick.AddListener(CloseDisplay);
        btnCloseChoice.onClick.AddListener(CloseDisplay);
    }

    void SwitchWindow(bool isChoice = false)
    {
        choiceContainer.SetActive(isChoice);
        alertContainer.SetActive(!isChoice);
    }

    void CloseDisplay()
    {
        alertCanvas.gameObject.SetActive(false);
    }

    void PrintAlert(string alertText)
    {
        SwitchWindow();
        btnCloseAlert.gameObject.SetActive(true);
        btnConfirmAction.gameObject.SetActive(false);

        alertMesh.text = alertText;
        alertCanvas.gameObject.SetActive(true);
    }

    void PrintAlertWithPromptedAction(string alertText, Action action)
    {
        SwitchWindow();
        btnCloseAlert.gameObject.SetActive(true);
        btnConfirmAction.gameObject.SetActive(true);

        alertMesh.text = alertText;
        alertCanvas.gameObject.SetActive(true);

        btnConfirmAction.onClick.RemoveAllListeners();
        btnConfirmAction.onClick.AddListener(() =>
        {
            Report.Write(name, "Attempting to invoke action assigned to Confirm button: " + action.Method?.Name ?? "unnamed");
            CloseDisplay();
            action.Invoke();
        });
    }

    void PrintAlertWithForcedAction(string alertText, Action action)
    {
        SwitchWindow();
        btnCloseAlert.gameObject.SetActive(false);
        btnConfirmAction.gameObject.SetActive(true);

        alertMesh.text = alertText;
        alertCanvas.gameObject.SetActive(true);

        btnConfirmAction.onClick.RemoveAllListeners();
        btnConfirmAction.onClick.AddListener(() =>
        {
            Report.Write(name, "Attempting to invoke action assigned to Confirm button: " + action.Method?.Name ?? "unnamed");
            CloseDisplay();
            action.Invoke();
        });
    }
    void PrintChoiceWithCustomOptions(string alertText, List<(string text, Action action)> choices, bool isForced = true)
    {
        if (choices.Count > 5 || (!isForced && choices.Count > 4))
        {
            Report.Write(name, "Custom choices had too many options to print: " + alertText);
        }

        SwitchWindow(true);
        choiceMesh.text = alertText;
        btnCloseChoice.gameObject.SetActive(!isForced);

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (choices.Count < i)
            {
                choiceButtons[i].tmp.text = choices[i].text;
                choiceButtons[i].button.onClick.RemoveAllListeners();
                choiceButtons[i].button.onClick.AddListener(() => choices[i].action.Invoke());
                choiceButtons[i].gameObject.SetActive(true);
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    void PrintChoiceYesNo(string alertText, Action actionYes, Action actionNo)
    {
        SwitchWindow(true);
        PrintChoiceWithCustomOptions(alertText, new()
        {
            (Repository.GetText("OPTION-YES"), actionYes),
            (Repository.GetText("OPTION-NO"), actionNo)
        });
    }

    #region STATIC METHODS
    static bool CheckIfInstanceIsNull()
    {
        if (instance == null)
        {
            Report.Write("AlertSystem static", "Attempted to call AlertSystem while instance was null.");
            return true;
        }
        return false;
    }

    public static void Print(string alertText)
    {
        if (CheckIfInstanceIsNull()) { return; }
        instance.PrintAlert(alertText);
    }

    public static void Prompt(string alertText, Action action)
    {
        if (CheckIfInstanceIsNull()) { return; }
        instance.PrintAlertWithPromptedAction(alertText, action);
    }

    public static void Force(string alertText, Action action)
    {
        if (CheckIfInstanceIsNull()) { return; }
        instance.PrintAlertWithForcedAction(alertText, action);
    }

    public static void Choice(string alertText, List<(string, Action)> choices, bool isForced = true)
    {
        if (CheckIfInstanceIsNull()) { return; }
        instance.PrintChoiceWithCustomOptions(alertText, choices, isForced);
    }

    public static void YesNo(string alertText, Action actionYes, Action actionNo)
    {
        if (CheckIfInstanceIsNull()) { return; }
        instance.PrintChoiceYesNo(alertText, actionYes, actionNo);
    }
    #endregion
}
