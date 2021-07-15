using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : UI_Inventory, IPointerClickHandler
{
    public int itemID;
    public int itemCount;
    public string itemType;
    float currentTimeClick;
    float lastTimeClick;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < DB.itemDB.Count;i++)
        {
            if(itemID == DB.itemDB[i].ID)
            {
                itemType = DB.itemDB[i].type;
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
                base.UseItem(this);
            }
        }
        lastTimeClick = currentTimeClick;
    }
}
