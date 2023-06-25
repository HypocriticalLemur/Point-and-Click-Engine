using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory<T>
{
    private Dictionary<T, int> inventory = new();
    /*
    public static Inventory<T> Instance = null;
    public void Awake()
    {
        if (Instance == null)
            Instance = this;
    }*/

    public void Push(T item)
    {
        if (!inventory.ContainsKey(item))
            inventory[item] = 1;
        else
            inventory[item]++;
    }

    public T Pop(T item)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] > 1)
                inventory[item]--;
            else
                inventory.Remove(item);
            return item;
        }
        return default;
    }
}
