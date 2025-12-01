using UnityEngine;

public class ChangeKnockback : MonoBehaviour
{
    public float KnockBackStrength = 2f;
    public float MaxKnockBackStrength = 6f;
    private float MinKnockBackStrength = 2f;

    private float cooldown;
    public float cooldownReset = 0.5f;

    private void Start()
    {
        cooldown = cooldownReset;
    }
    private void Update()
    {
        cooldown -= Time.deltaTime;
    }
    public void GetHit()
    {
        if (KnockBackStrength < MaxKnockBackStrength && cooldown <= 0f)
        {
            KnockBackStrength += 2;
            cooldown = cooldownReset;
        }
    }
    public void Hit()
    {
        if (KnockBackStrength > MinKnockBackStrength && cooldown <= 0f)
        {
            KnockBackStrength -= 2;
            cooldown = cooldownReset;
        }
    }
}
