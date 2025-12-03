using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XInput;

public class PlayerManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public string[] playerTags;
    public InputDevice[] inputDevices;

    private void Start()
    {
    }
    public void SetSpawn(PlayerInput player)
    {
        player.gameObject.transform.position = spawnPoints[player.playerIndex].position;
        player.gameObject.tag = playerTags[player.playerIndex];
    }
}
