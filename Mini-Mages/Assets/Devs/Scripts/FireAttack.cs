using UnityEngine;

public class FireAttack : Attack
{
    [Header ("Fire Attack")]
    public float ObjectSpeed;
    public float PAMovement = 3; // player automatic movement, voor die peper in je reet.

    private Rigidbody rb;

    private float AutoWalkTimer;
    private float AWTimerDone = 3;

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
        if (collision.gameObject.CompareTag("GaFFDood")) // tag nog veranderd worden naar de player
        {
            Destroy(gameObject);

            //instantiate animatie
        }
    }

}
