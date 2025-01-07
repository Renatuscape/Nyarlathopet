using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public static class SaveManager
{
    private static readonly string relativePath = "Data/SaveData.json";

    public static async Task SaveDataAsync()
    {
        string saveFilePath = Path.Combine(Application.dataPath, relativePath);

        if (!File.Exists(saveFilePath))
        {
            Report.Write("SaveManager", $"File not found at path: {saveFilePath}");
            return;
        }

        if (Player.Data == null)
        {
            Report.Write("SaveManager", "Player data is null. Cannot save.");
            return;
        }

        string jsonData = JsonUtility.ToJson(Player.Data, true); // Convert Player.Data to JSON string

        try
        {
            // Asynchronously write the JSON to the file
            using (StreamWriter writer = new StreamWriter(saveFilePath, false))
            {
                await writer.WriteAsync(jsonData);
            }
            Report.Write("SaveManager", $"SaveData written to {saveFilePath}");
        }
        catch (IOException e)
        {
            Report.Write("SaveManager", $"Error saving data to {saveFilePath}: {e.Message}");
        }
    }
}