using System.Collections.Generic;
using UnityEngine;

public class WallObjectSelf : MonoBehaviour
{
    [SerializeField] public float maxHeight = 5f; // 0.6 
    [SerializeField] public float minHeight = -2f; // -1
    [SerializeField] public float mainHeight = 0f;
    [SerializeField] public float ObjectSpeed = 4f;

    public void MoveUp()
    {
        if (transform.position.y < maxHeight)
        {
            transform.position += Vector3.up * ObjectSpeed * Time.deltaTime;
            Debug.Log(" Muur omhoog");
        }
    }

    public void MoveDown()
    {
        if (transform.position.y > minHeight)
        {
            transform.position += Vector3.down * ObjectSpeed * Time.deltaTime;
            Debug.Log("Muur omlaag");
        }
    }

    
    public void Neutral()
    {
        if (transform.position.y >= maxHeight)
        {
            transform.position += Vector3.zero * ObjectSpeed * Time.deltaTime;
            Debug.Log("terug naar normale positie // was boven");
        } else 
        if(transform.position.y <= minHeight)
        {
            transform.position += Vector3.zero * ObjectSpeed * Time.deltaTime;
            Debug.Log("terug naar normale positie // was beneden");
        }
    }

}
