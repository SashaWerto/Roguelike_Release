using UnityEngine;

public class Dead : MonoBehaviour
{
    [Header("References")]
    public Health health;
    public GameObject destroyObj;

    private void OnEnable()
    {
        health.OnDeath += DestroyThis;
    }
    private void OnDisable()
    {
        health.OnDeath -= DestroyThis;
    }

    public void DestroyThis()
    {
        Destroy(destroyObj);
    }
}
