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
	}

	void Move() 
	{
		float moveBy = MovementVector * speed;  
	    rb.velocity = new Vector2(moveBy, rb.velocity.y); 
	}
}
