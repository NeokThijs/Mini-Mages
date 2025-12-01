using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallsManager : MonoBehaviour
{
    enum WallHeight { Normal, Up, Down }
    [SerializeField] private WallHeight state = WallHeight.Normal;
    private float marge = 0.1f;

    [SerializeField] private List<GameObject> walls;
    [SerializeField] private WallObjectSelf currentWall;
    [SerializeField] private WallObjectSelf lastWall;
    private bool GotWall = false;
    public float MaxWall = 3;
    public float WallTimer;
    public float TimerInterval;
    [SerializeField] private List<GameObject> StartGameWalls = new List<GameObject>();

    void Start()
    {
        WallsUpBeginGame();
    }

    void Update()
    {
        WallTimer += Time.deltaTime;
        if (WallTimer >= TimerInterval && state != WallHeight.Down)
        {
            lastWall = currentWall;
            GotWall = false;
            currentWall = null;
            state = WallHeight.Up;
            WallTimer = 0;
        }
        if (WallTimer >= TimerInterval && state != WallHeight.Up)
        {
            lastWall = currentWall;
            GotWall = false;
            currentWall = null;
            state = WallHeight.Down;
            WallTimer = 0;
        }

        if (state == WallHeight.Down)
        {
            bool allAtMin = walls.All(w =>
            {
                var ws = w.GetComponent<WallObjectSelf>();
                return Mathf.Abs(ws.transform.position.y - ws.minHeight) <= marge;
            });

            if (allAtMin)
            {
                Debug.Log("All walls at min height, switching to Normal state.");
                state = WallHeight.Normal;
                GotWall = false;
                currentWall = null;
                WallTimer = 0;
            }
        }

        if (state == WallHeight.Up)
        {
            bool allAtMax = walls.All(w =>
            {
                var ws = w.GetComponent<WallObjectSelf>();
                return Mathf.Abs(ws.transform.position.y - ws.maxHeight) <= marge;
            });

            if (allAtMax)
            {
                Debug.Log("All walls at max height, switching to Down state.");
                GotWall = false;
                currentWall = null;
                state = WallHeight.Down;
            }
        }

        if (!GotWall && currentWall == null)
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
                        Debug.Log("Wall reached max height.");
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
                        Debug.Log("Wall reached min height.");
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
                        Debug.Log("Wall reset to neutral.");
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
            Debug.Log("Got Wall");
            if (lastWall != currentWall)
            {
                GotWall = true;
            }
        }
    }
    private void WallsUpBeginGame()
    {
        if (StartGameWalls.Count == 0)
        {
            for (int i = 0; i < MaxWall; i++)
            {
                var wall = walls[Random.Range(0, walls.Count)];
                StartGameWalls.Add(wall);
            }
        }

        foreach (var wall in StartGameWalls)
        {
            wall.transform.position = new Vector3(wall.transform.position.x, 0.6f, wall.transform.position.z);
        }
    }
}
