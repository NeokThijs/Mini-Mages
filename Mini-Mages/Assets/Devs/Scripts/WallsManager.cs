using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WallsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;

    private float Up_Y = 0.4f;
    private float Down_Y = -2f;

    private bool IsWallUp = false;
    private bool IsWallDown = false;

    private float RotateWallTimer = 5;
    
    void Start()
    {
        
    }

    void Update()
    {
        RotateWallTimer -= Time.deltaTime;

        if (RotateWallTimer < 0)
        {
            YAsWallUp();
            RotateWallTimer = 5;
        }
    }

    private void YAsWallUp()
    {
        Vector3 WallUp = new Vector3(transform.position.x, Up_Y, transform.position.z);

        if( IsWallUp == false && IsWallDown == false)
        {
            walls[Random.Range(0, walls.Count)].transform.position = WallUp;
            IsWallUp = true;
        }
        
    }

    private void YAsWallDown()
    {
        Vector3 WallDown = new Vector3(transform.position.x, Down_Y, transform.position.z);

        if (IsWallDown == false && IsWallUp == false)
        {
            transform.position = WallDown;
        }
    }
}
