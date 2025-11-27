using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallsManager : MonoBehaviour
{
    enum WallHeight { Normal, Up, Down }
    [SerializeField] private WallHeight state = WallHeight.Normal;
    private float marge = 0.1f;

    [SerializeField] private List<GameObject> walls;
    [SerializeField] private WallObjectSelf currentWall;
    private bool GotWall = false;
    public float MaxWall = 3;
    private bool MaxWallReached = false;
    private float wallsCurrentlyUp;
    private bool NoWallsUp = false;
    private float WallTimer;

    void Update()
    { 
        WallTimer += Time.deltaTime;
        if (WallTimer >= 2f && state != WallHeight.Down)
        {
            GotWall = false;
            currentWall = null;
            state = WallHeight.Up;
            WallTimer = 0;
        }

        if (state == WallHeight.Down)
        {
            bool allAtMin = walls.All(w => {
                var ws = w.GetComponent<WallObjectSelf>();
                return Mathf.Abs(ws.transform.position.y - ws.minHeight) <= marge;
            });

            if (allAtMin || NoWallsUp)
            {
                state = WallHeight.Normal;
                GotWall = false;
                currentWall = null;
                WallTimer = 0;
            }
        }

        if (state == WallHeight.Up)
        {
            bool allAtMax = walls.All(w => {var ws = w.GetComponent<WallObjectSelf>();
                return Mathf.Abs(ws.transform.position.y - ws.maxHeight) <= marge;
            });

            if (allAtMax || MaxWallReached)
            {
                GotWall = false;
                currentWall = null;
                state = WallHeight.Down;
            }
        }

        if (!GotWall && MaxWallReached == false)
        {
            SelectRandomWall();
        }

        switch (state)
        {
            case WallHeight.Up:
                if (currentWall != null)
                {
                    currentWall.MoveUp();
                    wallsCurrentlyUp++;
                    if (currentWall.transform.position.y >= currentWall.maxHeight + marge)
                    {
                        currentWall = null;
                        GotWall = false;
                    }
                }
                break;
            case WallHeight.Down:
                if (currentWall != null)
                {
                    currentWall.MoveDown();
                    wallsCurrentlyUp--;
                    if (currentWall.transform.position.y <= currentWall.minHeight + marge)
                    {
                        currentWall = null;
                        GotWall = false;
                        //Debug.Log("word null en false");
                    }
                }
                break;
            case WallHeight.Normal:
                if (currentWall != null)
                {
                    currentWall.Neutral();
                    if (currentWall.IsResetToNeutral())
                    {
                        currentWall = null;
                        GotWall = false;
                        //Debug.Log("word null en false");
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
