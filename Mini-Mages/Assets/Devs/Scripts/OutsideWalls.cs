using UnityEngine;

public class OutsideWalls : MonoBehaviour
{
    [SerializeField] public float minHeight = -2f; // -1
    [SerializeField] public float ObjectSpeed = 1f;

    private Vector3 m_StartPos;

    private void Start()
    {
        m_StartPos = transform.position;
    }

    public void MoveDown()
    {
        transform.position = Vector3.Lerp(transform.position, m_StartPos + new Vector3(0, minHeight, 0), Time.deltaTime * ObjectSpeed);
    }
}
