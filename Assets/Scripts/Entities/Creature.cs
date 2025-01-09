[System.Serializable]
public class Creature
{
    public string name;
    public string description;
    public CreatureType type;
}

public enum CreatureType
{
    Cultist,
    Investigator,
    Pet,
    Nyarlathotep
}             