using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LightningAttack : Attack
{
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
            //transform.Rotate(new Vector3(0, 180f, 0));
            //transform.position = Vector3.Reflect(transform.position, transform.forward);
            

            if (WallsBounced >= BounceLimit)
            {
                // iets van verwijderen of dat ie niet meer door bounced
            }

            //als ie de player raakt, dan gaat ie kapot

        }


        //test if statement
        if (collision.gameObject.CompareTag("kutjebef"))
        {

        }

    }
}
