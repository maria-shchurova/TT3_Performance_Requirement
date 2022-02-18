using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Enemy_type_A[] enemiesToActivate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(Enemy_type_A enemy in enemiesToActivate)
            {
                if(enemy != null)
                    enemy.isWaiting = false;
            }

        }
    }
}
