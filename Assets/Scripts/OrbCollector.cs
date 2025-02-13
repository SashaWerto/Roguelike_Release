using System;
using UnityEngine;

public class OrbCollector : MonoBehaviour
{
    public float takeRadius = 0.5f;
    public float magnitRadius = 1f;
    public float force;
    private void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, magnitRadius, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<Orb>(out var orb))
            {
                if (orb.canBeTaked)
                {
                    orb.SetTarget(transform);
                }
                var difference = transform.position - orb.transform.position;
                if (difference.magnitude <= takeRadius)
                {
                    Wallet.Instance.AddGold(orb.quantity);
                    Destroy(orb.gameObject);
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnitRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, takeRadius);
    }
}
