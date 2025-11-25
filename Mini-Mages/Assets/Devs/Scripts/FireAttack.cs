using UnityEngine;

public class FireAttack : Attack
{
    [Header ("Fire Attack")]
    public float ObjectSpeed;
    private Rigidbody rb;
    public GameObject ExplosionEffect;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(transform.forward * ObjectSpeed);

        CountTillDT += Time.deltaTime;

        if (CountTillDT >= DestroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
