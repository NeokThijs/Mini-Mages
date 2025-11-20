using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallsManager : MonoBehaviour
{
    enum WallHeight { Normal, Up, Down }
    [SerializeField] private WallHeight state = WallHeight.Normal;

    [SerializeField] private List<GameObject> walls;
    [SerializeField] private GameObject currentWall;
    private bool GotWall = false;

    void Update()
    {
        // toetsen om te testen
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            state = WallHeight.Up;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            state = WallHeight.Down;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            state = WallHeight.Normal;
        }

        switch (state)
        {
            case WallHeight.Up:
                RandomWallUp();
                break;
            case WallHeight.Down:
                RandomWallDown();
                break;
            case WallHeight.Normal:
                RandomWallStop();
                break;
        }
    }

    private void RandomWallUp()
    {
        if (GotWall == false)
        {
            currentWall = walls[Random.Range(0, walls.Count)];
            WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();
            WOScript.MoveUp();
            GotWall = true;
            if ( currentWall.transform.position.y == WOScript.maxHeight)
            {
                currentWall = null;
                GotWall = false;
            }
        }
    }

    private void RandomWallDown()
    {
        if (GotWall == false)
        {
            currentWall = walls[Random.Range(0, walls.Count)];
            WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();
            WOScript.MoveDown();
            GotWall = true;
            if (currentWall.transform.position.y == WOScript.minHeight)
            {
                currentWall = null;
                GotWall = false;
                Debug.Log("word null en false");
            }
        }
    }

    private void RandomWallStop()
    {
        if(GotWall == false)
        {
            currentWall = walls[Random.Range(0, walls.Count)];
            WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();
            WOScript.Neutral();
            GotWall = true;
            if (currentWall.transform.position.y == WOScript.mainHeight)
            {
                currentWall = null;
                GotWall = false;
                Debug.Log("word null en false");
            }
        }
    }

}
