using System;
using Unity.Mathematics;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [Header("References")]
    public GameObject objToSpawn;
    public Animator animator;
    public Health health;
    [Header("Preferences")]
    public float growTime = 5f;
    [HideInInspector] public float currentGrowTime;
    private bool isGrow;

    private void OnEnable()
    {
        currentGrowTime = growTime;
    }

    private void OnDisable()
    {
        currentGrowTime = growTime;
    }

    private void Update()
    {
        currentGrowTime -= Time.deltaTime;
        if (currentGrowTime <= 0 && !isGrow)
        {
            SpawnEntity();
            isGrow = true;
        }
    }

    public void SpawnEntity()
    {
        var entity = Instantiate(objToSpawn, transform.position, quaternion.identity);
        if (RoomManager.Instance)
        {
            RoomManager.Instance.TransferInRoom(entity);
        }
        health.Dead();
    }
}
