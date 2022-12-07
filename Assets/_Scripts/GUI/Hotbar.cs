using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public int activeSlot = 0;
    public Sprite sprActive, sprInactive;
    public Player player;
    public float machineRotation = 0.0f;

    GameObject[] slots;
    Text txtCurrentItem;
    bool placingPart = false;
    GameObject currentMachine;

    // Start is called before the first frame update
    void Start()
    {
        slots = new GameObject[5];
        slots[0] = transform.Find("Slot 0").gameObject;
        slots[1] = transform.Find("Slot 1").gameObject;
        slots[2] = transform.Find("Slot 2").gameObject;
        slots[3] = transform.Find("Slot 3").gameObject;
        slots[4] = transform.Find("Slot 4").gameObject;
        txtCurrentItem = transform.Find("Current Item").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SetActiveSlot(activeSlot - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SetActiveSlot(activeSlot + 1);
        }

        //Place active machine
        if (Input.GetMouseButtonDown(1))
        {
            PlaceMachine(currentMachine);
        }
    }

    public Item GetActiveItem()
    {
        return slots[activeSlot].transform.Find("Item").GetComponent<ItemStack>().item;
    }

    public void SetActiveSlot(int slot)
    {
        activeSlot = slot;
        //Wrap around when scrolling through slots
        if(activeSlot > 4)
        {
            activeSlot = 0;
        }
        if (activeSlot < 0)
        {
            activeSlot = 4;
        }

        //Set images of active/inactive slots
        for(int i = 0; i < 5; i++)
        {
            if (i == activeSlot)
            {
                slots[i].GetComponent<Image>().sprite = sprActive;
                StartPlacingItem();
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = sprInactive;
                //StartPlacingItem();
            }
        }

        //Set text to curent item
        ItemStack stack = slots[activeSlot].transform.Find("Item").GetComponent<ItemStack>();
        if (stack.item != null)
        {
            txtCurrentItem.text = stack.item.name;
        }
        else
        {
            txtCurrentItem.text = "";
        }
        
    }

    public void StartPlacingItem()
    {
        //destroy any previous machine
        if (currentMachine != null)
        {
            if (Game.machines.Contains(currentMachine.GetComponent<Machine>()))
            {
                Game.machines.Remove(currentMachine.GetComponent<Machine>());
            }
            Destroy(currentMachine);
        }
        //if active slot is a placable item
        //instantiate item
        //set item's placing variable to true
        if (GetActiveItem() != null)
        {
            GameObject mach = GetActiveItem().machine;
            if (mach != null)
            {
                currentMachine = Instantiate(mach, transform.position, Quaternion.identity);
                Machine machine = currentMachine.GetComponent<Machine>();
                if (Game.machines.Contains(machine) == false) //if machine not found in game machine list
                {
                    //Game.machines.Add(machine); //add machine to list
                }
                machine.placing = true;
                machine.SetRotation(machineRotation); //rotate machine to current rotation for consistency
            }
        }
        else if (currentMachine != null)
        {
            if (Game.machines.Contains(currentMachine.GetComponent<Machine>()))
            {
                Game.machines.Remove(currentMachine.GetComponent<Machine>());
            }
            Destroy(currentMachine);
        }
    }

    public void PlaceMachine(GameObject machine)
    {
        bool canPlace = true;

        //if machine is in active slot
        if (machine != null)
        {
            foreach(Machine m in Game.machines)
            {
                if (m != machine.GetComponent<Machine>())
                {
                    //if there's not already a machine in that position
                    if (Vector3.Distance(machine.transform.position, m.transform.position) < 0.1f)
                    {
                        print(Vector3.Distance(machine.transform.position, m.transform.position));
                        canPlace = false;
                        break;
                    }
                }
            }

            if (canPlace)
            {
                machine.GetComponent<Machine>().placing = false; //stop machine from following mouse
                Game.machines.Add(machine.GetComponent<Machine>());
                currentMachine = null;
                StartPlacingItem(); //spawn new machine
            }
        }
    }
}
