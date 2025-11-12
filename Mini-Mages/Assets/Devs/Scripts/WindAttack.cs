using Unity.VisualScripting;
using UnityEngine;

public class WindAttack : Attack
{
    public float MinObjSpeed;
    public float MaxObjSpeed;

    private void Update()
    {
        YPos = transform.position.y;

        if (Input.GetKey(KeyCode.Escape))
        {
            UseAttack();
        }
    }


    public override void UseAttack()
    {
        base.UseAttack();

        transform.Translate(Vector3.forward * ObjSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, YPos);

    }


    
}
