using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationLoader : MonoBehaviour
{
    [SerializeField] private TextAsset LocationsJSON;

    public IEnumerator LoadData()
    {
        Report.Write(name, "Loading locations data.");

        if (LocationsJSON == null)
        {
            Report.Write(name, "Locations.json is missing or not assigned.");
            yield break;
        }

        HorrorData data = JsonUtility.FromJson<HorrorData>(LocationsJSON.text);
        // RandomNameGenerator.SortNames(namesData);
        yield return null;
    }
}
