using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class TextData
{
    public Language language;
    public List<string[]> entries;
}

public class TextLoader : MonoBehaviour
{
    [SerializeField] private List<TextAsset> textJsonList;

    public IEnumerator LoadData()
    {
        Report.Write(name, "Loading locations data.");
        if (textJsonList == null)
        {
            Report.Write(name, "Locations.json is missing or not assigned.");
            yield break;
        }

        var textFile = textJsonList.FirstOrDefault(u => u.name.ToLower().Contains(GlobalSettings.language.ToString().ToLower()));
        if (textFile == null)
        {
            Report.Write(name, "Could not find any text file for " + GlobalSettings.language);
            yield break;
        }

        // Use JsonConvert instead of JsonUtility to ensure enums are correctly loaded
        TextData data = JsonConvert.DeserializeObject<TextData>(textFile.text);
        Repository.text = data;
        Report.Write(name, $"Found {Repository.text.entries.Count} text entries in {Repository.text.language}.");
        yield return null;
    }
}
