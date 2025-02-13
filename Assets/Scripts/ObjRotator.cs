using UnityEngine;
public class ObjRotator : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Rotate(0,0, speed * Time.deltaTime);
    }
}
