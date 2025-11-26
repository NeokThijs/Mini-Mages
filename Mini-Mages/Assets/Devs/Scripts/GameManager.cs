using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] public List<GameObject> playersInGame;
    [SerializeField] private List<Transform> SpawnpointsPlayers;
    [SerializeField] private float PlayerAmount = 4;

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
    private string[] playerNames = { "Red", "Blue", "Yellow", "Green" };
    public string winningPlayer;

    [Header("Winner Registration")]
    public int[] PlayerWins; // element 0 = red t/m 3 = green
    public bool WinnerInRound = false;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartUI.SetActive(true);
        Leaderboard.SetActive(false);
        Time.timeScale = 0f; // stop the game  
    }

    void Update()
    {
        if (playersInGame != null)
        {
            playersInGame.RemoveAll(player => player == null);
        }

        if (isRoundActive && playersInGame.Count == 1) // als geen ronde active is en telt hoeveel playerInGame zitten ( objecten)
        {
            winningPlayer = playersInGame[0].name;
            RemoveAllPickups();
            ShowLeaderboard();
        }

        removeStartUI += Time.unscaledDeltaTime; // timer voor geen pauze dr in

        if (removeStartUI >= 12)
        {
            StartUI.SetActive(false);
            Time.timeScale = 1f; // continue game

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
        isRoundActive = false;
        RemoveOldPlayers();
        Leaderboard.SetActive(false);
        Time.timeScale = 1f;
        roundAmount += 1;
        Debug.Log("Nieuwe ronde start");
        RoundsUI();
        SpawnPlayers();
        PlayerObjToList();
    }

    private void PlayerObjToList()
    {
        // zoekt de objecten met de tag player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); // alle players zoeken
        for (int i = 0; i < players.Length; i++)
        {
            playersInGame.Add(players[i]);
        }
        Debug.Log("Voegt player toe a/d list");
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
        for (int i = 0; i < PlayerAmount; i++)
        {
            GameObject spawnedPlayer = Instantiate(player, SpawnpointsPlayers[Random.Range(0, SpawnpointsPlayers.Count)].transform.position, Quaternion.identity);
            Debug.Log("Spawn player");
            spawnedPlayer.name = playerNames[i]; // spawned 1 player extra dus moet ff gefixed worden
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
            roundAmount += 1;
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
        //for (int i = 0; i < PlayerWins.Length;)
        //{
        //    if (PlayerWins[i] == 3)
        //    {
        //        SceneManager.LoadScene("EndScreen");
        //    }
        //}
    }

}
