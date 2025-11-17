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
        rb.AddForce(transform.forward * ObjectSpeed); // object speed
        lastVelocity = rb.linearVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.linearVelocity = direction * Mathf.Max(speed, 0f);

            WallsBounced++;
            // elke wall bounce is kleine animatie

            if (WallsBounced > BounceLimit)
            {
                Destroy(gameObject);
                // bij de laatste bounce een grote animatie
            }

        }

        //als ie de player raakt, dan gaat ie kapot
        if (collision.gameObject.CompareTag("GaFFDood")) // tag nog veranderd worden
        {
            // +1 knockback
            Destroy(collision.gameObject);
            Destroy(gameObject);

            //instantiate grote animatie
        }

    }
}
