using System;
using UnityEngine;

public class Enemy_Ability : MonoBehaviour
{
    [Header("Reference")] 
    public BasicEnemy enemy;
    [HideInInspector] public bool isBusy;

    public virtual void Update()
    {
        if (enemy.animatorState.IsTag("block"))
        {
            isBusy = true;
        }
        else isBusy = false;
    }
    public virtual void FixedUpdate()
    {
    }
}
