[System.Serializable]
public class Item
{
    public int level;
    public string name;
    public ItemType type;

    public int occultism;
    public int strength;
    public int lore;

    public string Print(bool lineBreak = true, bool includeType = false, bool noName = false)
    {
        return $"{(noName ? "" : name)}{(lineBreak ? "\n" : (noName ? "" : " "))}" +
            $"{(includeType ? $"{Tags.Get(type.ToString())} " : "")}" +
            $"{Tags.Get("OCL")}:{occultism:D2} {Tags.Get("LOR")}:{lore:D2} {Tags.Get("STR")}:{strength:D2}";
    }
}

public enum ItemType
{
    Artefact,
    Tome,
    Prayer
}