[System.Serializable]
public class Human : Creature
{
    public int occultism;
    public int lore;
    public int strength;
    public int sanity;
    public int funds;
    public string origin;

    public string Print(bool lineBreak = true, bool includeSanity = false, bool includeFunds = false, bool noName = false)
    {
        return $"{(noName? "": name)}{(lineBreak ? "\n" : (noName ? "" : " "))}" +
            $"{(includeSanity ? $"{Tags.Get("SAN")}:{sanity:D2} " : "")}" +
            $"{(includeFunds ? $"{Tags.Get("FNS")}:{funds:D2} " : "")}" +
            $"{Tags.Get("OCL")}:{occultism:D2} {Tags.Get("LOR")}:{lore:D2} {Tags.Get("STR")}:{strength:D2}";
    }
}