using UnityEngine;
//parent class for all characters
public class Character : MonoBehaviour 
{
	protected Animator animationController;
	private int _healthPoints = 1; //at least 1 
	public int HealthPoints
	{
		get
		{
			return _healthPoints;
		}
		set
		{
			_healthPoints = value;
			if (value == 0)
				Death();
		}
	}

	private bool _isAlive;

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
		if (!this.gameObject.CompareTag("Player")) //if this is NOT player character
		{
			if(collision.gameObject.CompareTag("Player")) //and he touches the player character
            {
				speed = 0; //movements stops in order not to push player back
				animationController.SetTrigger("Idle"); //looks better than walking without movement
				if (CanAttack)
                {
					animationController.SetTrigger("Attack");
					collision.gameObject.GetComponent<Character>().HealthPoints -= 1; //TODO make a melee attack for player?
					CanAttack = false;
					timeElapsed = 0;
				}
			}				
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
	}
}
