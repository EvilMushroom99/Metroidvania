using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStatsProfileSO profile;

    private Dictionary<StatType, StatInstance> _stats;

    void Awake()
    {
        _stats = new();
        foreach (var entry in profile.stats)
        {
            StatInstance stat = new(){ baseValue = entry.baseValue, statDef = entry.stat};
            _stats[entry.stat.statType] = stat;
        }
    }

    public int GetStat(StatType type)
    {
        return _stats.TryGetValue(type, out var stat) ? stat.Value : 0;
    }

    public void AddModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            Debug.Log("stat: " + stat.statDef.statType + " value: " + stat.Value);
            stat.AddModifier(amount);
            Debug.Log("stat: " + stat.statDef.statType + " value: " + stat.Value);
        }
    }

    public void RemoveModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.RemoveModifier(amount);
        }
    }
}


