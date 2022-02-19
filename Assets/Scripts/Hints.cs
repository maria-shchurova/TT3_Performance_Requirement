using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour //only for tutorial level
{
    [SerializeField] private GameObject hintUI;
    [SerializeField] private Text hintText;
    [SerializeField] private float timeToRead;
    private float timeElapsed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "invisible wall")
        {
            hintText.text = "There is no way back!";
            hintUI.SetActive(true);
        }    
    }

    private void Update()
    {
        if (hintUI.activeInHierarchy)
        {
            timeElapsed += Time.deltaTime; //waiting until next attack
            if (timeElapsed >= timeToRead)
            {
                hintUI.SetActive(false);
                timeElapsed = 0;
            }
        }
    }
}
