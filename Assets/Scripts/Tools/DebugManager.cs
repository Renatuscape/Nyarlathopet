using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
public static class DebugManager
{
    private static readonly List<string> debugLogs = new List<string>();
    private static readonly string sessionStart = "Debug report session " + DateTime.Now;

    static DebugManager()
    {
        debugLogs.Add(sessionStart);
    }

    public static void LogDebugStatement(string name, string report)
    {
        var formattedText = $"\n{DateTime.Now} {name}: {report}";
        debugLogs.Add(formattedText);
        Debug.Log(formattedText);
    }

    public static void WriteDebugSessionLog(bool overwrite)
    {
        string filePath = Application.streamingAssetsPath + "/DebugSessionLog.txt";
        try
        {
            if (overwrite)
                File.WriteAllLines(filePath, debugLogs);
            else
                File.AppendAllLines(filePath, debugLogs);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write debug log: {e.Message}");
        }
    }
}