using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Pickup : MonoBehaviour
{
    [Header("Base Var")]
    [SerializeField] public GameObject AttackObject;

    public virtual void Activate(GameObject player)
    {
        player.GetComponent<PlayerAttackManager>().SpecialAttack = AttackObject;
        player.GetComponent<PlayerAttackManager>().AttackAmount = 3;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag ("Player2") || other.gameObject.CompareTag("Player3") || other.gameObject.CompareTag("Player4"))
        {
            Activate(other.gameObject);
            Destroy(gameObject);
        }
    }
}
