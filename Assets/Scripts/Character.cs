using UnityEngine;
//parent class for all characters
public class Character : MonoBehaviour 
{
	protected Animator animationController;
	private int _healthPoints = 3;
	[SerializeField]private GameObject attackedCharacter;
	public int HealthPoints
	{
		get
		{
			return _healthPoints;
		}
		set
		{
			if (value < _healthPoints)
			{
				animationController.SetTrigger("Hurt"); //every time HP is decrasing
			}

			_healthPoints = value;

			if (value == 0)
				Death();
		}
	}

	protected Rigidbody2D rb; 
	[SerializeField]
	protected float speed;
	private float initialSpeed;

	protected float MovementVector; /*ranges from -1 to 1. for PlayerCharacter it is taken from horizontal axis input, 
	                                 * for enemies - determined by code(-1 for one who moves left constantly)*/
	protected virtual void Start() 
	{ 
    	rb = GetComponent<Rigidbody2D>();
		animationController = GetComponent<Animator>();
		initialSpeed = speed; //i have to save this in order to set speed from 0 to initioal one after attacking player
	}

	protected virtual void Update()
    {
		Move();
		Attack();
		timeElapsed += Time.deltaTime; //waiting until next attack
		if (timeElapsed >= attackCooldown)
		{
			CanAttack = true;
		}

	}

	void Move() 
	{		
		float moveBy = MovementVector * speed;
		transform.Translate(new Vector2 (moveBy, 0) * speed * Time.deltaTime);
	}

	public void Death()
    {
		animationController.SetTrigger("Death");
		Destroy(gameObject, 1);
	}

	protected float timeElapsed;
	[SerializeField]	private float attackCooldown;

	protected bool CanAttack = true;

	private void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.GetComponent<Character>() != null) //if we collide with anither character  //CompareTag("Player") || collision.gameObject.CompareTag("Enemy_A") || collision.gameObject.CompareTag("Enemy_B")
		{
			attackedCharacter = collision.gameObject;
		}				
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
		if (!this.gameObject.CompareTag("Player")) //if this is NOT player character
		{
			if (collision.gameObject.CompareTag("Player")) //and he touches the player character
			{
				speed = initialSpeed;
			}
		}
		attackedCharacter = null;
	}

	void Attack()
    {
		if (attackedCharacter)
        {
			if (!this.gameObject.CompareTag("Player")) //if this is NOT player character
			{
				speed = 0; //movements stops in order not to push player back
				if (CanAttack)
				{
					animationController.SetTrigger("Attack");
					attackedCharacter.GetComponent<Character>().HealthPoints -= 1; //TODO make a melee attack for player?
					CanAttack = false;
					timeElapsed = 0;
				}
			}
			else //if this is player character
			{
				if (Input.GetMouseButtonDown(0))
				{
					attackedCharacter.GetComponent<Character>().HealthPoints -= 1;
				}
			}
		}
		
	}
}
