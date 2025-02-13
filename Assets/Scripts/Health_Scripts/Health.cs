using UnityEngine;
using System;
public class Health : MonoBehaviour, IDamageable
{
    [Header("References")]
    public Rigidbody2D rigidbody;
    [Header("Preferences")]
    public float currentHealth;
    public float maxHealth;
    [Header("Sounds")]
    public Sound_Events hitSoundEvents;
    [HideInInspector] public bool isDead;
    public Action OnDamaged;
    public Action OnDeath;
    public virtual void Start()
    {
        maxHealth = currentHealth;
    }

    public virtual void GetDamage(float damage)
    {
        if(isDead) return;
        currentHealth -= damage;
        OnDamaged?.Invoke();
        if (currentHealth <= 0)
        {
            Dead();
        }
        if (hitSoundEvents)
        {
            hitSoundEvents.PlayAttachedClip();
        }
    }
    
    public virtual void Dead()
    {
        if(isDead) return;
        OnDeath?.Invoke();
        isDead = true;
    }
}
