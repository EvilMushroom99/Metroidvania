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
            StatInstance stat = new(){ baseValue = entry.baseValue, statDef = entry.stat, maxValue = entry.maxValue};
            _stats[entry.stat.statType] = stat;
        }
        RaiseEvent();
    }

    public int GetStat(StatType type)
    {
        return _stats.TryGetValue(type, out var stat) ? stat.Value : 0;
    }

    public void IncreaseBaseValue(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            if (type == StatType.Health && stat.Value == stat.maxValue) return;
            stat.IncreaseBaseValue(amount);
            RaiseEvent();
        }
    }

    public void RestBaseValue(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.RestBaseValue(amount);
            RaiseEvent();
        }
    }

    public void AddModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.AddModifier(amount);
            RaiseEvent();
        }
    }

    public void RemoveModifier(StatType type, int amount)
    {
        if (_stats.TryGetValue(type, out var stat))
        {
            stat.RemoveModifier(amount);
            RaiseEvent();
        }
    }

    private void RaiseEvent()
    {
        if (onStatsChanged != null)
            onStatsChanged.Raise();
    }
}


