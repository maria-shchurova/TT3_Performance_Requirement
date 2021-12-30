using UnityEngine;

public class Enemy_type_A : Character
{

    protected override void Update()
    {
        base.Update();
        MovementVector = 1; //constatly moving left, but it is flipped because of animation i used
    }

}
