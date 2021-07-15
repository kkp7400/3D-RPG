using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool isInventory = false;
    public Slot[] slot;
    public Transform slotHolder;
    // Start is called before the first frame update
    public SpellBook DB;
    public Text InventoryGoldCount;
    void Start()
    {
        slot = slotHolder.GetComponentsInChildren<Slot>();
        inventoryPanel.SetActive(isInventory);

        for(int i = 0; i < DB.info.Inventory.Count;i++)
        {
            slot[i].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/"+ DB.info.Inventory[i].ToString());
            slot[i].itemID = DB.info.Inventory[i];
            slot[i].gameObject.GetComponentInChildren<Text>().text = DB.info.itemCount[i].ToString();
            slot[i].itemCount = DB.info.itemCount[i];            
        }
        InventoryGoldCount.text = DB.info.GOLD.ToString();
        for(int i = 0; i < slot.Length;i++)
        {
            if(slot[i].transform.FindChild("Image").GetComponent<Image>().sprite ==null)
            {
                slot[i].itemID = 0;

                slot[i].itemCount = 0;
                slot[i].transform.FindChild("Image").gameObject.SetActive(false);
                slot[i].gameObject.GetComponentInChildren<Text>().gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        InventoryGoldCount.text = DB.info.GOLD.ToString();
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventory = !isInventory;
            inventoryPanel.SetActive(isInventory);
        }        
    }

    public void UseItem(Slot useSlot)
    {
        //if(useSlot.)
        for(int i = 0; i< slot.Length; i++)
        {
            if (slot[i].itemID != 0)
            {

            }
        }
    }
}
