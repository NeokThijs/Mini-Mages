using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Scriptable Objects/Attack")]
public class Attack : ScriptableObject
{
    [SerializeField] private Animation AttackAnimation;

    public float KnockbackAmount = 1;
    public float ObjSpeed;
    public float HitRange;

    public virtual void UseAttack()
    {

    }



}
