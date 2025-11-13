using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Base Var")]
    [SerializeField] private Animation AttackAnimation;
    public float KnockbackAmount = 1;
    public float HitRange;

    public float YPos;

    public virtual void UseAttack()
    {

    }



}
