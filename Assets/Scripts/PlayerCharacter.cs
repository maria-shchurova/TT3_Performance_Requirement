using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private HealthDisplay healthDisplay;
    private bool isJumping;
    private bool aboveEnemy;
    private float currentAttack;
/// //////////////////////////////
/// </upgrade booleans>
    private bool _upgradeA = false;
    public bool UpgradeA
    {
        get { return _upgradeA; }
        set { _upgradeA = value; }
    }
    private bool _upgradeB = false;
    public bool UpgradeB
    {
        get { return _upgradeB; }
        set { _upgradeB = value; }
    }

    [SerializeField] private GameObject FireballPrefab;


    protected override void Start()
    {
        base.Start();
        HealthPoints = 5;
        healthDisplay.AddHP(HealthPoints);
    }

    protected override void Update()
    {
        base.Update();
        MovementVector = Input.GetAxisRaw("Horizontal");
        Jump();
        Sprint();
        Animate();
        DetectCollisions();

        if (UpgradeA)
            ShootFireball();
    }

    void Animate()
    {
        // Swap direction of sprite depending on walk direction
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        //Run animation
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Epsilon)
        {
            animationController.SetInteger("AnimState", 1);
        }
        else
            animationController.SetInteger("AnimState", 0);

        //Attack
        if (Input.GetMouseButtonDown(0) && CanAttack)
        {
            currentAttack++;

            // Loop back to one after third attack
            if (currentAttack > 3)
                currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animationController.SetTrigger("Attack" + currentAttack);

            // Reset timer
            CanAttack = false;
            timeElapsed = 0;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animationController.SetTrigger("Jump");
            isJumping = true;
        }
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animationController.SetTrigger("Roll");
            speed *= 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 1.5f;
        }
    }
    void DetectCollisions()
    {
        RaycastHit2D hitRay = Physics2D.Raycast(groundCheck.position, Vector2.down);

        if (hitRay == true)
        {
            if (hitRay.collider.CompareTag("Enemy_A"))
            {
                aboveEnemy = true;
                //takeNoDamage = true;
            } 
            else
                aboveEnemy = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
            isJumping = false;

        if (collision.gameObject.CompareTag("Enemy_A") && aboveEnemy)
            collision.gameObject.GetComponent<Character>().Death();
    }

    public void PlayerTakeHit() //this is being triggered by Unity Animation Event during TakeHit animation( https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html)
    {
        healthDisplay.RemoveHP(); 
    }

    public override void Death()
    {
        base.Death();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); //restart current scene
    }

    [SerializeField]private Transform ProjectileSpawnPoint;
    [SerializeField]private float launchVelocity;
    void ShootFireball()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(FireballPrefab, ProjectileSpawnPoint.position, transform.rotation);
        }
    }
}
