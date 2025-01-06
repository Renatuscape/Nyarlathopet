using System;
using UnityEngine;

public class HumanNamesData
{
    public string firstNames;
    public string lastNames;
}

// Loads in first and last names for humans at the start of the game, and holds them for future use
public class HumanNameLoader : MonoBehaviour
{
    [SerializeField] private TextAsset HumanNamesJSON;

    void Start()
    {
        LoadData(HumanNamesJSON);
    }

    void LoadData(TextAsset jsonFile)
    {
        if (jsonFile == null)
        {
            Debug.LogError("JSON file is missing or not assigned.");
            return;
        }

        // Parse JSON content into the NamesData object
        HumanNamesData namesData = JsonUtility.FromJson<HumanNamesData>(jsonFile.text);
        HumanNameGenerator.SortNames(namesData);
    }
}
