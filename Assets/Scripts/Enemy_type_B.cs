using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_type_B : Character
{
    private bool movingRight = true;
    [SerializeField] private Transform groundCheck;

    protected override void Start()
    {
        base.Start();
        HealthPoints = 15;
        MovementVector = 1; //initially moving right
    }
    protected override void Update()
    {
        base.Update();

        RaycastHit2D groungHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.5f);
        RaycastHit2D sideHit = Physics2D.Raycast(groundCheck.position, Vector2.right, 0.5f);

        if(groungHit.collider == false)
        {
            if(movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (sideHit.collider && sideHit.collider.CompareTag("Wall"))
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

}
