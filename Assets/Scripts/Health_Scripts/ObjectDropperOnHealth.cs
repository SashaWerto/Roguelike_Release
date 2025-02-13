using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectDropperOnHealth : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Health health;
    [SerializeField] private float dropRange = 0.5f;
    [SerializeField] private List<ObjOnHealth> objectsOnHealth;
    private void OnEnable()
    {
        health.OnDamaged += Refresh;
    }
    private void OnDisable()
    {
        health.OnDamaged -= Refresh;
    }
    public void Refresh()
    {
        for (int i = 0; i < objectsOnHealth.Count; i++)
        {
            float percent = health.currentHealth / health.maxHealth;
            if (objectsOnHealth[i].healthPercent >= percent)
            {
                SpawnObj(objectsOnHealth[i].prefab, objectsOnHealth[i].quantity);
                objectsOnHealth.Remove(objectsOnHealth[i]);
                return;
            }
        }
    }
    public void SpawnObj(GameObject obj, int quantity)
    {
        if (quantity <= 0) quantity = 1;
        for (int i = 0; i < quantity; i++)
        {
            var createdObj = Instantiate(obj, transform.position + new Vector3(Random.Range(-dropRange,dropRange),Random.Range(-dropRange,dropRange),0), quaternion.identity);
            if (RoomManager.Instance)
            {
                RoomManager.Instance.TransferInRoom(createdObj);
            }
        }
    }
}
[Serializable]
public class ObjOnHealth
{
    public GameObject prefab;
    public int quantity;
    [Range(0f,1f)]
    public float healthPercent;
}
