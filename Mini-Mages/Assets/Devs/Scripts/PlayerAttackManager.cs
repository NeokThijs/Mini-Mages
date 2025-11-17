using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject Wack;
    [SerializeField] private GameObject SpecialAttack;

    [SerializeField] private Transform PlaceAttack;

    private int NoAttacksLeft = 0;
    private int AttackAmount = 3;

    private void Update()
    {
        CheckAttack();
    }
    private void CheckAttack() // komt later
    {
        if (SpecialAttack != null && AttackAmount >= 1)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) // moet ergens anders komen, zit hier ff voor de "kloppende code"
            {
                AttackAmount -= 1;
                UseAttack();
            }
        }
    }

    private void UseAttack() // attack gebruiken
    {
        // gebruik attack / spawnen
        // telt 1 ervanaf, als ie tot 0 is dan verwijderen
        Instantiate(SpecialAttack, PlaceAttack.position, Quaternion.identity);
        if (AttackAmount <= NoAttacksLeft)
        {
            SpecialAttack = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // de pickup oppakken die een tag heeft van pickup
        // attack v/d pickup word in de special attack dr ingezet

        Pickup pickupScript = other.GetComponent<Pickup>();

        

        if ( SpecialAttack != null)
        {
            SpecialAttack = null;
            if (other.gameObject.CompareTag("Pickup"))
            {
                SpecialAttack = pickupScript.AttackObject;
                Debug.Log("attack vervangen");
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Pickup"))
            {
                SpecialAttack = pickupScript.AttackObject;
                Debug.Log("attack erbij");
            }
        }
    }

}
