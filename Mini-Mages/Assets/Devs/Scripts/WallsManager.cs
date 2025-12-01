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
    private List<WallObjectSelf> currentWallsGroup = new List<WallObjectSelf>();
    private List<GameObject> WallsGoUp = new List<GameObject>();

    private HashSet<WallObjectSelf> usedWalls = new HashSet<WallObjectSelf>(); // voor de gebruikte muren

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
            SelectRandomWalls();
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

    private void SelectRandomWalls()
    {
        // maakt list aan voor muren die nog niet zijn geweest
        var availableWalls = walls
            .Select(w => w.GetComponent<WallObjectSelf>()) // w is de muur 
            .Where(w => !usedWalls.Contains(w))
            .ToList();

        if (availableWalls.Count == 0)
        {
            // als alle muren zijn geweest verwijderd ie ze allemaal/leegt ie de list
            usedWalls.Clear();
            availableWalls = walls.Select(w => w.GetComponent<WallObjectSelf>()).ToList();
        }

        // pakt random muur van de overige muren
        var newWall = availableWalls[Random.Range(0, availableWalls.Count)];

        currentWall = newWall;
        GotWall = true;
        usedWalls.Add(newWall); // de muur die is gebruikt
        Debug.Log("Got Wall: " + currentWall.name);
    }

    private void WallsUpBeginGame()
    {
        if (WallsGoUp.Count == 0)
        {
            var alreadyUsed = new HashSet<GameObject>(); // maakt list aan voor de muren die al zijn gebruikt

            while (WallsGoUp.Count < MaxWall)
            {
                var wall = walls[Random.Range(0, walls.Count)];

                if (alreadyUsed.Contains(wall))
                {
                    continue; // sla dubbele over
                }

                WallsGoUp.Add(wall);
                alreadyUsed.Add(wall);
            }
        }

        foreach (var wall in WallsGoUp) // loopt door de objecten heen
        {
            WallObjectSelf WOScript = wall.GetComponent<WallObjectSelf>();

            wall.transform.position = new Vector3(wall.transform.position.x, WOScript.maxHeight, wall.transform.position.z); // plaats ze op de correcte hoogte
        }
    }

    // uitvoering voor in code
    // pakt 3 muren van de muren die je kan pakken
    // die 3 muren moeten per stuk omhoog gaan
    // per stuk omlaag als alles omhoog is
    // pakt dan 3 andere muren zolang het niet 2 of meer dezelfde zijn
}
