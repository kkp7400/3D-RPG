using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class OpenBook : MonoBehaviour
{
    [Serializable]
    public struct Page
    {
        public GameObject num;
        public float doorOpenAngle;
        public float doorCloseAngle;
        public float startAngle;
    }
    public Page[] page;
    public bool open = false;
    //public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;
    public bool canOpenDoor;
    public GameObject pivot;
   // private bool isOpen;
    void Awake()
	{
        for (int i = 0; i < page.Length; i++)
        {
            page[i].startAngle = page[i].num.transform.localRotation.eulerAngles.z;
        }
        //isOpen = false;
    }
    void Start()
    {
        canOpenDoor = false;
        
    }
    public void ChangeDoorState()
    {
        open = !open;
    }
    void Update()
    {   
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    isOpen = true;
        //}
        if (!GameManager.instance.isOpen)
        {
			for (int i = 0; i < page.Length; i++)
			{
				page[i].num.transform.localRotation = Quaternion.Slerp(page[i].num.transform.localRotation, Quaternion.Euler(0, 0, page[i].startAngle), smoot * Time.deltaTime);
			}
		}
        else
        {
            if (GameManager.instance.isOpen)
            {
                StartCoroutine(Open());
                StartCoroutine(Open2());
            }
            //for (int i = 0; i < page.Length; i++)
            //{
            //    Quaternion targetRotation = Quaternion.Euler(0, 0, page[i].doorOpenAngle);
            //    page[i].num.transform.localRotation = Quaternion.Slerp(page[i].num.transform.localRotation, Quaternion.Euler(0, 0, page[i].doorOpenAngle), smoot * Time.deltaTime);
            //}
        }
    }
    public IEnumerator Open()
	{
        int a=23;
        for (int i = 14; i < page.Length; i++)
        {
                    
            if(i == 19)
			{
                page[i].doorOpenAngle = -32f;
            }
            else if (i == 20)
            {
                page[i].doorOpenAngle = -4.3f;
            }
            else if (i == 14)
            {
                page[i].doorOpenAngle = -70f*3;
            }
            else if (i == 15)
            {
                page[i].doorOpenAngle = -60f*3;
            }
            else
            page[i].doorOpenAngle = a * -7f;
            a--;
            
            Quaternion targetRotation = Quaternion.Euler(0, 0, page[i].doorOpenAngle);
            yield return new WaitForSeconds(0.05f);
            page[i].num.transform.localRotation = Quaternion.Slerp(page[i].num.transform.localRotation, Quaternion.Euler(0, 0, page[i].doorOpenAngle/3), smoot * Time.deltaTime);
            
        }
    }
    public IEnumerator Open2()
    {
        for (int i = 0; i <= 13; i++)
        {
            page[i].doorCloseAngle = i * 7f;
            if(i ==0)
			{
                page[i].doorCloseAngle = -10f;
            }
            Quaternion targetRotation = Quaternion.Euler(0, 0, page[i].doorCloseAngle);
            yield return new WaitForSeconds(0.05f);
            page[i].num.transform.localRotation = Quaternion.Slerp(page[i].num.transform.localRotation, Quaternion.Euler(0, 0, page[i].doorCloseAngle), smoot * Time.deltaTime);
        }
       // isOpen = false;
    }
}

  