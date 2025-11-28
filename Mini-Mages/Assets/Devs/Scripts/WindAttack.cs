using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WindAttack : Attack
{

    [Header ("Speed Settings")]
    public float MaxObjSpeed = 1000f;
    public float AttackSpeed = 900f;
    public float lessSpeedPSec = 100f;
    public float ObjectSpeed;
    public float Hitback;
    
    
    public GameObject parent;
    private int parentLayer;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ObjectSpeed = AttackSpeed;
        if(rb != null)
        {
            rb.AddForce(transform.forward * ObjectSpeed); // lerpen
            if(parent != null)
            {
                parentLayer = parent.layer;
                gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(parentLayer));
            }
        }
    }

    private void Update()
    {
        CountTillDT += Time.deltaTime;

        if(CountTillDT >= DestroyTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3") || other.gameObject.CompareTag("Player4"))
        {
            // jij geeft de player +1 knockback
            Rigidbody collisionRb = other.gameObject.GetComponent<Rigidbody>();
            Player playerScript = other.gameObject.GetComponent<Player>();
            playerScript.tempStun(0.2f); // stun de player voor 0.5 sec
            playerScript.blinktimer = playerScript.blinkDuration; // start de blink effect
            collisionRb.AddForce(transform.forward * Hitback, ForceMode.Impulse);  // knockback

        }
        else if(other.gameObject.CompareTag("FireAttack") )
        {
            FireAttack fireAttack = other.gameObject.GetComponent<FireAttack>();
            fireAttack.Death();
        }
        else if (other.gameObject.CompareTag("LightningAttack"))
        {
            LightningAttack lightningAttack = other.gameObject.GetComponent<LightningAttack>();
            lightningAttack.Death();

        }
    }

}
