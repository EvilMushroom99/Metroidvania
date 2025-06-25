using UnityEngine;
using System.Collections;

public class BuffHandler : MonoBehaviour
{
    public CharacterStats stats;

    public void StartBuffCoroutine(StatType stat, int amount, float duration)
    {
        StartCoroutine(RemoveAfterTime(stat, amount, duration));
    }

    private IEnumerator RemoveAfterTime(StatType stat, int amount, float duration)
    {
        yield return new WaitForSeconds(duration);
        stats.RemoveModifier(stat, amount);
    }
}


