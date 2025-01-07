using UnityEngine;
using System.Collections;

public class HumanNamesData
{
    public string firstNames;
    public string lastNames;
}

// Loads in first and last names for humans at the start of the game, and holds them for future use
public class HumanNameLoader : MonoBehaviour
{
    [SerializeField] private TextAsset HumanNamesJSON;

    public IEnumerator LoadData()
    {
        if (HumanNamesJSON == null)
        {
            Report.Write(name, "HumanNames.json is missing or not assigned.");
            yield break;
        }

        HumanNamesData namesData = JsonUtility.FromJson<HumanNamesData>(HumanNamesJSON.text);
        HumanNameGenerator.SortNames(namesData);
        yield return null;
    }
}
