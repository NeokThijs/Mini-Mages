using UnityEngine;

public class FireAttack : Attack
{
    [Header ("Fire Attack")]
    public float ObjectSpeed;



    [SerializeField] private Transform testWaypoint; // de player er naar toe laten lopen

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
            collision.gameObject.transform.position = Vector3.forward * ObjectSpeed; // test om naar een transform toe te lopen
            Debug.Log("hij beweegt een kant op");

            Destroy(gameObject);

            //instantiate animatie
        }
    }

}
