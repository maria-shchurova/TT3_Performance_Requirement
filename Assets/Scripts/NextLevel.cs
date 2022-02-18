using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //if(SceneManager.GetActiveScene().buildIndex < 4) //if it is not the last level
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //load next scene by its index in Build Settings
            //else
                SceneManager.LoadScene(nextSceneName); //easier to manage
        }
        
    }
}
