// Horrors are versions of Nyarlathotep, loaded in from JSON
// For pets, the stats indicate needed materials in order to summon them
// For Nyarlathotep, the stats indicates minimum stat growth needed
[System.Serializable]
public class Horror : Creature
{
    public int id = -1;
    public int rage; // Base for pets, added upon summoning for Nyarlathotep
    public int intrigue;
    public int abstraction;
    public int magick;
    public int strength;
    public int sanityLoss; // Max potential sanity loss upon witnessing the horror
}
