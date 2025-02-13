using UnityEngine;
public class Player_Animator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Inputs inputs;
    [Header("Components")]
    public Animator animator;
    private void Update()
    {
        Animations();
    }
    public void Animations()
    {
        if (inputs.HorizontalInput != 0 || inputs.VerticalInput != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
