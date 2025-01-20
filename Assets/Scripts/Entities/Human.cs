[System.Serializable]
public class Human : Creature
{
    public int occultism;
    public int lore;
    public int strength;
    public int sanity;
    public int funds;
    public string origin;

    public string GetStatPrintOut(bool abbreviate, bool isList)
    {
        return
            $"{(abbreviate ? "OCL:" : "Occultism ")}{occultism:D2}{GetBreak()}" +
            $"{(abbreviate ? "LOR:" : "Lore ")}{lore:D2}{GetBreak()}" +
            $"{(abbreviate ? "STR:" : "Strength ")}{strength:D2}{GetBreak()}" +
            $"{(abbreviate ? "FND:" : "Funds ")}{funds:D2}{GetBreak()}" +
            $"{(abbreviate ? "SAN:" : "Sanity ")}{sanity:D2}";

        string GetBreak() { return isList ? "\n" : (abbreviate ? " " : ", "); }
    }
}