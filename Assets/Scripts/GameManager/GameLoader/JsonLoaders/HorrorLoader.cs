using System.Collections;
using UnityEngine;

public class HorrorData
{
    public Horror[] pets;
    public Horror[] nyarlathotep;
}

public class HorrorLoader : MonoBehaviour
{
    [SerializeField] private TextAsset HorrorsJSON;

    public IEnumerator LoadData()
    {
        Report.Write(name, "Loading horrors data.");

        if (HorrorsJSON == null)
        {
            Report.Write(name, "Horrors.json is missing or not assigned.");
            yield break;
        }

        HorrorData data = JsonUtility.FromJson<HorrorData>(HorrorsJSON.text);
        // RandomNameGenerator.SortNames(namesData);
        yield return null;
    }
}
