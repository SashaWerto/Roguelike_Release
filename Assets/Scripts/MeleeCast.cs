using System;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCast : MonoBehaviour
{
    [Header("References")]
    public Transform castPoint;
    public Transform centerForcePoint;
    [Header("Preferences")]
    public float damage;
    public float force;
    public float radius = 1;
    private List<Collider2D> hitColliders = new List<Collider2D>();

    private void OnEnable()
    {
        centerForcePoint = GameObject.FindWithTag("Player").transform;
    }

    private void OnDisable()
    {
        hitColliders.Clear();
    }

    private void Update()
    {
        CastRay();
    }

    public void CastRay()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(castPoint.position, radius, Vector2.zero);
        foreach (var hit in hits)
        {
            if (!hitColliders.Contains(hit.collider))
            {
                if (hit.collider.TryGetComponent<Enemy_Health>(out var enemyHealth))
                {
                    enemyHealth.GetDamage(damage + Stats.Instance.force + Stats.Instance.equipForce);
                    if (enemyHealth.rigidbody)
                    {
                        var difference = enemyHealth.rigidbody.transform.position - centerForcePoint.position;
                        var distance = difference.magnitude;
                        var direction = difference / distance;
                        enemyHealth.rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
                    }
                }
                hitColliders.Add(hit.collider);
            }
        }
    }
    private void OnDrawGizmos()
    {
        if(!castPoint) return;
        Gizmos.DrawWireSphere(castPoint.position, radius);
    }
}
