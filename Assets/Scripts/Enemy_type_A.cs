using UnityEngine;

public class Enemy_type_A : Character
{
    public bool isWaiting = true;
    protected override void Start()
    {
        base.Start();
        HealthPoints = 10;  //it has a lot of HP but can immedeately die after jumping on his head
    }

    protected override void Update()
    {
        if (isWaiting) //will switch by EnemyTrigger.cs
            Wait();
        else
        {
            base.Update();
            animationController.SetTrigger("Walk");
            MovementVector = 1; //constatly moving left, but it is flipped because of animation i used
        }
    }

    void Wait()
    {
        animationController.SetTrigger("Idle");
        MovementVector = 0;
    }
}
