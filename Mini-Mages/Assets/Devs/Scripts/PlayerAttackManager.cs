using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject Wack;
    [SerializeField] private GameObject SpecialAttack;

    public int NoAttacksLeft = 0;
    public int AttackAmount = 3;

    private void Update()
    {
        CheckAttack();
    }
    private void CheckAttack() // komt later
    {
        if (SpecialAttack == null)
        {

        }
    }

    private void UseAttack() // attack gebruiken
    {
        // gebruik attack / spawnen
        // telt 1 ervanaf, als ie tot 0 is dan verwijderen
    }

    private void OnCollisionEnter(Collision collision)
    {
        // de pickup oppakken die een tag heeft van pickup
        // attack v/d pickup word in de special attack dr in gezet
    }

}
