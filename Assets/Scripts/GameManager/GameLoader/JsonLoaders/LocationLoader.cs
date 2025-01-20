using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LocationData
{
    public Location[] locations;
}

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

        // Use JsonConvert instead of JsonUtility to ensure enums are correctly loaded
        LocationData data = JsonConvert.DeserializeObject<LocationData>(LocationsJSON.text);
        Repository.locations = data.locations;
        Report.Write(name, $"Found {Repository.locations.Length} locations.");
        yield return null;
    }
}