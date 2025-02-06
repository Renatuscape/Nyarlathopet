using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnPromptMainMenu : MonoBehaviour
{
    public void BtnPromptMain()
    {
        AlertSystem.Prompt(Text.Get("MENU-MAINP"),
            () =>
            {
                SceneManager.LoadScene("MainMenu");
            });
    }

    public void BtnPromptMenuOptions()
    {
        AlertSystem.Choice(Text.Get("MENU-OPTIT"), new()
        {
            (Text.Get("OPTION-LANG"), () => PromptChooseLanguage()),
            (Text.Get("OPTION-MMENU"), () => BtnPromptMain()),
        }, false);
    }

    public void BtnPromptSettings()
    {
        AlertSystem.Choice(Text.Get("MENU-OPTIT"), new()
        {
            (Text.Get("OPTION-LANG"), () => PromptChooseLanguage()),
        }, false);
    }

    void PromptChooseLanguage()
    {
        List<(string, Action)> options = new();
        var languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToArray();

        foreach (var language in languages)
        {
            string label = language != GlobalSettings.language ? language.ToString() : $"[{language.ToString()}]";
            options.Add((language.ToString(), () => ChooseLanguage(language)));
        }

        AlertSystem.Choice(Text.Get("SETT-LANGP"), options, false);
    }

    void ChooseLanguage(Language language)
    {
        if (language != GlobalSettings.language)
        {
            GlobalSettings.language = language;
            AlertSystem.Print($"{Text.Get("SETT-LANGCA")} {language.ToString()}. {Text.Get("SETT-LANGCB")}");
        }
        else
        {
            AlertSystem.Print($"{language.ToString()} {Text.Get("SETT-LANGERR")}");
        }
    }
}
