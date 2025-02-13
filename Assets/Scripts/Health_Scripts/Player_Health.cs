using System;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : Health
{
    [Header("References/Objects")]
    public List<GameObject> objToDisableOnDeath;
    [Header("Audio")]
    public AudioClip heartBeatClip;
    public AudioClip deathClip;
    public AudioClip reviveClip;
    public float damageDelay = 1f;
    private float currentDelay;
    private static Player_Health playerHealth;
    public static Player_Health Instance => playerHealth;
    public override void Start()
    {
        playerHealth.maxHealth = Stats.Instance.health + Stats.Instance.equipHealth;
        playerHealth.currentHealth = playerHealth.maxHealth;
    }

    private void Awake()
    {
        playerHealth = this;
    }
    private void Update()
    {
        currentDelay -= Time.deltaTime;
    }

    public override void GetDamage(float damage)
    {
        if (currentDelay > 0 || isDead) return;
        currentDelay = damageDelay;
        float armorAll = Stats.Instance.armor + Stats.Instance.equipArmor;
        var multiplier = 1f - (0.05f * armorAll / (1f + 0.05f * Mathf.Abs(armorAll)));
        currentHealth -= multiplier * damage;
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

    public override void Dead()
    {
        base.Dead();
        if (objToDisableOnDeath.Count > 0)
        {
            foreach (var obj in objToDisableOnDeath)
            {
                obj.SetActive(false);
            }
            rigidbody.isKinematic = true;
            Sound_Manager.Instance.PlayShot(deathClip);
            Sound_Manager.Instance.ChangeMusic(heartBeatClip);
            rigidbody.velocity = Vector2.zero;
        }
    }
    public void Revive()
    {
        currentHealth = maxHealth;
        foreach (var obj in objToDisableOnDeath)
        {
            obj.SetActive(true);
        }
        rigidbody.isKinematic = false;
        isDead = false;
        currentDelay = 3f;
        Sound_Manager.Instance.PlayShot(reviveClip);
        Sound_Manager.Instance.ChangeMusicToMain();
        OnDamaged?.Invoke();
    }
}
