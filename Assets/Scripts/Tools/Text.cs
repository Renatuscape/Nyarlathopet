using System.Linq;

public static class Text
{
    public static string Get(string tag)
    {
        var entry = Repository.displayText?.entries?.FirstOrDefault(e => e[0] == tag);

        if (entry == null)
        {
            Report.Write("Tags", tag + " returned null.");
        }

        return entry != null ? entry[1] : RandomFactory.GetRandomString(UnityEngine.Random.Range(6, 12));
    }
}