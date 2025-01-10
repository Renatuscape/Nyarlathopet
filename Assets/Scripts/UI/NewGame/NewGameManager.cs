using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameManager : MonoBehaviour
{
    public PlayerData playerData;
    public TMP_InputField cultName;
    public TMP_InputField leaderName;
    public TextMeshProUGUI tagStats;

    void Start()
    {
        Initialise();
    }

    void Initialise()
    {
        playerData = Player.Data;
        Player.CreateNewGameData();
        UpdateDisplayFromData();
    }

    void UpdateDisplayFromData()
    { 
        cultName.text = Player.Data.cultName;
        leaderName.text = Player.Data.cultLeader?.name ?? "";
        tagStats.text = $"{Player.Data.cultLeader?.magick ?? 0:D2} magick power\r\n\r\n" +
                        $"{Player.Data.cultLeader?.strength ?? 0:D2} physical strength\r\n\r\n" +
                        $"{Player.Data.cultLeader?.lore ?? 0:D2} knowledge of arcane lore";
    }

    bool ValidateEntries()
    {
        if (string.IsNullOrEmpty(cultName.text) || cultName.text.Length < 2)
        {
            Report.Write(name, "Name of cult was null or too short.");
            return false;
        }
        else if (string.IsNullOrEmpty(leaderName.text) || leaderName.text.Length < 2)
        {
            Report.Write(name, "Name of cult leader was null or too short.");
            return false;
        }
        return true;
    }

    IEnumerator SaveAndGoToMenu()
    {
        yield return SaveManager.SaveDataAsync();
        Report.Write(name, "New game created and saved. Starting game.");
        SceneManager.LoadScene("Gameplay");
    }

    public void BtnFinalise()
    {
        if (ValidateEntries())
        {
            AlertSystem.Prompt("Start a new game and erase old save data?", () =>
            {
                Player.Data.cultLeader.name = leaderName.text;
                Player.Data.cultName = cultName.text;

                StartCoroutine(SaveAndGoToMenu());
            });
        }
        else
        {
            AlertSystem.Print("The name of your cult and your leader must be at least two characters in length.");
        }
    }

    public void BtnRandomise()
    {
        Player.CreateNewGameData();
        playerData = Player.Data;
        UpdateDisplayFromData();
    }
}
