using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Pickup : MonoBehaviour
{
    [Header("Base Var")]
    [SerializeField] public GameObject AttackObject;

    public virtual void Activate()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
