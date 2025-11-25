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

            if (allAtMin)
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

            if (allAtMax)
            {
                GotWall = false;
                currentWall = null;
                state = WallHeight.Down;
            }
        }

        if (!GotWall)
        {
            SelectRandomWall();
        }

        switch (state)
        {
            case WallHeight.Up:
                if (currentWall != null)
                {
                    currentWall.MoveUp();
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
