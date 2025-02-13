using Unity.Mathematics;
using UnityEngine;

public class Attack_Behaviour : StateMachineBehaviour
{
    private RotateToMousePos rotateToMouse;
    private FlipByMove flipByMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rotateToMouse = animator.gameObject.GetComponent<RotateToMousePos>();
        flipByMove = FindObjectOfType<FlipByMove>();
        flipByMove.freeze = true;
        rotateToMouse.Rotate();
        if (rotateToMouse.transform.localScale.y > 0)
        {
            flipByMove.FlipRight();
        }
        else
        {
            flipByMove.FlipLeft();
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
