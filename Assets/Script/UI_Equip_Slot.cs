using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Equip_Slot : MonoBehaviour, IPointerClickHandler
{
    public int itemID;
    float currentTimeClick;
    float lastTimeClick;
    public string itemType;
    public string EquipPart;
    public DataBase DB;
    public UI_Equip Equip;
    // Start is called before the first frame update
    void Start()
    {
        Equip = GameObject.Find("Canvas").GetComponent<UI_Equip>();
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        for (int i = 0; i < DB.itemDB.Count; i++)
        {
            if (itemID == DB.itemDB[i].ID)
            {
                itemType = DB.itemDB[i].type;
                EquipPart = DB.itemDB[i].EquipPart;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        currentTimeClick = Time.time;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            if (itemID != 0)
            {
                Equip.UseItem(this);
                GameManager.instance.UpdateUI();
            }
        }
        lastTimeClick = currentTimeClick;
    }
    public void UpdateSlot()
    {
        for (int i = 0; i < DB.itemDB.Count; i++)
        {
            if (itemID == DB.itemDB[i].ID)
            {
                itemType = DB.itemDB[i].type;
                EquipPart = DB.itemDB[i].EquipPart;
            }
        }
    }
}
