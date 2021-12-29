using UnityEngine;

public class Enemy_type_A : Character
{

    protected override void Update()
    {
        base.Update();
        MovementVector = -1; //constatly moving left
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attack");
        }
    }
}
