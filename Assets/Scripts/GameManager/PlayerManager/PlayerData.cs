using System.Collections.Generic;

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
}