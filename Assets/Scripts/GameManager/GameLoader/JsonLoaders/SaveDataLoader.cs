using UnityEngine;
using System.Collections;

public class SaveDataWrapper
{
    public PlayerData playerData;
}

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

        SaveDataWrapper saveData = JsonUtility.FromJson<SaveDataWrapper>(saveDataJSON.text);
        yield return null;
    }
}

