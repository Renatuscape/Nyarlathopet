// Horrors are versions of Nyarlathotep, loaded in from JSON
// For pets, the stats indicate needed materials in order to summon them
// For Nyarlathotep, the stats indicates minimum stat growth needed
[System.Serializable]
public class Horror : Creature
{
    public string id = "-1";
    public int mythos;
    public int intrigue;
    public int magick;
    public int abstraction;
    public int strength;
    public int rage; // Pets use this as starting value. Masks add this to the total upon being summoned
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

    public string Print(bool abbreviate, bool isList)
    {
        return 
            $"{(abbreviate ? $"{Tags.Get("MTH")}:" : $"{Tags.Get("Mth")} ")}{mythos:D2}{GetBreak()}" +
            $"{(abbreviate ? $"{Tags.Get("ITR")}:" : $"{Tags.Get("Itr")} ")}{intrigue:D2}{GetBreak()}" +
            $"{(abbreviate ? $"{Tags.Get("MGK")}:" : $"{Tags.Get("Mgk")} ")}{magick:D2}{GetBreak()}" +
            $"{(abbreviate ? $"{Tags.Get("ABS")}:" : $"{Tags.Get("Abs")} ")}{abstraction:D2} {GetBreak()}" +
            $"{(abbreviate ? $"{Tags.Get("STR")}:" : $"{Tags.Get("Str")} ")}{strength:D2} {GetBreak()}" +
            $"{(abbreviate ? $"{Tags.Get("RGE")}:" : $"{Tags.Get("Rge")} ")}{rage:D2}";

        string GetBreak() { return isList ? "\n" : (abbreviate ? " " : ", "); }
    }

    public void ApplyStatChanges(CreatureStats changes)
    {
        mythos += changes.mythos;
        intrigue += changes.intrigue;

        magick += changes.magick;
        abstraction += changes.abstraction;

        strength += changes.strength;
        rage += changes.rage;
    }
}
