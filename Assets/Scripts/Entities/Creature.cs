[System.Serializable]
public class Creature : Entity
{
    public CreatureType type;
}

public enum CreatureType
{
    Cultist,
    Investigator,
    Pet,
    Nyarlathotep
}