using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLibrary : MonoBehaviour
{
    public static Item[] items = new Item[2000];
    public static Sprite emptySprite;

    public void Awake()
    {
        emptySprite = Resources.Load<Sprite>("Sprites/empty");

        items[1] = new Item(1, "Iron Ore", Resources.Load<Sprite>("Sprites/Items/iron ore"));
        items[2] = new Item(2, "Gold Ore", Resources.Load<Sprite>("Sprites/Items/gold ore"));
        items[3] = new Item(3, "Uncut Opal", Resources.Load<Sprite>("Sprites/Items/opal_uncut"));

        items[1001] = new Item(1001, "Miner", Resources.Load<Sprite>("Sprites/Machines/miner"));
        items[1002] = new Item(1002, "Conveyor", Resources.Load<Sprite>("Sprites/Machines/conveyor"));
        items[1003] = new Item(1003, "Chest", Resources.Load<Sprite>("Sprites/Machines/chest"));
    }

    public void Start()
    {
        
    }

    public static Item GetItem(string name)
    {
        foreach (Item i in items)
        {
            if (i != null)
            {
                if (i.name.Equals(name))
                {
                    return i;
                }
            }
        }

        Debug.Log("Could not find \"" + name + "\"");
        return null;
    }
}
