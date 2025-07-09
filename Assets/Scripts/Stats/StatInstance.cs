using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class StatInstance
{
    public StatDefinitionSO statDef;
    public int baseValue;
    public int maxValue;
    private List<int> modifiers = new();

    public int Value => baseValue + modifiers.Sum();

    public void IncreaseBaseValue(int amount) => baseValue += amount;
    public void RestBaseValue(int amount) => baseValue -= amount;
    public void AddModifier(int mod) => modifiers.Add(mod);
    public void RemoveModifier(int mod) => modifiers.Remove(mod);
}
