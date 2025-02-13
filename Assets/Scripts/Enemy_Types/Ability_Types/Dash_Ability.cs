using System;
using UnityEngine;

public class Dash_Ability : Enemy_Ability
{
    [Header("Parameters")]
    [SerializeField] private float triggerDistance = 2f;
    [Header("Dash")] 
    [SerializeField] private float dashTime = 1f;
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashCooldown;
    private float maxDashCooldown;
    private float currentDashTime;
    private bool isDashing;
    public void Start()
    {
        maxDashCooldown = dashCooldown;
        currentDashTime = dashTime;
        dashCooldown = 0;
    }
    public override void Update()
    {
        base.Update();
        CheckDash();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Dash();
    }

    public void Dash()
    {
        if(isBusy) return;
        if (isDashing)
        {
            currentDashTime -= Time.deltaTime;
            if (currentDashTime <= 0)
            {
                currentDashTime = dashTime;
                isDashing = false;
            }
            var difference = enemy.target.position - enemy.rigidbody.transform.position;
            var distance = difference.magnitude;
            var direction = difference / distance;
            enemy.rigidbody.AddForce(direction * dashForce, ForceMode2D.Force);
        }
    }
    public void CheckDash()
    {
        dashCooldown += Time.deltaTime;
        if(dashCooldown < maxDashCooldown) return;
        
        if (enemy.distanceToPlayer > triggerDistance)
        {
            dashCooldown = 0;
            isDashing = true;
            enemy.animator.SetTrigger("Dash");
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.rigidbody.transform.position, triggerDistance);
    }
}
