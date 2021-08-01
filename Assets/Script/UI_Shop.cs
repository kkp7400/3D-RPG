using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Shop : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool isInventory = false;
    public UI_Shop_Slot[] slot;
    public UI_Equip equip;
    public UI_Inventory inventory;
    public Transform slotHolder;
    // Start is called before the first frame update
    public DataBase DB;
    public Text errorText;
    public GameObject anchor;
    // Start is called before the first frame update
    void Start()
    {
        anchor.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-1500, 20, 0);
        inventory = GameObject.Find("Canvas").GetComponent<UI_Inventory>();
        equip = GameObject.Find("Canvas").GetComponent<UI_Equip>();
        slotHolder = transform;
        slot = slotHolder.GetComponentsInChildren<UI_Shop_Slot>();
        DB = GameObject.Find("GM").GetComponent<DataBase>();        
        for (int i = 0; i < DB.itemDB.Count; i++)
        {
            slot[i].itemID = DB.itemDB[i].ID;
            slot[i].transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[i].itemID.ToString());
            slot[i].name = DB.itemDB[i].name;
            slot[i].Price = DB.itemDB[i].Price;
            slot[i].Type = DB.itemDB[i].type;
            slot[i].transform.FindChild("Price").GetComponent<Text>().text = slot[i].Price.ToString();
            slot[i].ToolTip = DB.itemDB[i].ToolTip;
        }        
        anchor.GetComponent<Image>().rectTransform.anchoredPosition = new Vector3(-636, 20, 0);
        anchor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem(UI_Shop_Slot useSlot)
    {
        
        if (DB.info.GOLD >= useSlot.Price)
        {
            if (useSlot.itemID != 0)
            {

                if (useSlot.Type == "Consumable")
                {
                    for (int i = 0; i < inventory.slot.Length; i++)
                    {
                        if (inventory.slot[i].itemID == useSlot.itemID)
                        {
                            inventory.slot[i].itemCount++;
                            DB.info.GOLD -= useSlot.Price;
                            equip.UpdateItem();
                            inventory.UpdateItem();
                            return;
                        }

                    }
                    for (int i = 0; i < inventory.slot.Length; i++)
                    {
                        if (inventory.slot[i].itemID == 0)
                        {
                            inventory.slot[i].itemID = useSlot.itemID;
                            inventory.slot[i].itemCount = 1;
                            DB.info.GOLD -= useSlot.Price;

                            equip.UpdateItem();
                            inventory.UpdateItem();
                            return;
                        }
                    }
                }
                else if(useSlot.Type == "Equipment")
                {
                    for (int i = 0; i < inventory.slot.Length; i++)
                    {
                        if (inventory.slot[i].itemID == 0)
                        {
                            inventory.slot[i].itemID = useSlot.itemID;
                            inventory.slot[i].itemCount = 1;
                            DB.info.GOLD -= useSlot.Price;
                            equip.UpdateItem();
                            inventory.UpdateItem();
                            return;
                        }                                         
                    }
                }
                StartCoroutine(Error());
            }
        }
        GameManager.instance.UpdateUI();
        equip.UpdateItem();
        inventory.UpdateItem();
    }
    public IEnumerator Error()
    {
        float a = 1f;
        errorText.color = new Color(255, 0, 0, 1f);
        while (true)
        {
            a -= 0.1f;
            errorText.color = new Color(255, 0, 0, 1f);
            yield return new WaitForSeconds(0.1f);
            if(a<=0)
            {
                break;
            }
        }
    }
}
