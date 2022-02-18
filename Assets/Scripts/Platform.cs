using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private PlayerCharacter player;
    private bool isPlayerOn;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerCharacter>();
        effector = GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.F) && player.UpgradeB == true) //F for stomp attack
                                                                                                  //TODO add && if player.isJumping...
        {
            effector.rotationalOffset = 180;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.F))
        {
            effector.rotationalOffset = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerOn = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isPlayerOn = false;
    }
}
