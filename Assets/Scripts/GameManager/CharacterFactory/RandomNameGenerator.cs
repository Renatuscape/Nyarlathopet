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

    public static void SortNames(RandomNamesData namesData)
    {
        Report.Write("RandomNameGenerator", "Sorting names into lists.");
        FirstNames = namesData.firstNames.Split(", ").ToArray();
        LastNames = namesData.lastNames.Split(", ").ToArray();

        if (namesData.cultPrefixes != null)
        {
            CultPrefixes = namesData.cultPrefixes.Split(", ").ToArray();
        }
        else
        {
            Report.Write("RandomNameGenerator", "Could not load cult prefixes.");
        }
        if (namesData.cultDefiniteSuffixes != null)
        {
            CultDefiniteSuffixes = namesData.cultDefiniteSuffixes.Split(", ").ToArray();
        }
        else
        {
            Report.Write("RandomNameGenerator", "Could not load cult definite suffixes.");
        }
        if (namesData.cultIndefiniteSuffixes != null)
        {
            CultIndefiniteSuffixes = namesData.cultIndefiniteSuffixes.Split(", ").ToArray();
        }
        else
        {
            Report.Write("RandomNameGenerator", "Could not load cult indefinite suffixes.");
        }
        if (namesData.cultNames != null)
        {
            CultNames = namesData.cultNames.Split(", ").ToArray();
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
        // Pick names depending on stats and strength

        return $"Random Item";
    }
}