using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Stat Definition")]
public class StatDefinitionSO : ScriptableObject
{
    public StatType statType;
    public string displayName;
    public string description;
    //public Sprite icon;
}
