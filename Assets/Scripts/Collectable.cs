using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private string collectableType; //"Coin", "UpgradeA" or "UpgradeB"
    private ItemDisplay display;

    private void Start()
    {
        display = GameObject.FindObjectOfType<ItemDisplay>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnCollect(collectableType);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollect(string collectableType)
    {
        display.UpdateItems(collectableType);
    }
}
