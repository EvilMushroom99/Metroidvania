using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStatsProfileSO profile;
    public GameEvent onStatsChanged;
    private Dictionary<StatType, StatInstance> _stats;

    void Awake()
    {
        _stats = new();
        foreach (var entry in profile.stats)
        {
            StatInstance stat = new(){ baseValue = entry.baseValue, statDef = entry.stat};
            _stats[entry.stat.statType] = stat;
        }
        onStatsChanged.Raise();
    }

    public int GetStat(StatType type)
    {
        return _stats.TryGetValue(type, out var stat) ? stat.Value : 0;
    }

    public void AddModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.AddModifier(amount);
            onStatsChanged.Raise();
        }
    }

    public void RemoveModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.RemoveModifier(amount);
            onStatsChanged.Raise();
        }
    }
}


