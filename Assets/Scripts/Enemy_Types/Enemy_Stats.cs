using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_Stats : MonoBehaviour
{
    [Header("Stats")]
    public int level = 1;
    [Header("Health")]
    public Health health;
    public AnimationCurve healthCurve;
    public float healthMultiplier = 1f;
    [Header("Damage")]
    public List<DamageCollider> damageColliders;
    public AnimationCurve damageCurve;
    public float damageMultiplier = 1f;
    public Action OnRefresh;

    private void OnEnable()
    {
        level = RoomContainer.level;
        level = Mathf.Clamp(Random.Range(level - 1, level + 2), 1, 100);
        RefreshStats();
    }

    private void Start()
    {
        RefreshStats();
    }

    public void RefreshStats()
    {
        if (health)
        {
            health.maxHealth = healthCurve.Evaluate(level) * healthMultiplier;
            health.currentHealth = health.maxHealth;
        }
        foreach (var damageCollider in damageColliders)
        {
            damageCollider.damage = damageCurve.Evaluate(level) * damageMultiplier;
        }
        OnRefresh?.Invoke();
    }
}
