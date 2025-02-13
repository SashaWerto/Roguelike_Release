using System;
using Unity.Mathematics;
using UnityEngine;

public class OnDamage : MonoBehaviour
{
    [Header("References/Obj")]
    public Health health;
    public GameObject bloodObj;
    [Header("References/Particles")]
    public ParticleSystem bloodParticles;
    private void OnEnable()
    {
        health.OnDamaged += SpawnBlood;
    }
    private void OnDisable()
    {
        health.OnDamaged -= SpawnBlood;
    }
    public void SpawnBlood()
    {
        if (bloodObj)
        {
            GameObject createdBlood = Instantiate(bloodObj, health.transform.position, quaternion.identity);
            if (RoomManager.Instance)
            {
                RoomManager.Instance.TransferInRoom(createdBlood);
            }   
        }
        bloodParticles.Play();
    }
}
