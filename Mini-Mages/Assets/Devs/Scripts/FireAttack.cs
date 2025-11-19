using UnityEngine;

public class FireAttack : Attack
{
    [Header ("Fire Attack")]
    public float ObjectSpeed;
    private Rigidbody rb;
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
        //instantiate explosie
        Destroy(gameObject);
    }

}
