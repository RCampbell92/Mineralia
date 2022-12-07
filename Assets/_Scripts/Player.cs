using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int iron = 0;
    public Text txtIron;
    public GameObject chestInfoPanel;
    public int GUITimer = 0;

    public bool placingPart = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtIron.text = "Iron: " + iron;

        if (Input.GetMouseButtonDown(0))
        {
            placingPart = false;
        }

        //close chest
        if (GUITimer > 3)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                chestInfoPanel.SetActive(false);
                GUITimer = 0;

                for (int i = 0; i < Chest.NUM_SLOTS; i++)
                {
                    //clear contents of chest panel
                    chestInfoPanel.transform.Find("Slot " + i).Find("Item").GetComponent<ItemStack>().SetItem(0, 0);
                }
            }
        }

        //GUI timer
        if (chestInfoPanel.activeInHierarchy)
        {
            GUITimer++;
        }
    }
}
