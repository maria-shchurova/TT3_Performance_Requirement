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
    private float sizeScale;
    /////////////////////////////////
    ///projectile
    [SerializeField] private Transform ProjectileSpawnPoint;
    [SerializeField] private Transform ProjectileRotator;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject damageAreaPrefab;
    /// //////////////////////////////
    /// </upgrade booleans>
    private bool _upgradeA = false;
    public bool UpgradeA
    {
        get { return _upgradeA; }
        set 
        { 
            _upgradeA = value; 
            if(value == false)
            {
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
            if (value == false)
            {
                if(itemDisplay != null)
                    itemDisplay.UpdateItems("UpdateB_Lost"); //remove item from UI
            }
        }
    }


    protected override void Start()
    {
        base.Start();
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        //WARNING next 3 lines cause Death and hence reloading if levels higher than 0 are played separately. Comment them to have 5 HP every time when starting a level
        if (SceneManager.GetActiveScene().name != "Level_0") //is the level is not the first, HP are loaded from StatsKeeper 
        {
            HealthPoints = StatsKeeper.HealthPointsCount;
            if(StatsKeeper.PlayerCharacterScale > 0) //if there is some data saved
                transform.localScale = new Vector3(StatsKeeper.PlayerCharacterScale, StatsKeeper.PlayerCharacterScale, 1); //loading player size if it was changed due to upgrades
        }
        else
        {
            HealthPoints = 5; //otherwise 5
            healthDisplay.AddHP(HealthPoints);
        }

        itemDisplay = FindObjectOfType<ItemDisplay>();
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
            ProjectileRotator.eulerAngles = new Vector3 (0, 90, 0); 
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            ProjectileRotator.eulerAngles = new Vector3(0, -90, 0); //flip spawn point for fireballs to face player's direction
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
            speed *= 1.25f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 1.25f;
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
        {
            transform.localScale /= 1.2f; //back to initial size
            UpgradeA = false; //after hit upgrade is lost
        }
        

        if (UpgradeB)
        {
            transform.localScale /= 1.2f; //back to initial size
            UpgradeB = false;
        }    
    }

    public override void Death()
    {
        base.Death();
        SceneManager.LoadScene("Menu");
    }

    void ShootFireball()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(fireballPrefab, ProjectileSpawnPoint.position, ProjectileSpawnPoint.rotation);
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

    public void Upgrade_onCollect() //this method is needet to ditinguish between collecting upgrade or setting it to true via loading from StatsKeeper.
    {
        transform.localScale *= 1.2f; //grows in size
        sizeScale = transform.localScale.x; //cashing variable for statsKeeper
        HealthPoints += 1;            //gains a life
        healthDisplay.AddHP(1);
    }
    void OnSceneUnloaded(Scene current) //called before next level is loaded
    {
        StatsKeeper.PlayerCharacterScale = sizeScale;
    }
}
