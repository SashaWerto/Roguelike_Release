using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcProjectile : MonoBehaviour
{
    [Header("References")]
    public AnimationCurve curve;
    public Transform gfx;

    [Header("Preferences")] 
    [SerializeField] private float radius;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float height = 3f;
    [HideInInspector] public Vector3 targetPos;
    [HideInInspector] public float damage;
    [Header("Particles")]
    [SerializeField] private ParticleSystem onDestroyParticles;
    private Vector3 startPos;
    private bool initiated;
    private float timePassed;
    private float linearT;

    private void Start()
    {
        startPos = transform.position;
    }

    public void SetTarget(Vector3 target, float damageRef)
    {
        targetPos = target;
        initiated = true;
        damage = damageRef;
    }

    public void CheckHit()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent<Player_Health>(out var playerHealth))
            {
                playerHealth.GetDamage(damage);
            }
        }
        gfx.gameObject.SetActive(false);
        onDestroyParticles.Play();
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        if (initiated)
        {
            timePassed += Time.deltaTime;
            linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);
            transform.position = Vector2.Lerp(startPos, targetPos, linearT);
            gfx.localPosition = new Vector2(0, heightT);
            if (transform.position == targetPos)
            {
                CheckHit();
                initiated = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
