using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Base Var")]
    [SerializeField] private Animation AttackAnimation;
    public float KnockbackAmount = 1;
    public float HitRange;

    public float YPos;

    [Header("Destroy Time")]
    public float CountTillDT;
    public float DestroyTime = 6;

    public virtual void UseAttack()
    {

    }



}
