using System;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public float damage;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<Player_Health>(out var health))
        {
            health.GetDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Player_Health>(out var health))
        {
            health.GetDamage(damage);
        }
    }
}
