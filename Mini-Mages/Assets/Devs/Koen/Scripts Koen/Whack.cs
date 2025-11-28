using UnityEngine;

public class Whack : MonoBehaviour
{
    public float hitStrength = 10f;
    public bool IsWhacking = false;
    public GameObject whackEffect;
    private ChangeKnockback changeKnockback;

    private void Start()
    {
        changeKnockback = GetComponentInParent<ChangeKnockback>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        if(otherRb != null && IsWhacking == true )
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
                Player playerScript = other.GetComponent<Player>();
                playerScript.tempStun(0.1f);
                ChangeKnockback knockbackScript = other.gameObject.GetComponent<ChangeKnockback>();
                knockbackScript.GetHit(); // andere speler's knockback verhogen
                changeKnockback.Hit(); // eigen knockback verlagen
                playerScript.blinktimer = playerScript.blinkDuration;
                if (otherRb.gameObject.CompareTag("Player1") || otherRb.gameObject.CompareTag("Player2") || otherRb.gameObject.CompareTag("Player3") || otherRb.gameObject.CompareTag("Player4"))
                {
                    Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                    otherRb.AddForce(forceDirection * hitStrength * knockbackScript.KnockBackStrength, ForceMode.Impulse);
                }
            }
            else
            {
                return;
            }
        }
    }
    public void StartWhacking()
    {
        IsWhacking = true;
        whackEffect.SetActive(true);
        Invoke("EndWhacking", 1.28f);
    }
    public void EndWhacking()
    {
        whackEffect.SetActive(false);
        IsWhacking = false;
    }   
}
