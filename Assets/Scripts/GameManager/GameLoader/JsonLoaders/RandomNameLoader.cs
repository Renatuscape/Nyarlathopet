using UnityEngine;
using System.Collections;

public class RandomNamesData
{
    public string firstNames;
    public string lastNames;
    public string cultPrefixes;
    public string cultDefiniteSuffixes;
    public string cultIndefiniteSuffixes;
    public string cultNames;
    public string itemPrefixMagick;
    public string itemPrefixLore;
    public string itemPrefixStrength;
    public string itemArtefactTypes;
    public string itemSuffixes;
    public string itemTomeTypes;
    public string itemPrayerTypes;
}

// Loads in first and last names for humans at the start of the game, and holds them for future use
public class RandomNameLoader : MonoBehaviour
{
    [SerializeField] private TextAsset RandomNamesJSON;

    public IEnumerator LoadData()
    {
        Report.Write(name, "Loading random name data.");

        if (RandomNamesJSON == null)
        {
            Report.Write(name, "RandomNames.json is missing or not assigned.");
            yield break;
        }

        RandomNamesData namesData = JsonUtility.FromJson<RandomNamesData>(RandomNamesJSON.text);
        RandomNameGenerator.SortNames(namesData);
        yield return null;
    }
}
