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

    [Header ("Destroy Time")]
    public float CountTillDT;
    public float DestroyTime = 6;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ObjectSpeed = AttackSpeed;
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

        

    }


    public override void UseAttack()
    {
        base.UseAttack();

        UsedAttacks++;

        if (UseTheAttack == UsedAttacks)
        {
            // dan moet ie gewoon verwijderd worden uit inventory achtig iets???
        }

        rb.AddForce(transform.forward * ObjectSpeed);

        if ( ObjectSpeed >= MaxObjSpeed )
        {
            ObjectSpeed -= lessSpeedPSec * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            // jij geeft de player +1 knockback

            Debug.Log("Hapetee lekker voor je, een knockback erbij");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(gameObject.transform.forward * Hitback, ForceMode.Impulse);

        }
    }

}
