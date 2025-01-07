using UnityEngine;
using System.Collections;

// Loads the player's save data from JSON
// Currently there is only one save data file, but more could be added in the wrapper
public class SaveDataLoader : MonoBehaviour
{
    [SerializeField] private TextAsset saveDataJSON;

    public IEnumerator LoadData()
    {
        if (saveDataJSON == null)
        {
            Report.Write(name, "SaveData.json is missing or not assigned.");
            yield break;
        }

        PlayerData saveData = JsonUtility.FromJson<PlayerData>(saveDataJSON.text);

        if (saveData == null)
        {
            Report.Write(name, "The loaded data returned null. Creating new.");
            yield return Player.CreateNewGameDataAsync();
        }
        else
        {
            Player.SetPlayerData(saveData);
            Report.Write(name, "Correctly loaded data: " + saveData.ToString());
        }

        yield return null;
    }
}

