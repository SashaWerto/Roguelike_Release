using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Behaviour : StateMachineBehaviour
{
    private RotateToMousePos rotateToMouse;
    private FlipByMove flipByMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent<RotateToMousePos>(out var rotateToMouseRef))
        {
            rotateToMouse = rotateToMouseRef;
            rotateToMouse.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            rotateToMouse.transform.localScale = new Vector2(1, 1);
        }
        flipByMove = FindObjectOfType<FlipByMove>();
        if (flipByMove)
        {
            flipByMove.freeze = false;
        }
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
               
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rotateToMouse.Rotate();
    }
}
