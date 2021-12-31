using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private string _collectableType; //"Coin", "UpgradeA", "LargeCoin" etc
    private ItemDisplay display;

    private void Start()
    {
        display = GameObject.FindObjectOfType<ItemDisplay>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnCollect(_collectableType);
            Destroy(gameObject);
        }
    }

    protected virtual void OnCollect(string collectableType)
    {
        display.UpdateItems(collectableType);
    }
}
