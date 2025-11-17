using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 Movement;
    [SerializeField] private PlayerInput playerInputObject;
    public float moveSpeed = 10f;
    public float lookSpeed = 10f;
    public bool canMove = true;
    private Dash dashBrain;
    private MeshRenderer MeshRenderer;
    public Material[] colors;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
        dashBrain = GetComponent<Dash>();
        MeshRenderer.material = GetComponent<MeshRenderer>().material = colors[playerInputObject.playerIndex];
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (canMove == true)
        {
            Movement.x = context.ReadValue<Vector2>().x;
            Movement.z = context.ReadValue<Vector2>().y;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") && rb.linearVelocity.z >= 10)
        {
            canMove = false;
        }
    }
}