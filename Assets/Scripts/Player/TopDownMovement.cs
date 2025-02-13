using UnityEngine;
public class TopDownMovement : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rigidbody;
    [SerializeField] private Inputs inputs;
    [Header("Player Settings")]
    [SerializeField] private float speed;
    private Vector2 moveDirection;
    private void Update()
    {
        moveDirection = transform.up * inputs.VerticalInput + transform.right * inputs.HorizontalInput;
    }
    private void FixedUpdate()
    {
        Vector2 normVector = new Vector2(moveDirection.x, moveDirection.y).normalized;
        rigidbody.velocity = normVector * speed;
        //SpeedControl();
    }
    private void SpeedControl()
    {
        Vector2 flatVelocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);
        if (flatVelocity.magnitude > speed)
        {
            Vector2 limitVelocity = flatVelocity.normalized * speed;
            rigidbody.velocity = new Vector2(limitVelocity.x, limitVelocity.y);
        }
    }
}
