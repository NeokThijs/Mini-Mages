using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;

    private float ObjectSpeed = 4f;

    private bool IsWallUp = false;
    private bool IsWallDown = false;

    private float RotateWallTimer = 3;
    
    void Start()
    {
        
    }

    void Update()
    {
        RotateWallTimer -= Time.deltaTime;

        if (RotateWallTimer <= 0)
        {
            YAsWallUp();
            RotateWallTimer = 3;
        }
    }

    private void YAsWallUp()
    {
        Vector3 WallUp = new Vector3(transform.position.x, 1, transform.position.z);

        if( IsWallUp == false && IsWallDown == false)
        {
            walls[Random.Range(0, walls.Count)].transform.position = Vector3.MoveTowards(walls[Random.Range(0, walls.Count)].transform.position, WallUp, ObjectSpeed * Time.deltaTime);
            IsWallUp = true;
        }
        
    }

    private void YAsWallDown()
    {
        Vector3 WallDown = new Vector3(transform.position.x, -2, transform.position.z);

        if (IsWallDown == false && IsWallUp == false)
        {
            walls[Random.Range(0, walls.Count)].transform.position = Vector3.MoveTowards(walls[Random.Range(0, walls.Count)].transform.position, WallDown, ObjectSpeed * Time.deltaTime);
            IsWallDown = true;
        }
    }
}
