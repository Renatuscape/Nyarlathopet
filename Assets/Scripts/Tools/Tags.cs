using System.Linq;

public static class Tags
{
    public static string Get(string tag)
    {
        var entry = Repository.displayText?.tags?.FirstOrDefault(e => e[0] == tag);

        if (entry == null)
        {
            Report.Write("Tags", tag + " returned null.");
        }

        return entry != null ? entry[1] : $"[{tag}]";
    }
}
