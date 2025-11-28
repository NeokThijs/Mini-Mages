using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public Vector3 Movement;
    [SerializeField] private PlayerInput playerInputObject;
    public float moveSpeed = 10f;
    public float lookSpeed = 10f;
    public bool canMove = true;
    private Dash dashBrain;
    private SkinnedMeshRenderer meshRenderer;
    public Material[] colors;
    private Rigidbody rb;
    public bool hitByFire = false;
    private float fireDuration = 5f;
    private float fireTimer = 0f;
    public GameObject GnomeColor;
    public GameObject StaffColor;
    private float localStunDuration;
    private Collider Collider;
    public Animator playerAnimator;
    public bool Grounded;
    public GameObject normalSteps;
    public GameObject fireSteps;

    [SerializeField] private float blinkIntensity;
    [SerializeField] public float blinkDuration;
    [SerializeField] public float blinktimer;

    private ChangeKnockback changeKnockBack;


    void Start()
    {
        changeKnockBack = GetComponent<ChangeKnockback>();
        rb = GetComponent<Rigidbody>();
        dashBrain = GetComponent<Dash>();
        if (GnomeColor != null)
        { 
        meshRenderer = GnomeColor.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material = colors[playerInputObject.playerIndex];
        }
        if (StaffColor != null)
        {
            MeshRenderer StaffMesh = StaffColor.GetComponent<MeshRenderer>();
            StaffMesh.material = colors[playerInputObject.playerIndex];
        }
        Collider = GetComponent<Collider>();
        Collider.excludeLayers = 1 << LayerMask.NameToLayer(gameObject.tag + "Attack");
        rb.excludeLayers = 1 << LayerMask.NameToLayer(gameObject.tag + "Attack");
    }

    // Update is called once per frame
    void Update()
    {
        blinktimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinktimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        meshRenderer.material.color = Color.white * intensity;

        if (hitByFire == true)
        {
            fireSteps.SetActive(true);
            normalSteps.SetActive(false);
        }
        else if (hitByFire == false)
        {
            fireSteps.SetActive(false);
            normalSteps.SetActive(true);
        }
        // Build a world-space movement vector (y=0 to keep movement on the XZ plane)
        Vector3 worldMove = new Vector3(Movement.x, 0f, Movement.z);

        // If there is movement, rotate to face the movement direction.
        if (worldMove.sqrMagnitude > 0.0001f)
        {
             Quaternion targetRotation = Quaternion.LookRotation(worldMove.normalized);
             // Smoothly rotate toward the target rotation. For instant rotation use: transform.rotation = targetRotation;
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
        }
        // Move in world space so changing the GameObject's rotation does NOT change the movement direction.
        transform.Translate(worldMove * Time.deltaTime * moveSpeed, Space.World);
        //rb.AddForce(worldMove * moveSpeed, ForceMode.Acceleration);
        if (hitByFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireDuration)
            {
                hitByFire = false;
                fireTimer = 0f;
            }
            assOnFire();
        }
        if(localStunDuration > 0)
        {
            localStunDuration -= Time.deltaTime;
            if(localStunDuration <= 0)
            {
                canMove = true;
            }
        }


        playerAnimator.SetFloat("Speed", Movement.magnitude);
        if (Grounded == false)
        {
            playerAnimator.SetBool("InAir", true);
        }
        else if (Grounded == true)
        {
            playerAnimator.SetBool("InAir", false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove == true)
        {
            Movement.x = context.ReadValue<Vector2>().x;
            Movement.z = context.ReadValue<Vector2>().y;
        }
        else if (canMove == false)
        {
            Movement.x = 0f;
            Movement.z = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("hit wall");
            canMove = false;
            hitByFire = false;
            Movement = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
        }
        else if (collision.gameObject.CompareTag("FireAttack"))
        {
            blinktimer = blinkDuration;
            hitByFire = true;
        }
        if (collision.gameObject.CompareTag("DeathWater"))
        {
            RemoveFromPlayerList();
            Destroy(gameObject);
        }
    }

    public void assOnFire()
    {
        if (hitByFire == true)
        {
            playerAnimator.SetTrigger("AssOnFire");
            rb.AddForce(10f * transform.forward, ForceMode.Acceleration);
        }
    }

    public void tempStun(float stunDuration)
    {
        canMove = false;
        localStunDuration = stunDuration;
    }

    public void RemoveFromPlayerList()
    {
        //GameManager.instance.playersInGame.Remove(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Grounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Grounded = false;
    }
    public void Drown()
    {
        playerAnimator.SetTrigger("Drowning");
    }
}