using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LightningAttack : Attack
{
    [Header ("Attack Var")]
    public float ObjectSpeed;

    public float WallsBounced;
    public float BounceLimit;
    private Rigidbody rb;
    public float stunDuration = 0.5f;
    public GameObject bounceEffect;
    public GameObject hitEffect;

    Vector3 lastVelocity;

    public ChangeKnockback changeKnockBack;

    private void Start()
    {
        changeKnockBack = GetComponentInParent<ChangeKnockback>();

        rb = GetComponent<Rigidbody>();
        Debug.Log(gameObject.layer);
        rb.AddForce(10f * transform.forward, ForceMode.Impulse); // initial force
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
        if (WallsBounced >= BounceLimit)
        {
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("MiniWall"))
        {
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
            rb.linearVelocity = Vector3.zero;
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direction);

            rb.linearVelocity = direction * Mathf.Max(speed, 0f);

            WallsBounced++;
        }

        //als ie de player raakt, dan gaat ie kapot
        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player3") || collision.gameObject.CompareTag("Player4"))
        {
            Player playerscript = collision.gameObject.GetComponent<Player>();
            playerscript.blinktimer = playerscript.blinkDuration; // start knipperen
            ChangeKnockback knockbackScript = collision.gameObject.GetComponent<ChangeKnockback>();
            knockbackScript.GetHit(); // knockback verhogen
            changeKnockBack.Hit(); // eigen knockback verlagen
            Rigidbody collisionRb = collision.gameObject.GetComponent<Rigidbody>();
            collisionRb.AddForce(gameObject.transform.forward * ObjectSpeed * 4 * knockbackScript.KnockBackStrength); // knockback
            playerscript.tempStun(stunDuration); // player kan niet bewegen
            Death();
        } 
    }
        public void Death()
    {
        Instantiate(bounceEffect, transform.position, transform.rotation);
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}

