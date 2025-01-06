using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    // Holds data relevant to the player's progress
    public string cultName;
    public int level;
    public int funds; // The cult's pooled funds
    public Human cultLeader; // The current player character
    public List<Human> cultMembers;
    public List<Item> inventory; // Holds inventory items, which are randomly generated
    public List<int> discoveredCreatures; // Holds ID of discovered creatures
    public Horror currentCreature;
}