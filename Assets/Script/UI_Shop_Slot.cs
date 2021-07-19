using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;


public class UI_Shop_Slot : MonoBehaviour//, IPointerClickHandler
{
    public int itemID;
    public string name;
    public string Type;
    public int Price;
    public string ToolTip;
    float currentTimeClick;
    float lastTimeClick;
    public UI_Inventory Inventory;
    public UI_Shop Shop;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("Canvas").GetComponent<UI_Inventory>();
        Shop = GameObject.Find("Canvas").GetComponent<UI_Shop>();
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
        }
        lastTimeClick = currentTimeClick;
    }
    public void UseItem()
    {

        Shop.UseItem(this);
    }
   
}
