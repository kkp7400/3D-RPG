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
    public float HP;
    public float MP;
    public float ATK;
    public float SPEED;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Canvas").GetComponent<UI_Inventory>();
        slot = slotHolder.GetComponentsInChildren<UI_Equip_Slot>();
        EquipPanel.SetActive(isInventory);
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        //머리0 무기1 몸2 신발3
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
            slot[1].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[1].itemID.ToString());
        }
        if (DB.info.Equip_Body != 0)
        {
            slot[2].itemID = DB.info.Equip_Body;
            slot[2].EquipPart = "Body";
            slot[2].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[2].itemID.ToString());
        }
        if (DB.info.Equip_Foot != 0)
        {
            slot[3].itemID = DB.info.Equip_Foot;
            slot[3].EquipPart = "Foot";
            slot[3].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[3].itemID.ToString());
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
            UpdateItem();
            inventory.UpdateItem();
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
            Debug.Log("템 못옯김");
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
            //if (i > DB.itemDB.Count) continue;
            //0머리 1무기 2바디 3신발
            //스탯: 1HP,2MP,3ATK,4SPEED
            //그러니까 slot[0]=status[2](mp) / slot[1]=status[3](ATK) / slot[2]=status[1](HP) / slot[3]=status[4](SPEED)
            if (slot[2].itemID == DB.itemDB[i].ID)
            {
                status[1].GetComponent<Text>().text = "HP: " + DB.itemDB[i].HP.ToString();
                HP = DB.itemDB[i].HP;
            }
            
            if (slot[0].itemID == DB.itemDB[i].ID)
            {
                status[2].GetComponent<Text>().text = "MP: " + DB.itemDB[i].MP.ToString();
                MP = DB.itemDB[i].MP;
            }
           
            if (slot[1].itemID == DB.itemDB[i].ID)
            {
                status[3].GetComponent<Text>().text = "ATK: " + DB.itemDB[i].ATK.ToString();
                ATK = DB.itemDB[i].ATK;
            }
            
            if (slot[3].itemID == DB.itemDB[i].ID)
            {
                status[4].GetComponent<Text>().text = "SPEED: " + DB.itemDB[i].SPEED.ToString();
                SPEED = DB.itemDB[i].SPEED;
            }
            

            status[5].GetComponent<Text>().text = "SP: " + DB.info.SP.ToString();
        }
        if (slot[2].itemID == 0)
        {
            status[1].GetComponent<Text>().text = "HP: " + 0;
        }
        if (slot[0].itemID == 0)
        {
            status[2].GetComponent<Text>().text = "MP: " + 0;
        }
        if (slot[1].itemID == 0)
        {
            status[3].GetComponent<Text>().text = "ATK: " + 0;
        }
        if (slot[3].itemID == 0)
        {
            status[4].GetComponent<Text>().text = "SPEED: " + 0;
        }
    }


}
