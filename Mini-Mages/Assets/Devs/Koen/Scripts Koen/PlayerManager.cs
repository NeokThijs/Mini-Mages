using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public string[] playerTags;

    public void SetSpawn(PlayerInput player)
    {
        player.gameObject.transform.position = spawnPoints[player.playerIndex].position;
        player.gameObject.tag = playerTags[player.playerIndex];
    }
}
