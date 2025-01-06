// Horrors are versions of Nyarlathotep, loaded in from JSON
[System.Serializable]
public class Horror : Creature
{
    public int id; // Generated on load
    public int rage; // Changes depending on player action
    public int intrigue;
    public int abstraction;
    public int magick;
    public int strength;
}
