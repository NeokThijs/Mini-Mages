using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animation AttackAnimation;

    public float KnockbackAmount = 1;
    public float ObjSpeed;
    public float HitRange;

    public float YPos;

    public virtual void UseAttack()
    {

    }



}
