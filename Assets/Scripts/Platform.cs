using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private bool isPlayerOn;
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            effector.rotationalOffset = 180;
        }
        if (Input.GetKeyUp(KeyCode.S))
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
