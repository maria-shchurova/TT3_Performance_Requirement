using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private ItemDisplay itemDisplay;
    private bool isJumping;
    private bool aboveEnemy;
    private float currentAttack;
/// //////////////////////////////
/// </upgrade booleans>
    private bool _upgradeA = false;
    public bool UpgradeA
    {
        get { return _upgradeA; }
        set 
        { 
            _upgradeA = value; 
            if(value == true)
            {
                transform.localScale *= 1.2f; //grows in size
                HealthPoints += 1;            //gains a life
                healthDisplay.AddHP(1);
            }
            else
            {
                transform.localScale /= 1.2f; //back to initial size
                if (itemDisplay != null)
                    itemDisplay.UpdateItems("UpdateA_Lost"); //remove item from UI
            }
        }
    }
    private bool _upgradeB = false;
    public bool UpgradeB
    {
        get { return _upgradeB; }
        set 
        { 
            _upgradeB = value;
            if (value == true)
            {
                transform.localScale *= 1.2f; //grows in size
                HealthPoints += 1;            //gains a life
                healthDisplay.AddHP(1);
            }
            else
            {
                transform.localScale /= 1.2f; //back to initial size
                if(itemDisplay != null)
                    itemDisplay.UpdateItems("UpdateB_Lost"); //remove item from UI
            }
        }
    }

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject damageAreaPrefab;


    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetActiveScene().name != "Level_0") //is the level is not the first, HP are loaded from StatsKeeper
            HealthPoints = StatsKeeper.HealthPointsCount;
        else
            HealthPoints = 5; //otherwise 5
        healthDisplay.AddHP(HealthPoints);

        itemDisplay = GameObject.FindObjectOfType<ItemDisplay>();
;    }

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

        if (UpgradeB)
            StompAttack();
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
            if (hitRay.collider.CompareTag("Enemy_A")) //if player is above enemy, so enemy A is in a vulnerable position
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
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") 
            || collision.gameObject.CompareTag("Enemy_A") || collision.gameObject.CompareTag("Enemy_B"))
            isJumping = false;

        if (collision.gameObject.CompareTag("Enemy_A") && aboveEnemy)
            collision.gameObject.GetComponent<Character>().Death(); //kill Schroom(enemy A) immediately if player is colliding it from above

        if (stompJump)
        {
            if (collision.gameObject.tag != "Ground")
            {
                //preventing getting stack on enemie's heads while stomp attack
                if(collision.gameObject.GetComponent<BoxCollider2D>())
                    Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>()); 
            }

        }
    }
        

    public void PlayerTakeHit() //this is being triggered by Unity Animation Event during TakeHit animation( https://docs.unity3d.com/Manual/script-AnimationWindowEvent.html)
    {
        healthDisplay.RemoveHP();
        if (UpgradeA)
            UpgradeA = false; //after hit upgrade is lost

        if (UpgradeB)
            UpgradeB = false;
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
            Instantiate(fireballPrefab, ProjectileSpawnPoint.position, transform.rotation);
        }
    }


    [SerializeField] private bool stompJump = false;
    [SerializeField] private float stompJumpSpeed = 85;
    void StompAttack()
    {
        if(stompJump && !isJumping) //if attack started AND player finished the jump
        {
            Instantiate(damageAreaPrefab, transform.position, transform.rotation); //killing area appears
            rb.gravityScale /= stompJumpSpeed; // goes back to initial
            stompJump = false;
        }
        if (Input.GetKeyDown(KeyCode.F)) //F because player can have Upgrade A in the same time
        {
            if (isJumping)
            {
                rb.gravityScale *= stompJumpSpeed; //landing faster
                stompJump = true; //attack is activated
            }
        }
    }
}
