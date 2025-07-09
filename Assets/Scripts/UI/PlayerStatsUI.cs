using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private CharacterStats characterStats;
    [SerializeField] private GameObject healthParent;
    [SerializeField] private GameObject heart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private int currentHealth;
    private readonly List<GameObject> hearts = new();

    private void Start()
    {
        InitializeStatsUI();
    }

    private void InitializeStatsUI()
    {
        if (characterStats != null)
        {
            currentHealth = characterStats.GetStat(StatType.Health);

            for (int i = 0; i < currentHealth; i++)
            {
                GameObject newHeart = Instantiate(heart, healthParent.transform);
                hearts.Add(newHeart);
            }
        }
    }

    public void RefreshStatsUI()
    {
        if (characterStats != null)
        {
            currentHealth = characterStats.GetStat(StatType.Health);

            for (int i = currentHealth; i < hearts.Count; i++)
            {
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }

            for (int i = 0; i < currentHealth && i < hearts.Count; i++)
            {
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
        }
    }
}
