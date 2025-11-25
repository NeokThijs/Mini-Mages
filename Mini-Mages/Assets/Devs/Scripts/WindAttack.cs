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


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ObjectSpeed = AttackSpeed;
        if(rb != null)
        {
            rb.AddForce(transform.forward * ObjectSpeed); // lerpen
        }
    }

    private void Update()
    {
        CountTillDT += Time.deltaTime;

        if(CountTillDT >= DestroyTime)
        {
            Destroy(gameObject);
        }

        YPos = transform.position.y;

        if (Keyboard.current != null && Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            UseAttack();
        }

        //if (ObjectSpeed >= MaxObjSpeed)
        //{
        //    ObjectSpeed -= lessSpeedPSec * Time.deltaTime;
        //}
        ObjectSpeed -= lessSpeedPSec ;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3") || other.gameObject.CompareTag("Player4"))
        {
            // jij geeft de player +1 knockback

            Debug.Log("Hapetee lekker voor je, een knockback erbij");
            Rigidbody collisionRb = other.gameObject.GetComponent<Rigidbody>();
            collisionRb.AddForce(transform.forward * Hitback, ForceMode.Impulse);  // knockback

        }
    }

}
