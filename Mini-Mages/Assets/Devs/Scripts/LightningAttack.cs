using UnityEngine;

public class LightningAttack : Attack
{
    public float ObjectSpeed;

    public float WallsBounced;
    public float BounceLimit = 2;

    private Rigidbody rb;
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            WallsBounced++;
            rb.AddForce(-transform.forward * ObjectSpeed);
            if (WallsBounced >= BounceLimit)
            {
                
            } 

            //als ie de player raakt, dan gaat ie kapot
        }
    }

}
