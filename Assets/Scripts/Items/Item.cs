[System.Serializable]
public class Item
{
    public int level;
    public string name;
    public ItemType type;

    public int magick;
    public int strength;
    public int lore;
}

public enum ItemType
{
    Artefact,
    Tome,
    Prayer
}