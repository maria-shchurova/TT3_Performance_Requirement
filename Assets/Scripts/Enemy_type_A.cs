using UnityEngine;

public class Enemy_type_A : Character
{
    protected override void Start()
    {
        base.Start();
        HealthPoints = 10;  //it has a lot of HP but can immedeately die after jumping on his head
    }

    protected override void Update()
    {
        base.Update();
        MovementVector = 1; //constatly moving left, but it is flipped because of animation i used
    }

}
