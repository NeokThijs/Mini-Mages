using UnityEngine;

public class CleanupAfterCompletion : MonoBehaviour
{
    public float lifetime = 2f;
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroys the GameObject after 2 seconds
    }

}
