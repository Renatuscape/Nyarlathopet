using System;
using System.Linq;

public static class Repository
{
    public static TextData displayText;
    public static Location[] locations;
    public static Horror[] masks;
    public static Horror[] pets;

    public static string GetText(string tag)
    {
        var entry = displayText?.entries?.FirstOrDefault(e => e[0] == tag);

        if (entry == null)
        {
            Report.Write("Repository", tag + " returned null.");
        }

        return entry != null ? entry[1] : "▓║▓┤▓░▓";
    }

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

    public static Horror GetPetByStats(Horror refStats)
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
        locations = new Location[]
        {
            new Location
            {
                name = "Duckberg",
                level = 1,
                x = 0,
                y = 0,
                hasMagick = true,
                hasStrength = false,
                hasLore = true,
                hasMoney = false,
                isRisky = false
            },
            new Location
            {
                name = "Walachia",
                level = 1,
                x = 0,
                y = 1,
                hasMagick = false,
                hasStrength = true,
                hasLore = false,
                hasMoney = true,
                isRisky = true
            },
            new Location
            {
                name = "Constantinople",
                level = 1,
                x = 1,
                y = 0,
                hasMagick = true,
                hasStrength = true,
                hasLore = false,
                hasMoney = true,
                isRisky = true
            }
        };

        // Initialize masks array
        masks = new Horror[]
        {
            new Horror
            {
                id = "MASK2",
                name = "Void Visage",
                description = "A mask that seems to absorb light and hope alike.",
                rage = 12,
                intrigue = 7,
                abstraction = 8,
                magick = 4,
                strength = 2,
                sanityLoss = 5
            },
            new Horror
            {
                id = "MASK3",
                name = "Crown of Thorns",
                description = "Ancient symbols writhe across its surface, forming patterns that hurt to observe.",
                rage = 18,
                intrigue = 3,
                abstraction = 6,
                magick = 6,
                strength = 3,
                sanityLoss = 7
            },
            new Horror
            {
                id = "MASK4",
                name = "Whispers of the Deep",
                description = "This mask speaks in tongues that no mortal should comprehend.",
                rage = 14,
                intrigue = 8,
                abstraction = 9,
                magick = 5,
                strength = 1,
                sanityLoss = 8
            }
                };

        // Initialize pets array
        pets = new Horror[]
        {
            new Horror
            {
                id = "PET0",
                name = "Dummy",
                description = "If you can't summon this one, I don't know what you're doing.",
                rage = 8,
                intrigue = 0,
                abstraction = 0,
                magick = 1,
                strength = 1,
                sanityLoss = 4
            },
            new Horror
            {
                id = "PET2",
                name = "Shifting Shade",
                description = "A living shadow that dances at the edge of reality.",
                rage = 8,
                intrigue = 2,
                abstraction = 3,
                magick = 2,
                strength = 0,
                sanityLoss = 4
            },
            new Horror
            {
                id = "PET3",
                name = "Ethereal Familiar",
                description = "A wispy creature that phases between dimensions.",
                rage = 6,
                intrigue = 3,
                abstraction = 0,
                magick = 3,
                strength = 0,
                sanityLoss = 3
            },
            new Horror
            {
                id = "PET4",
                name = "Crawling Chaos",
                description = "A mass of tentacles and eyes that shouldn't exist.",
                rage = 12,
                intrigue = 1,
                abstraction = 4,
                magick = 1,
                strength = 1,
                sanityLoss = 5
            }
        };
    }

    public static string GetLocationNameByLevel(int level)
    {
        int searchLevel = level;

        while (searchLevel > 0)
        {
            var locationsByLevel = locations.Where(l => l.level == searchLevel).ToList();
            if (locationsByLevel.Count > 0)
            {
                return locationsByLevel[UnityEngine.Random.Range(0, locationsByLevel.Count)].name;
            }
            searchLevel--;
        }

        return "???";
    }
}
