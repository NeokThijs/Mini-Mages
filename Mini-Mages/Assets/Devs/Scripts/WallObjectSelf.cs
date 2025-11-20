using System.Collections.Generic;
using UnityEngine;

public class WallObjectSelf : MonoBehaviour
{
    [SerializeField] private float maxHeight = 5f; // 0.6 
    [SerializeField] private float minHeight = -2f; // -1
    [SerializeField] private float ObjectSpeed = 4f;

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
        if (transform.position.y == maxHeight)
        {
            transform.position += Vector3.down * ObjectSpeed * Time.deltaTime;
            Debug.Log("terug naar normale positie // was boven");
        }
        if (transform.position.y == minHeight)
        {
            transform.position += Vector3.up * ObjectSpeed * Time.deltaTime;
            Debug.Log("terug naar normale positie // was beneden");
        }
    }

}
