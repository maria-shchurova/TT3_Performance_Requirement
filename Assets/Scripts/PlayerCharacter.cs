using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField]
    private float jumpForce;
    private bool isJumping;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        MovementVector = Input.GetAxisRaw("Horizontal");
        Jump();
        Sprint();
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
