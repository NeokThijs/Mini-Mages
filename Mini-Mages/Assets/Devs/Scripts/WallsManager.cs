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
        currentWall = walls[Random.Range(0, walls.Count)];
        WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();

        WOScript.MoveUp();
    }

    private void RandomWallDown()
    {
        currentWall = walls[Random.Range(0, walls.Count)];
        WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();

        WOScript.MoveDown();
    }

    private void RandomWallStop()
    {
        currentWall = walls[Random.Range(0, walls.Count)];
        WallObjectSelf WOScript = currentWall.GetComponent<WallObjectSelf>();

        WOScript.Neutral();
    }

}
