using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject Wack;
    [SerializeField] public GameObject SpecialAttack;
    [SerializeField] private Transform PlaceAttack;
    
    public float AttackAmount;

    private void Update()
    {
        CheckAttack();
    }
    private void CheckAttack() // kijkt of er een special attack is, en of die nog niet op is
    {
        if(SpecialAttack != null)
        {
            if (AttackAmount <= 0f)
            {
                SpecialAttack = null;
            }
        }
        else
        {
            return;
        }
    }

    public void UseAttack(InputAction.CallbackContext context) // attack gebruiken
    {
        // gebruik attack / spawnen
        // telt 1 ervanaf, als ie tot 0 is dan verwijderen
        if (context.performed)
        {
        if (SpecialAttack != null && AttackAmount > 0)
        {
            GameObject Attack = Instantiate(SpecialAttack, PlaceAttack.position, PlaceAttack.rotation);
                Attack.layer = LayerMask.NameToLayer(gameObject.tag + "Attack"); //zet de layer van de attack naar de player tag + attack
                Attack.transform.parent = null; //remove the parent of the attack
                Debug.Log("special attack gebruikt");
            AttackAmount --; //attack charges -1
            Debug.Log(AttackAmount + "charges left");
        }
        else if (SpecialAttack == null)
        {
            Instantiate(Wack, PlaceAttack.position, PlaceAttack.rotation);
        }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // de pickup oppakken die een tag heeft van pickup
        // attack v/d pickup word in de special attack dr ingezet
        //if (collision.gameObject.CompareTag("Pickup") == false)
        //{
        //    return;
        //}
        //else if (collision.gameObject.CompareTag("Pickup"))
        //{
        //    Pickup pickupScript = collision.gameObject.GetComponent<Pickup>();
        //    if (SpecialAttack != null)
        //    {
        //        SpecialAttack = pickupScript.AttackObject;
        //        Debug.Log("attack vervangen");
        //    }

        //}
    }

}
