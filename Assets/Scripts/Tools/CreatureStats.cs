using System.Linq;
using UnityEngine;

[System.Serializable]
public class CreatureStats
{
    public int mythos;
    public int intrigue;

    public int magick;
    public int abstraction;

    public int strength;
    public int rage;

    public void AddRandomStats(int totalPoints)
    {
        var stats = typeof(CreatureStats)
            .GetFields()
            .Where(f => f.FieldType == typeof(int))
            .ToArray();

        for (int i = 0; i < totalPoints; i++)
        {
            stats[Random.Range(0, stats.Length)]
                .SetValue(this, (int)stats[Random.Range(0, stats.Length)].GetValue(this) + 1);
        }
    }
}
