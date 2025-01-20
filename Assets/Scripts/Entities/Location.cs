// Node on the map. Level denotes when it will be accessible to visit, x and y are map coordinates
[System.Serializable]
public class Location: Entity
{
    public int level = 1;           // Player level must be at least this high

    public int x;
    public int y;

    public bool hasMagick;      // Items from here can increase magick
    public bool hasStrength;    // Items from here can increase stength
    public bool hasLore;        // Items from here can increase lore
    public bool hasMoney;       // Money can be gained here
    public bool isRisky;        // A risky location has higher rewards and higher potential costs
}