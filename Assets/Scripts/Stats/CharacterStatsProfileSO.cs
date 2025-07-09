using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Stat Profile")]
public class CharacterStatsProfileSO : ScriptableObject
{
    [System.Serializable]
    public struct StatEntry
    {
        public StatDefinitionSO stat;
        public int baseValue;
        public int maxValue;
    }

    public List<StatEntry> stats;
}
