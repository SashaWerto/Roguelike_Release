using UnityEngine;

public class OnDamageScreenAimation : MonoBehaviour
{
    [Header("References")]
    public Health health;
    public Animator animator;

    private void OnEnable()
    {
        health.OnDamaged += GetHitAnimation;
    }

    private void OnDisable()
    {
        health.OnDamaged -= GetHitAnimation;
    }

    public void GetHitAnimation()
    {
        animator.SetTrigger("getHit");
    }
}
