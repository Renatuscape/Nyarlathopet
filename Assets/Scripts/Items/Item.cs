[System.Serializable]
public class Item
{
    public string name;
    public ItemType type;
}

public enum ItemType
{
    Artefact,
    Tome,
    Spell,
    Prayer
}