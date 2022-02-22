using UnityEngine.SceneManagement;
using UnityEngine;

public class CreditsToMenu : MonoBehaviour
{
    private float timeElapsed;
    public float timeToRead;

    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeToRead)
        {
            SceneManager.LoadScene("Menu");
        }

        //skip option:

        if(Input.GetKeyDown(KeyCode.Escape)) 
            SceneManager.LoadScene("Menu");
    }
}
