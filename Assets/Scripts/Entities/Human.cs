using System;
using UnityEngine;

[Serializable]
public class Human : Creature
{
    [field: SerializeField]
    public int occultism { get; private set; }
    [field: SerializeField]
    public int lore { get; private set; }
    [field: SerializeField]
    public int strength { get; private set; }
    [field: SerializeField]
    public int sanity { get; private set; }
    [field: SerializeField]
    public int funds { get; private set; }
    public string origin;

    public string Print(bool lineBreak = true, bool includeSanity = false, bool includeFunds = false, bool noName = false)
    {
        return $"{(noName ? "" : name)}{(lineBreak ? "\n" : (noName ? "" : " "))}" +
            $"{(includeSanity ? $"{Tags.Get("SAN")}:{sanity:D2} " : "")}" +
            $"{(includeFunds ? $"{Tags.Get("FNS")}:{funds:D2} " : "")}" +
            $"{Tags.Get("OCL")}:{occultism:D2} {Tags.Get("LOR")}:{lore:D2} {Tags.Get("STR")}:{strength:D2}";
    }

    public int GetLevel()
    {
        return Math.Max(1, (occultism + lore + strength) / 2);
    }

    public void ApplyStatChanges(MundaneStats changes)
    {
        occultism = Math.Clamp(occultism + changes.occultism, 0, 99);
        lore = Math.Clamp(lore + changes.lore, 0, 99);
        strength = Math.Clamp(strength + changes.strength, 0, 99);
        funds = Math.Clamp(funds + changes.funds, 0, 99);
        sanity = Math.Clamp(sanity + changes.sanity, 0, 99);
    }
}