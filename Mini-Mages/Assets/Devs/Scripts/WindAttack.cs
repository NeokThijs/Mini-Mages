using Unity.VisualScripting;
using UnityEngine;

public class WindAttack : Attack
{
    public float MinObjSpeed;
    public float MaxObjSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        YPos = transform.position.y;

        //if (Input.GetKey(KeyCode.Backspace))
        //{
        //    UseAttack();
        //}
    }


    public override void UseAttack()
    {
        base.UseAttack();

        Instantiate(gameObject, transform.position, Quaternion.identity);
        rb.MovePosition(Vector3.forward * ObjSpeed * Time.deltaTime);

        // beweegt naar de kant de player op kijkt ( nu ff naar voren)
        // snelheid neemt enorm toe tot een bepaald getal
        // als die dat getal heeft gehaald
        // dan gaat ie afremmen ( langzamer dan dat de snelheid toe nam)



    }


    
}
