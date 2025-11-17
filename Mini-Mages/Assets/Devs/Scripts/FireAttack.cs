using UnityEngine;

public class FireAttack : Attack
{
    [Header ("Fire Attack")]
    public float ObjectSpeed;
    public float PAMovement = 3; // player automatic movement, voor die peper in je reet.

    private Rigidbody rb;

    [Header("Usable Attack")]
    private int UseTheAttack = 0;
    private int UsedAttacks = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(transform.forward * ObjectSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GaFFDood")) // tag nog veranderd worden naar de player
        {
            // +1 knockback
            // verhoogt de movement
            // loopt automatisch naar een kant op 
            Rigidbody hitRb = collision.gameObject.GetComponent<Rigidbody>(); // moet de player worden

            if (hitRb != null)
            {
                Vector3 dir = Vector3.forward;

                hitRb.linearVelocity = dir * 5f;   // richting de testwaypoint // pamovement word 5f trouwens
            }

            Destroy(gameObject);

            //instantiate animatie
        }
    }

}
