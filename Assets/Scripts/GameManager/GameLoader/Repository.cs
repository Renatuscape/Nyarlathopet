using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;
public static class Repository
{
    public static TextData displayText;
    public static Location[] locations;
    public static Horror[] masks;
    public static Horror[] pets;

    public static Location GetLocationByID(int id)
    {
        return locations[id];
    }

    public static Location GetLocationByCoordinates((int x, int y) coords)
    {
        if (locations == null)
        {
            CreateDummyData();
        }

        return locations.FirstOrDefault(l => l.x == coords.x && l.y == coords.y);
    }

    public static Horror GetHorrorByID(int id)
    {
        var horrors = masks.Concat(pets).ToArray();
        return horrors[id].Clone();
    }

    public static Horror GetPetByStats(CreatureStats refStats)
    {
        if (refStats == null)
        {
            return null;
        }

        var eligiblePets = pets.Where(p => p.abstraction <= refStats.abstraction &&
                                           p.magick <= refStats.magick &&
                                           p.intrigue <= refStats.intrigue &&
                                           p.strength <= refStats.strength)
                                           .ToList();

        if (eligiblePets.Count == 0)
        {
            return null;
        }

        if (eligiblePets.Count == 1)
        {
            return eligiblePets[0];
        }

        // Calculate the "distance" from reference stats for each eligible pet and get the closest one
        return eligiblePets.OrderBy(pet =>
            Math.Pow(refStats.abstraction - pet.abstraction, 2) +
            Math.Pow(refStats.magick - pet.magick, 2) +
            Math.Pow(refStats.intrigue - pet.intrigue, 2) +
            Math.Pow(refStats.strength - pet.strength, 2))
            .First().Clone();

        // The Math.Pow(x, 2) function raises x to the power of 2 - in other words, it squares the number.
        // We use this to calculate something similar to the Euclidean distance between two points in a four-dimensional space(since we have 4 stats).
        // Very ironic to use Euclidean distance in a Lovecraft game, I know
    }

    public static void CreateDummyData()
    {
        // Initialize locations array
        locations = GenerateDummyLocations(15);

        // Initialize masks array
        masks = GenerateDummyHorrors(10);

        // Initialize pets array
        pets = GenerateDummyHorrors(10, true);
    }

    public static string GetLocationNameByLevel(int level)
    {
        int searchLevel = level;

        while (searchLevel > 0)
        {
            var locationsByLevel = locations.Where(l => l.level == searchLevel).ToList();
            if (locationsByLevel.Count > 0)
            {
                return locationsByLevel[Random.Range(0, locationsByLevel.Count)].name;
            }
            searchLevel--;
        }

        return "???";
    }

    static Horror[] GenerateDummyHorrors(int amount, bool isPet = false)
    {
        List<Horror> horrors = new();

        for (int i = 0; i < amount; i++)
        {
            var newHorror = GetDummyHorror(isPet);
            newHorror.id = i.ToString();

            int skillPoints = isPet ? i * 2 : i * 15;
            CreatureStats stats = new CreatureStats();
            stats.AddRandomStats(skillPoints);
            newHorror.ApplyStatChanges(stats);
        }

        return horrors.ToArray();
    }

    static Horror GetDummyHorror(bool isPet = false)
    {
        Horror newHorror = new()
        {
            name = RandomFactory.GetRandomString(Random.Range(6, 10)),
            description = RandomFactory.GetRandomString(Random.Range(12, 22)),
        };

        return newHorror;
    }

    static Location[] GenerateDummyLocations(int amount)
    {
        List<Location> locations = new();

        var newLocation = AttemptToGenerate();

        while (locations.Count < amount)
        {
            newLocation = AttemptToGenerate();

            if (locations.FirstOrDefault(l => l.x == newLocation.x && l.y == newLocation.y) == null)
            {
                locations.Add(newLocation);
            }
        }

        return locations.ToArray();

        Location AttemptToGenerate()
        {
            return new Location
            {
                name = RandomFactory.GetRandomString(Random.Range(4, 10)),
                level = Random.Range(1, 10),
                x = Random.Range(0, 60),
                y = Random.Range(0, 45),
                hasOccultism = Random.Range(0, 2) != 0,
                hasStrength = Random.Range(0, 2) != 0,
                hasLore = Random.Range(0, 2) != 0,
                hasMoney = Random.Range(0, 1) == 0,
                isRisky = Random.Range(0, 3) == 0
            };
        }
    }
}
