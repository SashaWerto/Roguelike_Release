using UnityEngine;

public class EnemyWalker : BasicEnemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!animatorState.IsTag("block"))
            rigidbody.AddForce(force, ForceMode2D.Force);
    }
}
