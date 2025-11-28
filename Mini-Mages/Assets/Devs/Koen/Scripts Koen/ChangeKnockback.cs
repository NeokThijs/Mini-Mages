using UnityEngine;

public class ChangeKnockback : MonoBehaviour
{
    public float KnockBackStrength = 1f;
    private float originalKnockBackStrength = 1f;
    public float MaxKnockBackStrength = 3f;
    private float MinKnockBackStrength = 1f;
    public void GetHit()
    {
        if (KnockBackStrength < MaxKnockBackStrength)
        {
            KnockBackStrength++;
        }
    }
    public void Hit()
    {
        if (KnockBackStrength > MinKnockBackStrength)
        {
            KnockBackStrength--;
        }
    }
}
