using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameBehavior gameManager = null;
    public Inventory<Item> inventory = null;
    private bool   collectable;
    private string itemName = string.Empty;
    private string description = string.Empty;
    private string icon = string.Empty;

    public Item(string name)
    {
        this.itemName = name;
    }

    private void Start()
    {
        gameManager = GameBehavior.Instance;
        inventory = gameManager.inventory;
        if (inventory == null)
            Debug.LogError("Cannot find Inventory");
        if (gameManager == null)
            Debug.LogError("Cannot find gameManager");
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inventory.Push(this);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
