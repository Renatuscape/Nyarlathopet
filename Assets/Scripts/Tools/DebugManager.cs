using System;
using UnityEngine;
using System.IO;
public static class DebugManager
{
    // Allows tracking of debug statements and report writing
    public static string debugReport = "Debug report session " + DateTime.Now;
    public static void LogDebugStatement(string name, string report)
    {
        debugReport += $"\n{DateTime.Now} {name}: {report}";
        Debug.Log(report);
    }

    public static void WriteDebugSessionLog(bool overwrite)
    {
        string filePath = Application.streamingAssetsPath + "/DebugSessionLog.txt";

        if (overwrite)
        {
            OverwriteFileWithText(filePath, debugReport);
        }
        else
        {
            AppendTextToFile(filePath, debugReport);
        }
    }

    static void OverwriteFileWithText(string filePath, string textToWrite)
    {
        // Overwrite the existing file with new text
        File.WriteAllText(filePath, textToWrite);
    }

    static void AppendTextToFile(string filePath, string textToAppend)
    {
        // Append text to the existing file
        File.AppendAllText(filePath, textToAppend + "\nDebug report session " + DateTime.Now + "\n");  // Adds a newline after the text
    }
}