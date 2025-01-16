using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        LocationData data = JsonUtility.FromJson<LocationData>(LocationsJSON.text);
        Repository.locations = data.locations;
        Report.Write(name, $"Found {Repository.locations.Length} locations.");
        yield return null;
    }
}
