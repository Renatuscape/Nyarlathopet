using System.Collections;
using UnityEngine;

public class HorrorData
{
    public Horror[] pets;
    public Horror[] masks;
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
        Repository.pets = data.pets;
        Repository.masks = data.masks;

        Report.Write(name, $"Found {Repository.pets.Length} pets.");
        Report.Write(name, $"Found {Repository.masks.Length} masks.");
        yield return null;
    }
}
