using System.Collections.Generic;
using UnityEngine;

public class WallObjectSelf : MonoBehaviour
{
    [SerializeField] public float maxHeight = 5f; // 0.6 
    [SerializeField] public float minHeight = -2f; // -1
    [SerializeField] public float mainHeight = 0f;
    [SerializeField] public float ObjectSpeed = 1f;
    [SerializeField] public float DownObjectSpeed = 1f;

    private Vector3 m_StartPos;

    private void Start()
    {
        m_StartPos = transform.position;
    }

    public void MoveUp()
    {
        if (transform.position.y <= maxHeight)
        {
            transform.position = Vector3.Lerp(transform.position, m_StartPos + new Vector3(0, maxHeight, 0), Time.deltaTime * ObjectSpeed);
            //Debug.Log(" Muur omhoog");
        }
    }

    public void MoveDown()
    {
        if (transform.position.y >= minHeight)
        {
            transform.position = Vector3.Lerp(transform.position, m_StartPos + new Vector3(0, minHeight, 0), Time.deltaTime * DownObjectSpeed);
            //Debug.Log("Muur omlaag");
        }
    }


    public void Neutral()
    {
        transform.position = Vector3.Lerp(transform.position, m_StartPos, Time.deltaTime * ObjectSpeed);
    }

    public bool IsResetToNeutral()
    {
        if (transform.position == m_StartPos) return true;
        return false;
    }
}
