using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicEnemy : EnemyAI
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (rigidbody.velocity.magnitude > 0.1f && !animatorState.IsTag("block"))
        {
            animator.SetBool("isMoving", true);
        }
        else animator.SetBool("isMoving", false);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
