using UnityEngine;
//parent class for all characters
public class Character : MonoBehaviour 
{
    private int _healthPoints;

	public int HealthPoints
	{
		get
		{
			return _healthPoints;
		}
		set
		{
			_healthPoints = value;

		}
	}

	private bool _isAlive;

	public bool IsAlive
	{
		get
		{
			return _isAlive;
		}
		set
		{
			_isAlive = value;
		}
	}
	protected Rigidbody2D rb; 
	[SerializeField]
	protected float speed;

	protected float MovementVector; /*ranges from -1 to 1. for PlayerCharacter it is taken from horizontal axis input, 
	                                 * for enemies - determined by code(-1 for one who moves left constantly)*/
	protected virtual void Start() 
	{ 
    	rb = GetComponent<Rigidbody2D>(); 
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

	void Death()
    {

    }

	protected float timeElapsed;
	[SerializeField]	private float attackCooldown;

	private bool CanAttack = true;

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (!this.gameObject.CompareTag("Player")) //if this is any enemy (NOT player character)
		{
			if(collision.gameObject.CompareTag("Player")) //and he touches the player character
            {
				if(CanAttack)
                {
					collision.gameObject.GetComponent<Character>().HealthPoints -= 1;
					CanAttack = false;
					timeElapsed = 0;
				}
			}				
		}
	}
}
