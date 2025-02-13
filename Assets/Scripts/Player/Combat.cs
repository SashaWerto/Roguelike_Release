using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator weaponAnimator;
    public float attackSpeed;

    private void OnEnable()
    {
        Inputs.OnPrimaryDownAction += AttackPrimary;
        Inputs.OnSecondaryDownAction += AttackSecondary;
    }

    private void OnDisable()
    {
        Inputs.OnPrimaryDownAction -= AttackPrimary;
        Inputs.OnSecondaryDownAction -= AttackSecondary;
    }

    public void AttackPrimary()
    {
        weaponAnimator.SetTrigger("AttackPrimary");
    }
    public void AttackSecondary()
    {
        weaponAnimator.SetTrigger("AttackSecondary");
    }
}
