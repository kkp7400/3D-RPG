using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inventory_Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler

{
    public Camera UICamera;
    public int itemID;
    public int itemCount;
    public string itemType;
    float currentTimeClick;
    float lastTimeClick;
    public string EquipPart;
    public DataBase DB;
    public UI_Inventory Inventory;
    // Start is called before the first frame update
    void Start() 
    {
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        Inventory = GameObject.Find("Canvas").GetComponent<UI_Inventory>();
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

    public static Vector3 defaultposition;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)//드래그시작할 때

    {
       // defaultposition = this.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)//드래그중일 때
    {
        //var screenPoint = Input.mousePosition;
        //screenPoint.z = 100f;        
        //this.transform.position = UICamera.ScreenToWorldPoint(screenPoint);
    }
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)//드래그 끝났을 때
    {
        //Vector3 mousePos = UICamera.ScreenToWorldPoint(Input.mousePosition);
        //this.transform.position = defaultposition;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        currentTimeClick = Time.time;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            if (itemID != 0)
            {

                Inventory.UseItem(this);
                UpdateSlot();
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