using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI_Equip : MonoBehaviour
{
    public GameObject EquipPanel;
    bool isInventory = false;
    public Transform slotHolder;
    public UI_Equip_Slot[] slot;
    public UI_Inventory inventory;
    public GameObject[] status;
    public DataBase DB;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Canvas").GetComponent<UI_Inventory>();
        slot = slotHolder.GetComponentsInChildren<UI_Equip_Slot>();
        EquipPanel.SetActive(isInventory);
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        //赣府0 公扁1 个2 脚惯3
        if (DB.info.Equip_Head != 0)
        {
            slot[0].itemID = DB.info.Equip_Head;
            slot[0].EquipPart = "Head";
            slot[0].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[0].itemID.ToString());
        }
        if (DB.info.Equip_Staff != 0)
        {
            slot[1].itemID = DB.info.Equip_Staff;
            slot[1].EquipPart = "Staff";
            slot[0].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[0].itemID.ToString());
        }
        if (DB.info.Equip_Body != 0)
        {
            slot[2].itemID = DB.info.Equip_Body;
            slot[2].EquipPart = "Body";
            slot[0].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[0].itemID.ToString());
        }
        if (DB.info.Equip_Foot != 0)
        {
            slot[3].itemID = DB.info.Equip_Foot;
            slot[3].EquipPart = "Foot";
            slot[0].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[0].itemID.ToString());
        }

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.FindChild("Image").GetComponent<Image>().sprite == null)
            {
                slot[i].itemID = 0;
                slot[i].transform.FindChild("Image").gameObject.SetActive(false);
            }
        }
        status[0].GetComponent<Text>().text = "LV: " + DB.info.Level.ToString();
        for (int i = 0; i < DB.itemDB.Count; i++)
        {
            if (i > DB.itemDB.Count) continue;
            if (slot[0].itemID == DB.itemDB[i].ID)
            {
                status[1].GetComponent<Text>().text = "HP: " + DB.itemDB[i].HP.ToString();
            }
            else
            {
                status[1].GetComponent<Text>().text = "HP: 0";
            }
            if (slot[1].itemID == DB.itemDB[i].ID)
            {
                status[2].GetComponent<Text>().text = "MP: " + DB.itemDB[i].MP.ToString();
            }
            else
            {
                status[2].GetComponent<Text>().text = "MP: 0";
            }
            if (slot[2].itemID == DB.itemDB[i].ID)
            {
                status[3].GetComponent<Text>().text = "ATK: " + DB.itemDB[i].ATK.ToString();
            }
            else
            {
                status[3].GetComponent<Text>().text = "ATK: 0";
            }
            if (slot[3].itemID == DB.itemDB[i].ID)
            {
                status[4].GetComponent<Text>().text = "SPEED: " + DB.itemDB[i].SPEED.ToString();
            }
            else
            {
                status[4].GetComponent<Text>().text = "SPEED: 0";
            }
                status[5].GetComponent<Text>().text = "SP: " + DB.info.SP.ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isInventory = !isInventory;
            EquipPanel.SetActive(isInventory);
        }
    }
    bool canSwap = true;
    int tempSlot;
    public void UseItem(UI_Equip_Slot useSlot)
    {
        for(int i = 0; i < inventory.slot.Length; i++)
        {
            if(inventory.slot[i].itemID != 0)
            {
                canSwap = false;
            }
            else if (inventory.slot[i].itemID == 0)
            {
                tempSlot = i;
                canSwap = true;
                break;
            }
                
            
        }

        if (canSwap)
        {
            inventory.slot[tempSlot].itemID = useSlot.itemID;
            useSlot.itemID = 0;
            tempSlot = 0;
        }
        if (!canSwap)
        {
            Debug.Log("袍 给灚辫");
        }
        tempSlot = 0;
        UpdateItem();
        inventory.UpdateItem();
    }
    public void UpdateItem()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].itemID != 0)
            {
                slot[i].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[i].itemID.ToString());
                slot[i].transform.FindChild("Image").gameObject.SetActive(true);
            }
            else if (slot[i].itemID == 0)
            {
                slot[i].transform.FindChild("Image").gameObject.SetActive(false);
            }
        }
        status[0].GetComponent<Text>().text = "LV: " + DB.info.Level.ToString();
        for (int i = 0; i < DB.itemDB.Count; i++)
        {
            if (i > DB.itemDB.Count) continue;
            if (slot[0].itemID == DB.itemDB[i].ID)
            {
                status[1].GetComponent<Text>().text = "HP: " + DB.itemDB[i].HP.ToString();
            }
            else
            {
                status[1].GetComponent<Text>().text = "HP: 0";
            }
            if (slot[1].itemID == DB.itemDB[i].ID)
            {
                status[2].GetComponent<Text>().text = "MP: " + DB.itemDB[i].MP.ToString();
            }
            else
            {
                status[2].GetComponent<Text>().text = "MP: 0";
            }
            if (slot[2].itemID == DB.itemDB[i].ID)
            {
                status[3].GetComponent<Text>().text = "ATK: " + DB.itemDB[i].ATK.ToString();
            }
            else
            {
                status[3].GetComponent<Text>().text = "ATK: 0";
            }
            if (slot[3].itemID == DB.itemDB[i].ID)
            {
                status[4].GetComponent<Text>().text = "SPEED: " + DB.itemDB[i].SPEED.ToString();
            }
            else
            {
                status[4].GetComponent<Text>().text = "SPEED: 0";
            }

            status[5].GetComponent<Text>().text = "SP: " + DB.info.SP.ToString();
        }
        if (slot[0].itemID == 0)
        {
            status[1].GetComponent<Text>().text = "HP: " + 0;
        }
        if (slot[1].itemID == 0)
        {
            status[2].GetComponent<Text>().text = "MP: " + 0;
        }
        if (slot[2].itemID == 0)
        {
            status[3].GetComponent<Text>().text = "ATK: " + 0;
        }
        if (slot[3].itemID == 0)
        {
            status[4].GetComponent<Text>().text = "SPEED: " + 0;
        }
    }


}
