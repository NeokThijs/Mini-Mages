using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Base Var")]
    [SerializeField] private Animation AttackAnimation;
    public float KnockbackAmount = 1;
    public float HitRange;

    public float YPos;

    [Header("Usable Attack")]
    public int UseTheAttack = 0;
    public int UsedAttacks = 3;

    public virtual void UseAttack()
    {

    }



}
