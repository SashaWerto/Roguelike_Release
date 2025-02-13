using UnityEngine;

public class HealthbarWorld : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject gfx;
    public Health health;
    private float maxLength;
    private Vector3 baseScale;
    private void OnEnable()
    {
        gfx.SetActive(false);
        baseScale = spriteRenderer.transform.localScale;
        maxLength = baseScale.x;
        health.OnDamaged += Refresh;
    }
    private void OnDisable()
    {
        health.OnDamaged -= Refresh;
    }
    public void Refresh()
    {
        if (health.currentHealth < health.maxHealth)
        {
            gfx.SetActive(true);
        }
        else
        {
            gfx.SetActive(false);
            return;
        }
        if (health.currentHealth <= 0)
        {
            spriteRenderer.transform.localScale = new Vector3(0, baseScale.y, baseScale.z);
            return;
        }
        spriteRenderer.transform.localScale = new Vector3(health.currentHealth / health.maxHealth * maxLength, baseScale.y, baseScale.z);
    }
}
