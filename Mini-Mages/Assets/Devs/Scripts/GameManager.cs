using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> playersInGame;
    [SerializeField] private List<Transform> SpawnpointsPlayers;
    [SerializeField] private float PlayerAmount = 4;

    // testUI
    [SerializeField] private TMPro.TextMeshPro roundText;
    private float roundAmount = 3;
    private bool isRoundActive = true;

    void Start()
    {
       
        SpawnPlayers();
    }

    void Update()
    {
        if (isRoundActive && playersInGame.Count <= 1)
        {
            StartNewRound();
        }
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
    }

    private void SpawnPlayers()
    {
        for ( int i = 0; i < PlayerAmount; i++)
        {
            Instantiate(player, SpawnpointsPlayers[Random.Range(0, SpawnpointsPlayers.Count)].transform.position, Quaternion.identity);
        }

        
    }

    private void RoundsUI()
    {
        roundText.text = "Round: " + roundAmount;
    }
}
