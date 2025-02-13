using System;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangerByHealth : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [Header("References/GFX")]
    public List<SpriteByHealth> stages = new List<SpriteByHealth>();

    public ParticleSystem onStageParticles;

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
        for (int i = 0; i < stages.Count; i++)
        {
            float percent = health.currentHealth / health.maxHealth;
            if (stages[i].healthPercent > percent)
            {
                ChangeStage(stages[i].sprite);
                stages.Remove(stages[i]);
                return;
            }
        }
    }
    public void ChangeStage(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        onStageParticles.Play();
    }
}
[Serializable]
public class SpriteByHealth
{
    public Sprite sprite;
    [Range(0f,1f)]
    public float healthPercent;
}
