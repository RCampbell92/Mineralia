using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStack : MonoBehaviour
{
    public int stackSize = 64;
    public int numItems = 0;
    public int itemID = 0;
    public Item item;
    public Text txtAmount; 

    // Start is called before the first frame update
    void Start()
    {
        txtAmount = transform.Find("Amount").GetComponent<Text>();
        UpdateItem();
    }

    // Update is called once per frame
    void Update()
    {
        txtAmount.text = "" + numItems;
    }

    void OnMouseDown()
    {

    }

    public void SetItem(int ID, int amount)
    {
        itemID = ID;
        numItems = amount;
        UpdateItem();
    }

    public void UpdateItem()
    {
        item = ItemLibrary.items[itemID];
        if (item != null)
        {
            if (GetComponent<Image>())
            {
                GetComponent<Image>().sprite = item.sprite;
            }
            else if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().sprite = item.sprite;
            }
        }
        else
        {
            if (GetComponent<Image>())
            {
                GetComponent<Image>().sprite = ItemLibrary.emptySprite;
            }
            else if (GetComponent<SpriteRenderer>())
            {
                GetComponent<SpriteRenderer>().sprite = item.sprite;
            }
        }
    }
}
