using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public Sprite sprite;
    public GameObject machine;
    public bool isMachine = false;
    public int amount = 0;

    public Item(int id, string name, Sprite sprite)
    {
        this.id = id;
        this.name = name;
        this.sprite = sprite;
        if (id >= 1000)
        {
            isMachine = true;
        }
        try
        {
            this.machine = Resources.Load<GameObject>("Prefabs/" + name);
        }
        catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    public Item(int id, int amount)
    {
        this.id = id;
        this.amount = amount;
        this.name = ItemLibrary.items[id].name;
        this.sprite = ItemLibrary.items[id].sprite;
    }
}
