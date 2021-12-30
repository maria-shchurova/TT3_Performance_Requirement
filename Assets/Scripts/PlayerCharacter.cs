using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    private bool isJumping;
    private bool aboveEnemy;

    protected override void Start()
    {
        base.Start();
        HealthPoints = 10;
    }

    protected override void Update()
    {
        base.Update();
        MovementVector = Input.GetAxisRaw("Horizontal");
        Jump();
        Sprint();
        DetectCollisions();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
        }
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed /= 2;
        }
    }
    void DetectCollisions()
    {
        RaycastHit2D hitRay = Physics2D.Raycast(groundCheck.position, Vector2.down);

        if (hitRay.collider.CompareTag("Enemy"))
            aboveEnemy = true;
        else
            aboveEnemy = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJumping = false;

        if (collision.gameObject.CompareTag("Enemy_A") && aboveEnemy)
            Destroy(collision.gameObject); //TODO add crush animation and call method Death() on enemy
    }
}
