using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private GameBehavior gameManager = null;
    private Inventory<Item> inventory = null;
    private bool   collectable;
    private string name = string.Empty;
    private string description = string.Empty;
    private string icon = string.Empty;
    private Texture2D texture;
    public Coordinate position;

    public Item(string name)
    {
        this.name = name;
    }

    public Item(string name, string icon, bool collectable = true, string description = "")
    {
        gameManager = GameBehavior.Instance;
        inventory = gameManager.inventory;
        if (inventory == null)
            Debug.LogError("Cannot find Inventory");
        if (gameManager == null)
            Debug.LogError("Cannot find gameManager");

        this.name = name;
        this.icon = icon;
        texture = null;
        texture = Resources.Load<Texture2D>(FilePaths.items + this.icon);

        this.collectable = collectable;
        this.description = description;
    }

    /*
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inventory.Push(this);
            Destroy(this.transform.parent.gameObject);
        }
    }*/
}
