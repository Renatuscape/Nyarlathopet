using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    // Holds data relevant to the player's progress
    public string cultName;
    public int level;
    public int funds; // The cult's pooled funds
    public Human cultLeader; // The current player character
    public List<Human> cultMembers = new();
    public List<Item> inventory = new(); // Holds inventory items, which are randomly generated
    public List<int> discoveredCreatures = new(); // Holds ID of discovered creatures
    public Horror currentCreature;

    public override string ToString()
    {
        return $"{cultName ?? "Unnamed Cult"} ({level})\nLeader\t\t{cultLeader.name}\nFunds:\t\t{funds}\nMembers:\t{cultMembers.Count}";
    }
}