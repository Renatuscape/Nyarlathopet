using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public string name;
    public CreatureType type;
}

public enum CreatureType
{
    Cultist,
    Investigator,
    Nightmare,
    Nyarlathotep
}