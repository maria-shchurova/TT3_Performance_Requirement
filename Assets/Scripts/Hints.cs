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
            hintText.transform.localScale = new Vector3 (1.5f, 1.5f, 0); //because of a short text
            hintUI.SetActive(true);
        } 
    }

    private bool alreadyApproached = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EnemyTrigger" && !alreadyApproached)
        {
            hintText.text = "The Shroom. He is strong, but a jump on his head kills him";
            hintText.transform.localScale = new Vector3(1f, 1f, 0); //because of a long text TODO make a separate class for rescaling ui
            hintUI.SetActive(true);
            alreadyApproached = true; //to not show it again
        }

        if (collision.gameObject.name == "GoblinHint")
        {
            hintText.text = "A Goblin. I can slay it easy with my magic power";
            hintText.transform.localScale = new Vector3(1f, 1f, 0); //because of a long text TODO make a separate class for rescaling ui
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
