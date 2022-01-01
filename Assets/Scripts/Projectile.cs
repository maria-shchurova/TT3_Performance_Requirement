using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player")) //player is the first collision for projectile - so it has to ignore it
            Destroy(gameObject, 1); //TODO: add effects and make a method to desttoy

        if (collision.gameObject.CompareTag("Enemy_B")) 
            collision.gameObject.GetComponent<Character>().Death();
    }
}
