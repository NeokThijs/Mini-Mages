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
        foreach(var g in  XInputController.all)
        {
            Debug.Log("xinput found: "+ g.deviceId);
            Debug.Log(g.device.enabled);
        }
        
    }
    public void SetSpawn(PlayerInput player)
    {

        //Debug.Log("gamepads: "+ Gamepad.all.ToString());
        //Debug.Log("xinputs: "+ XInputController.all.ToString());

        player.gameObject.transform.position = spawnPoints[player.playerIndex].position;
        player.gameObject.tag = playerTags[player.playerIndex];
    }
}
