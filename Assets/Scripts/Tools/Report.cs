public static class Report
{
    // Receives debug statements and sends them to DebugManager
    public static void Write(string name, string report)
    {
        DebugManager.LogDebugStatement(name, report);
    }
}
