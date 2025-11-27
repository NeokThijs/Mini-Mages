using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    private Rigidbody rb;
    public float DashForce;
    [SerializeField] private float dashCooldown = 3f;
    public float DashDuration = 0.2f;
    [SerializeField] private float currentDashCooldown;
    private PlayerInput PlayerInput;
    private Player PlayerScript;
    public GameObject dashEffect;
    private void Start()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerScript = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        currentDashCooldown -= Time.deltaTime;
        if (currentDashCooldown <= 0)
        {
            //code that let's the player know they can dash again
        }

    }
     public void ExecuteDash(InputAction.CallbackContext context)
    {
        if (context.performed && currentDashCooldown <= 0)
        {
            Instantiate(dashEffect, gameObject.transform.position, gameObject.transform.rotation);
            PlayerScript.canMove = false;
            rb.AddForce(DashForce * transform.forward, ForceMode.VelocityChange);
            currentDashCooldown = dashCooldown;
            Invoke("EndDash", DashDuration);
        }
    }
    public void EndDash()
    {
        PlayerScript.canMove = true;
    }
}
