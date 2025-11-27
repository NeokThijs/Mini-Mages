using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour
{
    private Whack WhackScript;
    [SerializeField] private GameObject Whacker;
    [SerializeField] public GameObject SpecialAttack;
    [SerializeField] private Transform PlaceAttack;
    [SerializeField] private float attackCooldown;
    private float currentAttackCooldown;
    private float whackTiming = 1.28f;
    private float currentWhackTiming;
    private Animator animator;

    public float AttackAmount;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        WhackScript = Whacker.GetComponent<Whack>();
    }

    private void Update()
    {
        CheckAttack();
        if(currentAttackCooldown >= 0f)
        {
            currentAttackCooldown -= Time.deltaTime;
        }
        currentWhackTiming -= Time.deltaTime;
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
        if (context.performed && currentAttackCooldown <= 0)
        {
            startCooldown();
            if (SpecialAttack != null && AttackAmount > 0)
            {
                animator.SetTrigger("Cast");
                GameObject Attack = Instantiate(SpecialAttack, PlaceAttack.position, PlaceAttack.rotation);
                    Attack.layer = LayerMask.NameToLayer(gameObject.tag + "Attack"); //zet de layer van de attack naar de player tag + attack
                    Attack.transform.parent = null; //remove the parent of the attack
                    Debug.Log("special attack gebruikt");
                AttackAmount --; //attack charges -1
                Debug.Log(AttackAmount + "charges left");
            }
            else if (SpecialAttack == null && currentWhackTiming <= 0)
            {
                currentWhackTiming = whackTiming;
                animator.SetTrigger("Whack");
                WhackScript.StartWhacking();
                //Instantiate(Wack, PlaceAttack.position, PlaceAttack.rotation);
            }
        }
    }
    private void startCooldown()
    {
        currentAttackCooldown = attackCooldown;
    }

}
