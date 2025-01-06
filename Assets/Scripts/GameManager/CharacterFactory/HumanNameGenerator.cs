using System.Linq;
using UnityEngine;

// Sorts and holds human name data, and gives out a random name when called
public static class HumanNameGenerator
{
    public static string[] FirstNames { get; private set; }
    public static string[] LastNames { get; private set; }

    public static void SortNames(HumanNamesData namesData)
    {
        FirstNames = namesData.firstNames.Split(", ").ToArray();
        LastNames = namesData.lastNames.Split(", ").ToArray();
    }

    public static string GetRandomName()
    {
        return FirstNames[Random.Range(0, FirstNames.Length)] + " " + LastNames[Random.Range(0, FirstNames.Length)];
    }
}