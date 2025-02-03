// Horrors are versions of Nyarlathotep, loaded in from JSON
// For pets, the stats indicate needed materials in order to summon them
// For Nyarlathotep, the stats indicates minimum stat growth needed
using System;
using UnityEngine;

[Serializable]
public class Horror : Creature
{
    public string id = "-1";
    [field: SerializeField]
    public int mythos { get; private set; }
    [field: SerializeField]
    public int intrigue { get; private set; }
    [field: SerializeField]
    public int magick { get; private set; }
    [field: SerializeField]
    public int abstraction { get; private set; }
    [field: SerializeField]
    public int strength { get; private set; }
    [field: SerializeField]
    public int rage { get; private set; } // Pets use this as starting value. Masks add this to the total upon being summoned
    [field: SerializeField]
    public int sanityLoss { get; private set; } // Max potential sanity loss upon witnessing the horror

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

    public int GetLevel()
    {
        return Math.Max(1, (mythos + intrigue + magick + abstraction + strength + rage) / 15);
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
        mythos = Math.Clamp(mythos + changes.mythos, 0, 99);
        intrigue = Math.Clamp(intrigue + changes.intrigue, 0, 99);

        magick = Math.Clamp(magick + changes.magick, 0, 99);
        abstraction = Math.Clamp(abstraction + changes.abstraction, 0, 99);

        strength = Math.Clamp(strength + changes.strength, 0, 99);
        rage = Math.Clamp(rage + changes.rage, 0, 99);
    }
}
