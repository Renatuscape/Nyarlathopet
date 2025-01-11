using System.Linq;
using UnityEngine;

// Sorts and holds human name data, and gives out a random name when called
public static class RandomNameGenerator
{
    public static string[] FirstNames { get; private set; }
    public static string[] LastNames { get; private set; }
    public static string[] CultPrefixes { get; private set; }
    public static string[] CultDefiniteSuffixes { get; private set; }
    public static string[] CultIndefiniteSuffixes { get; private set; }
    public static string[] CultNames { get; private set; }
    public static string[] ItemPrefixMagick { get; private set; }
    public static string[] ItemPrefixLore { get; private set; }
    public static string[] ItemPrefixStrength { get; private set; }
    public static string[] ItemArtefactTypes { get; private set; }
    public static string[] ItemSuffixes { get; private set; }
    public static string[] ItemTomeTypes { get; private set; }
    public static string[] ItemPrayerTypes { get; private set; }

    public static void SortNames(RandomNamesData namesData)
    {
        Report.Write("RandomNameGenerator", "Sorting names into lists.");
        FirstNames = namesData.firstNames.Split(", ").ToArray();
        LastNames = namesData.lastNames.Split(", ").ToArray();

        if (namesData.cultPrefixes != null)
        {
            CultPrefixes = namesData.cultPrefixes.Split(", ").ToArray();
        }
        if (namesData.cultDefiniteSuffixes != null)
        {
            CultDefiniteSuffixes = namesData.cultDefiniteSuffixes.Split(", ").ToArray();
        }
        if (namesData.cultIndefiniteSuffixes != null)
        {
            CultIndefiniteSuffixes = namesData.cultIndefiniteSuffixes.Split(", ").ToArray();
        }
        if (namesData.cultNames != null)
        {
            CultNames = namesData.cultNames.Split(", ").ToArray();
        }
        if (namesData.itemPrefixMagick != null)
        {
            ItemPrefixMagick = namesData.itemPrefixMagick.Split(", ").ToArray();
        }
        if (namesData.itemPrefixLore != null)
        {
            ItemPrefixLore = namesData.itemPrefixLore.Split(", ").ToArray();
        }
        if (namesData.itemPrefixStrength != null)
        {
            ItemPrefixStrength = namesData.itemPrefixStrength.Split(", ").ToArray();
        }
        if (namesData.itemArtefactTypes != null)
        {
            ItemArtefactTypes = namesData.itemArtefactTypes.Split(", ").ToArray();
        }
        if (namesData.itemSuffixes != null)
        {
            ItemSuffixes = namesData.itemSuffixes.Split(", ").ToArray();
        }
        if (namesData.itemTomeTypes != null)
        {
            ItemTomeTypes = namesData.itemTomeTypes.Split(", ").ToArray();
        }
        if (namesData.itemPrayerTypes != null)
        {
            ItemPrayerTypes = namesData.itemPrayerTypes.Split(", ").ToArray();
        }
        else
        {
            Report.Write("RandomNameGenerator", "Could not load cult names.");
        }
    }

    public static string GetRandomHumanName()
    {
        if (FirstNames == null || LastNames == null)
        {
            return "Inspector Gregson";
        }

        return FirstNames[Random.Range(0, FirstNames.Length)] + " " + LastNames[Random.Range(0, FirstNames.Length)];
    }

    public static string GetRandomCultName()
    {
        string cultName = "";

        while (true)
        {
            cultName = GenerateCultName();

            if (cultName.Length < 31)
            {
                return cultName;
            }
        }
    }

    static string GenerateCultName()
    {
        int nameType = Random.Range(0, 3); // 0 = grab a full name, 1 = construct a definite, 2 = construct indefinite

        if (CultNames == null)
        {
            return "Troup of the Trial";
        }

        if (nameType == 0)
        {
            return CultNames[Random.Range(0, CultNames.Length)];
        }
        else if (nameType == 1)
        {
            return CultPrefixes[Random.Range(0, CultPrefixes.Length)] + " of the " + CultDefiniteSuffixes[Random.Range(0, CultDefiniteSuffixes.Length)];
        }
        else
        {
            return CultPrefixes[Random.Range(0, CultPrefixes.Length)] + " of " + CultIndefiniteSuffixes[Random.Range(0, CultIndefiniteSuffixes.Length)];
        }
    }

    public static string GetRandomItemName(Item item)
    {
        if (ItemSuffixes == null)
        {
            return $"Random Item of Testing";
        }
        // Pick names depending on stats and type

        string prefix;
        string type;
        string suffix = "";

        // Choose prefix
        if (item.lore > item.strength && item.lore > item.magick)
        {
            prefix = ItemPrefixLore[Random.Range(0, ItemPrefixLore.Length)];
        }
        else if (item.strength >= item.magick && item.strength > item.lore)
        {
            prefix = ItemPrefixStrength[Random.Range(0, ItemPrefixStrength.Length)];
        }
        else
        {
            prefix = ItemPrefixMagick[Random.Range(0, ItemPrefixMagick.Length)];
        }

        // Choose type
        if (item.type == ItemType.Tome)
        {
            type = ItemTomeTypes[Random.Range(0, ItemTomeTypes.Length)];
        }
        else if (item.type == ItemType.Prayer)
        {
            type = ItemPrayerTypes[Random.Range(0, ItemPrayerTypes.Length)];
        }
        else
        {
            type = ItemArtefactTypes[Random.Range(0, ItemArtefactTypes.Length)];
        }

        // Choose suffix, if book strength is high enough
        if ((item.lore + item.magick + item.strength / 2) >= Player.Data.level)
        {
            suffix = " of " + ItemSuffixes[Random.Range(0, ItemSuffixes.Length)];
        }

        return $"{prefix} {type}{suffix}";
    }
}