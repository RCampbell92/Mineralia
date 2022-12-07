using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Machine
{
    public const int NUM_SLOTS = 21;

    public Item[] items = new Item[NUM_SLOTS];

    GameObject GUI;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        SetRotation(0.0f);

        GUI = GameObject.Find("Canvas").transform.Find("Chest Info Panel").gameObject;

        items[0] = new Item(1, 10);
        items[1] = new Item(2, 64);
        items[2] = new Item(3, 1);
        items[17] = new Item(1001, 5);
        items[18] = new Item(1002, 32);
        items[19] = new Item(1003, 12);

        //additem test
        AddItem(1, 10);
        AddItem(2, 10);
        AddItem(3);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    //On click, open GUI
    private void OnMouseDown()
    {
        base.OnMouseDown();
    }

    private void OnMouseOver()
    {
        //press E to open
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (!placing)
            {
                GUI.SetActive(true);
                GetContents();
            }
        }
    }

    void GetContents()
    {
        for(int i = 0; i < NUM_SLOTS; i++)
        {
            if (items[i] != null)
            {
                GUI.transform.Find("Slot " + i).Find("Item").GetComponent<ItemStack>().SetItem(items[i].id, items[i].amount);
                print("Item " + i + ": " + items[i].name);
            }
        }
    }

    public void AddItem(int id, int amount = 1)
    {
        //get next available item
        Item item = GetNextAvailableSlot(id);
        if (item != null)
        {
            //add items to stack
            item.amount += amount;

            //if stack amount is more than 64
            if (item.amount > 64)
            {
                //fill another stack
                Item item2 = GetNextAvailableSlot(id);
                if (item2 != null)
                {
                    //add remaining amount
                    item2.amount += item.amount - 64;
                }
            }
        }
    }

    Item GetNextAvailableSlot(int itemID)
    {
        //look for slot with same item
        for (int i = 0; i < NUM_SLOTS - 1; i++)
        {
            //if item found
            if (items[i] != null)
            {
                if (items[i].id == itemID)
                {
                    //if num items is less than stack size
                    if (items[i].amount < 64)
                    {
                        //use this stack
                        return items[i];
                    }
                }
            }
        }

        //if there's no non-full stacks, then look for an empty slot
        for (int i = 0; i < NUM_SLOTS - 1; i++)
        {
            //look for next empty space
            if (items[i] == null)
            {
                items[i] = new Item(itemID, 0);
                return items[i];
            }
        }

        //return null if there's no available spaces
        return null;
    }

    protected override void OnPlace()
    {
        base.OnPlace();

        print("Chest OnPlace");
        //get conveyors connected to chest
        GetConveyors();
    }

    protected override void RotateCW()
    {
        
    }

    protected override void RotateACW()
    {
        
    }
}
