using UnityEngine;
public class FlipByMove : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Inputs inputs;
    [SerializeField] private Transform flipObj;
    public bool freeze;
    private void Update()
    {
        if(freeze) return;
        if(inputs.HorizontalInput != 0)
        {
            if (inputs.HorizontalInput < 0)
            {
                FlipLeft();
            }
            else
            {
                FlipRight();
            }
        }
    }

    public void FlipRight()
    {
        flipObj.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void FlipLeft()
    {
        flipObj.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
