using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    // Holds data relevant to the player's progress
    public string cultName;
    public int level;
    public int rounds;
    public int funds;           // The cult's pooled funds
    public int network;         // Can be used instead of funds or members, but greatly increases notoriety
    public int notoriety;       // The cult's public notoriety. Too high and your plots will be thwarted!
    public Human cultLeader;    // The current player character
    public List<Human> cultMembers = new();
    public List<Item> inventory = new(); // Holds inventory items, which are randomly generated
    public List<int> discoveredCreatures = new(); // Holds ID of discovered creatures
    public Horror currentPet;

    public override string ToString()
    {
        return $"{cultName ?? "Unnamed Cult"} ({level})\nLeader\t\t{cultLeader.name}\nFunds:\t\t{funds}\nMembers:\t{cultMembers.Count}";
    }

    public PlayerData DeepCopy()
    {
        var copy = new PlayerData
        {
            cultName = cultName,
            level = level,
            rounds = rounds,
            funds = funds,
            network = network,
            notoriety = notoriety,
            cultLeader = cultLeader,
            currentPet = currentPet
        };

        // Simple list copies are sufficient since all contained types are value types or only contain immutable types
        copy.cultMembers = new List<Human>(cultMembers);
        copy.inventory = new List<Item>(inventory);
        copy.discoveredCreatures = new List<int>(discoveredCreatures);

        return copy;
    }

    public void CreateDummyData()
    {
        cultName = "Dummies";
        level = 1;
        funds = 100;
        cultLeader = CultistFactory.GetCultist(3);
        cultLeader.origin = "Testlandia";
        cultMembers = new()
        {
            CultistFactory.GetCultist(1),
            CultistFactory.GetCultist(2),
            CultistFactory.GetCultist(3)
        };

        cultMembers[0].origin = "Testlandia";
        cultMembers[1].origin = "Constantinope";
        cultMembers[2].origin = "Nootlevania";
    }
}