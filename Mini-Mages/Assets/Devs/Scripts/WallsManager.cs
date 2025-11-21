using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallsManager : MonoBehaviour
{
    enum WallHeight { Normal, Up, Down }
    [SerializeField] private WallHeight state = WallHeight.Normal;

    [SerializeField] private List<GameObject> walls;
    [SerializeField] private WallObjectSelf currentWall;
    private bool GotWall = false;

    private float WallTimer;

    void Update()
    {
        // toetsen om te testen
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            GotWall = false;
            currentWall = null;
            state = WallHeight.Up;
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            GotWall = false;
            currentWall = null;

            state = WallHeight.Down;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            GotWall = false;
            currentWall = null;

            state = WallHeight.Normal;
        }

        WallTimer += Time.deltaTime;

        if (WallTimer >= 5)
        {
            GotWall = false;
            currentWall = null;
            state = WallHeight.Up;
            WallTimer = 0;
        } 

        //if (walls[0,5].gameObject.transform.position == currentWall.maxHeight) // als alle muren op die positie staan dat ie dan naar beneden begint te gaan
        //{
        //    GotWall = false;
        //    currentWall = null;
        //    state = WallHeight.Down;
        //}

        switch (state)
        {
            case WallHeight.Up:
                SelectRandomWall();
                if (currentWall != null)
                {
                    currentWall.MoveUp();
                    if (currentWall.transform.position.y >= currentWall.maxHeight)
                    {
                        currentWall = null;
                        GotWall = false;
                    }
                }
                break;
            case WallHeight.Down:
                SelectRandomWall();
                if (currentWall != null)
                {
                    currentWall.MoveDown();
                    if (currentWall.transform.position.y <= currentWall.minHeight)
                    {
                        currentWall = null;
                        GotWall = false;
                        Debug.Log("word null en false");
                    }
                }
                break;
            case WallHeight.Normal:
                SelectRandomWall();
                if (currentWall != null)
                {
                    currentWall.Neutral();
                    if (currentWall.IsResetToNeutral())
                    {
                        currentWall = null;
                        GotWall = false;
                        Debug.Log("word null en false");
                    }
                }
                break;
        }
    }

    private void SelectRandomWall()
    {
        if (GotWall == false)
        {
            currentWall = walls[Random.Range(0, walls.Count)].GetComponent<WallObjectSelf>();
            GotWall = true;
        }
    }
}
