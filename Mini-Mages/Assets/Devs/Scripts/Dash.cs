using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody rb;
    public float DashForce;
    public void ExecuteDash()
    {
        rb.AddForce(rb.transform.forward * DashForce);
    }
}
