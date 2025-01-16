// Horrors are versions of Nyarlathotep, loaded in from JSON
// For pets, the stats indicate needed materials in order to summon them
// For Nyarlathotep, the stats indicates minimum stat growth needed
[System.Serializable]
public class Horror : Creature
{
    public string id = "-1";
    public int rage; // Pets use this as starting value. Masks add this to the total upon being summoned
    public int intrigue;
    public int abstraction;
    public int magick;
    public int strength;
    public int sanityLoss; // Max potential sanity loss upon witnessing the horror

    public Horror Clone()
    {
        return new Horror
        {
            name = name,
            description = description,
            type = type,
            id = id,
            abstraction = abstraction,
            magick = magick,
            intrigue = intrigue,
            strength = strength
        };
    }
}
