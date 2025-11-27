using UnityEngine;

public class Whack : MonoBehaviour
{
    public float hitStrength = 10f;
    public bool IsWhacking = false;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if(otherRb != null && IsWhacking == true )
        {
            if (otherRb.gameObject.CompareTag("Player1") || otherRb.gameObject.CompareTag("Player2") || otherRb.gameObject.CompareTag("Player3") || otherRb.gameObject.CompareTag("Player4"))
            {
            Vector3 forceDirection = (other.transform.position - transform.position).normalized;
            otherRb.AddForce(forceDirection * hitStrength, ForceMode.Impulse);
            }
        }
    }
    public void StartWhacking()
    {
        IsWhacking = true;
        Invoke("EndWhacking", 1.28f);
    }
    public void EndWhacking()
    {
        IsWhacking = false;
    }   
}
