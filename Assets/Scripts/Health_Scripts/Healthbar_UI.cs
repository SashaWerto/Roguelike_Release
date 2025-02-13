using System;
using UnityEngine;
using UnityEngine.UI;
public class Healthbar_UI : MonoBehaviour
{
    public Health health;
    public Image healthBar;

    private void Start()
    {
        Refresh();
    }

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
        healthBar.fillAmount = health.currentHealth / health.maxHealth;
    }
}
