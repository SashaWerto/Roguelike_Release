using System;
using System.Collections;
using UnityEngine;
public class Orb : MonoBehaviour
{
    public float quantity;
    [HideInInspector] public bool canBeTaked;
    [HideInInspector] public Transform target;
    private float time;
    private void Start()
    {
        Invoke("Initiate", 0.35f);
    }

    private void Update()
    {
        if (target)
        {
            time += 1f * Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, target.position, time);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        if(target) return;
        target = newTarget;
    }
    private void Initiate()
    {
        canBeTaked = true;
    }
}
