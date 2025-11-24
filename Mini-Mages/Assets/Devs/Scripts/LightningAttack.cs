using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LightningAttack : Attack
{
    [Header ("Attack Var")]
    public float ObjectSpeed;

    public float WallsBounced;
    public float BounceLimit = 2;
    private Rigidbody rb;

    Vector3 lastVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(transform.forward * ObjectSpeed, ForceMode.VelocityChange); // object speed
        lastVelocity = rb.linearVelocity;

        CountTillDT += Time.deltaTime;

        if (CountTillDT >= DestroyTime)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direction);

            rb.linearVelocity = direction * Mathf.Max(speed, 0f);

            WallsBounced++;
            //elke wall bounce is kleine animatie

            if (WallsBounced > BounceLimit)
            {
                Destroy(gameObject);
                // bij de laatste bounce een grote animatie
            }

        }

        //als ie de player raakt, dan gaat ie kapot
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player3") || collision.gameObject.CompareTag("Player4"))
        {
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            collisionRb.AddForce(transform.forward * ObjectSpeed * 2); // knockback
            //instantiate grote animatie
            Destroy(gameObject);

        }

    }
}
