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
            rb.linearVelocity = Vector3.zero;
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direction);

            rb.linearVelocity = direction * Mathf.Max(speed, 0f);

            WallsBounced++;
            //elke wall bounce is kleine animatie
        }
        else
        {
            Destroy(gameObject);
        }
        //als ie de player raakt, dan gaat ie kapot
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player3") || collision.gameObject.CompareTag("Player4"))
        {
            Player playerscript = collision.gameObject.GetComponent<Player>();
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            collisionRb.AddForce(transform.forward * ObjectSpeed * 4); // knockback
            playerscript.tempStun(1); // player kan niet bewegen

            //instantiate grote animatie
            Destroy(gameObject);

        }

    }
}
