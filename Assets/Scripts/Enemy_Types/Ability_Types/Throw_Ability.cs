using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throw_Ability : Enemy_Ability
{
    [Header("References/Damage")]
    public DamageCollider damageCollider;
    [Header("Parameters")]
    [SerializeField] private float triggerDistance = 2f;
    [Header("Throw")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float throwDelay = 1f;
    private float currentTime;
    private bool isThrowing;

    private void Start()
    {
        currentTime = Random.Range(0.1f, throwDelay);
    }

    public override void Update()
    {
        base.Update();
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            StartThrow();
        }
    }
    public void Throw()
    {
        GameObject projectile = Instantiate(projectilePrefab, enemy.animator.transform.position, Quaternion.identity);
        projectile.TryGetComponent<ArcProjectile>(out var projectileRef);
        projectileRef.SetTarget(enemy.target.position, damageCollider.damage);
        isThrowing = false;
    }
    public void StartThrow()
    {
        enemy.animator.SetTrigger("Throw");
        currentTime = throwDelay;
        isThrowing = true;
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.rigidbody.transform.position, triggerDistance);
    }
}
