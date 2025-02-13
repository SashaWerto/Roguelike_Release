using System;
using System.Collections.Generic;
using UnityEngine;

public class BuffsBasedOnHealth : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Health health;
    [SerializeField] private EnemyAI enemy;
    [SerializeField] private DamageCollider damageCollider;
    [Header("Buff's")]
    [SerializeField] private List<BuffEnemyParameters> buffs;
    private void OnEnable()
    {
        health.OnDamaged += Refresh;
    }
    private void OnDisable()
    {
        health.OnDamaged -= Refresh;
    }
    public void Refresh()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            float percent = health.currentHealth / health.maxHealth;
            if (buffs[i].healthPercent > percent)
            {
                AddBuff(buffs[i]);
                buffs.Remove(buffs[i]);
                return;
            }
        }
    }
    public void AddBuff(BuffEnemyParameters buff)
    {
        switch (buff.buffType)
        {
            case BuffType.Speed:
                if (enemy)
                {
                    enemy.speed *= buff.valueMultiplier;
                }
                break;
            case BuffType.Damage:
                if (damageCollider)
                {
                    damageCollider.damage *= buff.valueMultiplier;
                }
                break;
        }
    }
}
[Serializable]
public class BuffEnemyParameters
{
    public BuffType buffType;
    [Range(0f,10f)]
    public float valueMultiplier;
    [Range(0f,1f)]
    public float healthPercent;
}
