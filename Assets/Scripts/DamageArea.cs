using UnityEngine;

public class DamageArea : MonoBehaviour
{
    void Start() //TODO add vfx
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy_A") || collision.gameObject.CompareTag("Enemy_B")) 
            collision.gameObject.GetComponent<Character>().Death(); //kill all enemies in this zone
    }
}
