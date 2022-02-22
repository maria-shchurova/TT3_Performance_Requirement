using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject PlayButton;
    private GameObject CreditsButton;
    private GameObject QuitButton;
    
    void Start()
    {
        PlayButton = GameObject.Find("Play");
        CreditsButton = GameObject.Find("Credits");
        QuitButton = GameObject.Find("Quit");

        PlayButton.GetComponent<Button>().onClick.AddListener(Play);
        CreditsButton.GetComponent<Button>().onClick.AddListener(Credits);
        QuitButton.GetComponent<Button>().onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene(1);
    }

    void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    void Quit()
    {
        Application.Quit();
    }
}
