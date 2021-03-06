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
            if(HealthPoints > 0) //wal only if not dying XD
                animationController.SetTrigger("Walk");
            MovementVector = 1; //constatly moving left - flip in Editor if needed
        }
    }

    void Wait()
    {
        animationController.SetTrigger("Idle");
        MovementVector = 0;
    }
}
