using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool isInventory = false;
    public UI_Inventory_Slot[] slot;
    public UI_Equip equip;
    public Transform slotHolder;
    // Start is called before the first frame update
    public DataBase DB;

    public int Gold;
    public Text InventoryGoldCount;
    public GameObject potionSlot;
    void Start()
    {
        equip = GameObject.Find("Canvas").GetComponent<UI_Equip>();
        slot = slotHolder.GetComponentsInChildren<UI_Inventory_Slot>();
        inventoryPanel.SetActive(isInventory);

        if (DB.info.Inventory[0] != 0)
            for (int i = 0; i < DB.info.Inventory.Count; i++)
            {
                slot[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + DB.info.Inventory[i].ToString());
                slot[i].itemID = DB.info.Inventory[i];
                slot[i].gameObject.GetComponentInChildren<Text>().text = DB.info.itemCount[i].ToString();
                slot[i].itemCount = DB.info.itemCount[i];

            }
        InventoryGoldCount.text = DB.info.GOLD.ToString();
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].transform.Find("Image").GetComponent<Image>().sprite == null)
            {
                slot[i].itemID = 0;

                slot[i].itemCount = 0;
                slot[i].transform.Find("Image").gameObject.SetActive(false);


            }
            //if (slot[i].itemID == potionSlot.GetComponent<UI_Inventory_Slot>().itemID)
            //{
            //    potionSlot.SetActive(true);
            //    potionSlot.GetComponent<UI_Inventory_Slot>().itemID = slot[i].itemID;
            //    potionSlot.GetComponent<UI_Inventory_Slot>().itemCount = slot[i].itemCount;
            //    potionSlot.transform.FindChild("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[i].itemID);
            //    potionSlot.transform.FindChild("Text").GetComponent<Text>().text = slot[i].itemCount.ToString();
            //}
            if (slot[i].itemCount <= 1)
            {
                slot[i].gameObject.GetComponentInChildren<Text>().text = " ";
            }
        }

        potionSlot.GetComponent<UI_Inventory_Slot>().itemID = DB.info.PotionSlot;
        potionSlot.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + DB.info.PotionSlot.ToString());
       
    }

    // Update is called once per frame
    void Update()
    {
        InventoryGoldCount.text = DB.info.GOLD.ToString();
        if (Input.GetKeyDown(KeyCode.I))
        {
            DB.SaveData();
               isInventory = !isInventory;
            inventoryPanel.SetActive(isInventory);

            GameManager.instance.UpdateUI();
        }
        if (GameManager.instance.maxHP > GameManager.instance.nowHP)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                for (int i = 0; i < slot.Length; i++)
                {
                    if (slot[i].itemID == potionSlot.GetComponent<UI_Inventory_Slot>().itemID
                        && potionSlot.GetComponent<UI_Inventory_Slot>().itemCount > 0)
                    {
                        potionSlot.GetComponent<UI_Inventory_Slot>().itemCount = slot[i].itemCount;
                        potionSlot.GetComponent<UI_Inventory_Slot>().itemCount -= 1;
                        slot[i].itemCount = potionSlot.GetComponent<UI_Inventory_Slot>().itemCount;
                        for (int j = 0; j < DB.itemDB.Count; j++)
                        {
                            if (DB.itemDB[j].ID == potionSlot.GetComponent<UI_Inventory_Slot>().itemID)
                            {
                                GameManager.instance.nowHP += DB.itemDB[j].RecoverAmount * GameManager.instance.maxHP;
                                if (GameManager.instance.nowHP > GameManager.instance.maxHP)
                                {
                                    GameManager.instance.nowHP = GameManager.instance.maxHP;
                                    GameManager.instance.UpdateUI();
                                    break;
                                }

                            }
                        }

                        break;
                    }
                }

            }
        }

    }

    public void UseItem(UI_Inventory_Slot useSlot)
    {
        if (useSlot.itemType == "Equipment")
        {
            if (useSlot.EquipPart == "Head")//0��
            {
                if (equip.slot[0].itemID != 0)
                {
                    int tempID = equip.slot[0].itemID;
                    equip.slot[0].itemID = useSlot.itemID;
                    useSlot.itemID = tempID;
                }
                else
                {
                    equip.slot[0].itemID = useSlot.itemID;
                    useSlot.itemID = 0;
                }
            };
            if (useSlot.EquipPart == "Staff")//1��
            {
                if (equip.slot[1].itemID != 0)
                {
                    int tempID = equip.slot[1].itemID;
                    equip.slot[1].itemID = useSlot.itemID;
                    useSlot.itemID = tempID;
                }
                else
                {
                    equip.slot[1].itemID = useSlot.itemID;
                    useSlot.itemID = 0;
                }
            };
            if (useSlot.EquipPart == "Body")//2��
            {
                if (equip.slot[2].itemID != 0)
                {
                    int tempID = equip.slot[2].itemID;
                    equip.slot[2].itemID = useSlot.itemID;
                    useSlot.itemID = tempID;
                }
                else
                {
                    equip.slot[2].itemID = useSlot.itemID;
                    useSlot.itemID = 0;
                }
            };
            if (useSlot.EquipPart == "Foot")//3��
            {
                if (equip.slot[3].itemID != 0)
                {
                    int tempID = equip.slot[3].itemID;
                    equip.slot[3].itemID = useSlot.itemID;
                    useSlot.itemID = tempID;
                }
                else
                {
                    equip.slot[3].itemID = useSlot.itemID;
                    useSlot.itemID = 0;
                }
            };
        }
        if (useSlot.itemType == "Consumable")
        {
            potionSlot.SetActive(true);
            potionSlot.GetComponent<UI_Inventory_Slot>().itemID = useSlot.itemID;
            potionSlot.GetComponent<UI_Inventory_Slot>().itemCount = useSlot.itemCount;
            potionSlot.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + useSlot.itemID);
            potionSlot.transform.Find("Text").GetComponent<Text>().text = useSlot.itemCount.ToString();
            //���� ���°� ���߿�
        }

        equip.UpdateItem();
        UpdateItem();
    }
    public void UpdateItem()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].itemID == 0)
            {
                slot[i].itemID = 0;

                slot[i].itemCount = 0;
                slot[i].transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 0f);
                slot[i].transform.Find("Image").gameObject.SetActive(false);
            }
            else if (slot[i].itemID != 0)
            {

                slot[i].transform.GetComponentInChildren<Text>().text = slot[i].itemCount.ToString();
                slot[i].transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1f);
                slot[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + slot[i].itemID.ToString());
                slot[i].transform.Find("Image").gameObject.SetActive(true);

            }
            if (slot[i].itemCount <= 1)
            {
                slot[i].gameObject.GetComponentInChildren<Text>().text = " ";
            }
        }

        InventoryGoldCount.text = DB.info.GOLD.ToString();

        for (int i = 0; i < slot.Length; i++)
        {
            if (potionSlot.transform.Find("Image").GetComponent<Image>().sprite == null) break;
            if (potionSlot.transform.Find("Image").GetComponent<Image>().sprite.name == slot[i].itemID.ToString())
            {
                potionSlot.GetComponent<UI_Inventory_Slot>().itemID = slot[i].itemID;
                potionSlot.GetComponent<UI_Inventory_Slot>().itemCount = slot[i].itemCount;
                potionSlot.transform.Find("Text").GetComponent<Text>().text = slot[i].itemCount.ToString();
                if (slot[i].itemCount < 1)
                {
                    potionSlot.transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);

                    potionSlot.transform.Find("Text").GetComponent<Text>().color = new Color(255, 255, 255, 0f);
                }
                if (slot[i].itemCount >= 1)
                {

                    potionSlot.transform.Find("Image").GetComponent<Image>().color = new Color(255, 255, 255, 1f);

                    potionSlot.transform.Find("Text").GetComponent<Text>().color = new Color(255, 255, 255, 1f);
                }
            }
        }
    }
}
