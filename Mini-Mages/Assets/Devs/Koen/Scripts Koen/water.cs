using UnityEngine;

public class water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3") || other.gameObject.CompareTag("Player4"))
        {
            
            if (other.GetComponent<Player>() != null)
            {
                Player player = other.GetComponent<Player>();
                player.Drown();
            }
        }
    }
}
