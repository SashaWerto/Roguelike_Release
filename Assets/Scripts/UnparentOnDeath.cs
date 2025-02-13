using UnityEngine;

public class UnparentOnDeath : MonoBehaviour
{
    [Header("References")]
    public Health health;
    public ParticleSystem unParentObj;

    private void OnEnable()
    {
        health.OnDeath += UnParent;
    }
    private void OnDisable()
    {
        health.OnDeath -= UnParent;
    }
    public void UnParent()
    {
        if (unParentObj)
        {
            if (RoomManager.Instance)
            {
                RoomManager.Instance.TransferInRoom(unParentObj.gameObject);
            }
            unParentObj.Play();
        }
    }
}
