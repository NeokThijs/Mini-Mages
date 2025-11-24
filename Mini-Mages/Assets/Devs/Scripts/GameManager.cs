using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> playersInGame;
    [SerializeField] private List<Transform> SpawnpointsPlayers;
    [SerializeField] private float PlayerAmount = 4;

    [Header("UI (Settings)")]
    [SerializeField] private TMPro.TextMeshProUGUI roundText;
    private float roundAmount = 3;
    private bool isRoundActive = false;

    [SerializeField] private GameObject StartUI;
    [SerializeField] private TMPro.TMP_InputField playerAmountIF; // IF = inputfield
    public float removeStartUI = 0;
    
    void Start()
    {
        StartUI.SetActive(true);
        Time.timeScale = 0f; // stop the game  
    }

    void Update()
    {
        if (isRoundActive && playersInGame.Count == 1)
        {
            StartNewRound();
        }

        removeStartUI += Time.unscaledDeltaTime; // timer voor geen pauze dr in

        if (removeStartUI >= 12)
        {
            StartUI.SetActive(false);
            Time.timeScale = 1f; // continue game

            if (!isRoundActive)
            {
                StartNewRound();
                isRoundActive = true;
            }
        }

    }

    public void UIPlayerAmount()
    {
        int UIPlayerAmount = int.Parse(playerAmountIF.text);
        PlayerAmount = UIPlayerAmount;
    }

    private void StartNewRound()
    {
        PlayerObjToList();
        SpawnPlayers();
    }

    private void PlayerObjToList()
    {
        // zoekt de objecten met de tag player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        playersInGame.AddRange(players);
        Debug.Log("Voegt player toe a/d list");
    }

    private void SpawnPlayers()
    {
        for ( int i = 0; i < PlayerAmount; i++)
        {
            Instantiate(player, SpawnpointsPlayers[Random.Range(0, SpawnpointsPlayers.Count)].transform.position, Quaternion.identity);
            Debug.Log("Spawn players");
        }

        
    }

    private void RoundsUI()
    {
        roundText.text = "Round: " + roundAmount;
    }
}
