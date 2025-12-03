using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] public List<GameObject> playersInGame;
    [SerializeField] private List<Transform> SpawnpointsPlayers;
    [SerializeField] private float PlayerAmount = 4;

    [SerializeField] private CinemachineTargetGroup targetGroup;

    [Header("UI (Settings)")]
    [SerializeField] private TMPro.TextMeshProUGUI roundText;
    private float roundAmount = 0;
    public bool isRoundActive = false;

    [SerializeField] private GameObject StartUI;
    [SerializeField] private TMPro.TMP_InputField playerAmountIF; // IF = inputfield
    public float removeStartUI = 0;

    [SerializeField] private GameObject Leaderboard;
    [SerializeField] private TMPro.TextMeshProUGUI roundResultText;
    [SerializeField] private TMPro.TextMeshProUGUI leaderboardText;
    public float showLeaderboard = 10;

    [Header("Player Registration")]
    public GameObject[] players; 
    public Player[] playerScripts;
    private string[] playerNames = { "Red", "Blue", "Yellow", "Purple" };
    public string winningPlayer;

    [Header("Winner Registration")]
    public int[] PlayerWins; // element 0 = red t/m 3 = green
    public bool WinnerInRound = false;

    [Header("Managers")]
    public WallsManager WallsManager;
    public PlayerManager PlayerManager;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartUI.SetActive(true);
        Leaderboard.SetActive(false);
        Time.timeScale = 0f; // stop the game  
        WallsManager = FindAnyObjectByType<WallsManager>();
    }

    void Update()
    {
        if (playersInGame != null)
        {
            playersInGame.RemoveAll(player => player == null);
        }

        if (isRoundActive && playersInGame.Count == 1) // als geen ronde active is en telt hoeveel playerInGame zitten ( objecten)
        {
            WallsManager.RoundTimer = 0;
            winningPlayer = playersInGame[0].name;
            RemoveAllPickups();
            if (PlayerWins[0] == 3)
            {
                SceneManager.LoadScene("EndScreen");
            }
            ShowLeaderboard();
        }

        removeStartUI += Time.unscaledDeltaTime; // timer voor geen pauze dr in

        if (removeStartUI >= 12)
        {
            StartUI.SetActive(false);
            Time.timeScale = 1f; // continue game
            WallsManager.WallsUpAndDown();

            if (isRoundActive == false)
            {
                isRoundActive = true;
                StartNewRound();
            }
        }
    }

    public void UIPlayerAmount()
    {
        int UIPlayerAmount = int.Parse(playerAmountIF.text);
        if (UIPlayerAmount <= 1)
        {
            UIPlayerAmount = 2;
        }
        else
        if (UIPlayerAmount > 4)
        {
            UIPlayerAmount = 4;
        }

        PlayerAmount = UIPlayerAmount;
        Debug.Log("spelers voor de ronde: " + PlayerAmount);
    }

    public void StartNewRound()
    {
        isRoundActive = true;
        RemoveOldPlayers();
        Leaderboard.SetActive(false);
        Time.timeScale = 1f;
        roundAmount += 1;
        for (int i = 0; i < playerScripts.Length; i++)
        {
            playerScripts[i].isDead = false;
        }
            Debug.Log("Nieuwe ronde start");
        RoundsUI();
        SpawnPlayers();
        PlayerObjToList();
    }

    private void PlayerObjToList()
    {
        // zoekt de objecten met de tag player
        players = new GameObject[] 
            { 
                GameObject.FindGameObjectWithTag("Player1"), 
                GameObject.FindGameObjectWithTag("Player2"), 
                GameObject.FindGameObjectWithTag("Player3"), 
                GameObject.FindGameObjectWithTag("Player4")
            }; // alle players zoeken
        playerScripts = new Player[]
            {
                players[0].GetComponent<Player>(),
                players[1].GetComponent<Player>(),
                players[2].GetComponent<Player>(),
                players[3].GetComponent<Player>()
            }; // alle player scripts zoeken
        for (int i = 0; i < players.Length; i++)
        {
            playersInGame.Add(players[i]);
            targetGroup.AddMember(players[i].transform, 1, 0.5f);
            Debug.Log("Voegt player toe a/d list");
        }
    }

    private void RemoveOldPlayers()
    {
        for (int i = 0; i < playersInGame.Count; i++)
        {
            Destroy(playersInGame[i]);
        }
        playersInGame.Clear();
    }

    private void SpawnPlayers()
    {
        isRoundActive = true;
        
        List<Transform> availableSpawns = new List<Transform>(SpawnpointsPlayers); // nieuwe list

        for (int i = 0; i < PlayerAmount; i++)
        {

            if (availableSpawns.Count == 0)
            {
                Debug.LogWarning("Niet genoeg spawnpoints voor alle spelers!");
                break;
            }

            int index = Random.Range(0, availableSpawns.Count); // checkt ofdat er plek is
            Transform spawnPoint = availableSpawns[index];

            GameObject spawnedPlayer = Instantiate(player, spawnPoint.position, Quaternion.identity);
            Debug.Log("Spawn player");
            spawnedPlayer.name = playerNames[i]; // spawned 1 player extra dus moet ff gefixed worden

            availableSpawns.RemoveAt(index);
        }
    }

    private void RemoveAllPickups()
    {
        GameObject[] pickup2Remove = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < pickup2Remove.Length; i++)
        {
            Destroy(pickup2Remove[i]);
            Debug.Log("verwijder de pickups bro");
        }
    }

    private void RoundsUI()
    {
        roundText.text = "Round: " + roundAmount;
    }

    private void ShowLeaderboard()
    {
        Leaderboard.SetActive(true);
        TextsUpdate();
        if (!WinnerInRound)
        {
            AddWinToPlayer();
        }
        Time.timeScale = 0f;
        showLeaderboard -= Time.unscaledDeltaTime;

        if (showLeaderboard <= 0)
        {
            Leaderboard.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Nieuwe ronde start");
            RoundsUI();
            isRoundActive = false;
            WinnerInRound = false;
        }
    }

    private void TextsUpdate()
    {
        roundResultText.text = "Round " + roundAmount + " winner";
        leaderboardText.text = winningPlayer;
    }

    private void AddWinToPlayer()
    {
        WinnerInRound = true;
        if (winningPlayer == playerNames[0]) // win naar rood
        {
            PlayerWins[0] += 1;
            Debug.Log("Rood wint");
        }
        if (winningPlayer == playerNames[1]) // win naar blauw
        {
            PlayerWins[1] += 1;
            Debug.Log("Blauw wint");
        }
        if (winningPlayer == playerNames[2]) // win naar geel
        {
            PlayerWins[2] += 1;
            Debug.Log("Geel wint");
        }
        if (winningPlayer == playerNames[3]) // win naar groen
        {
            PlayerWins[3] += 1;
            Debug.Log("Groen wint");
        }
    }

}
