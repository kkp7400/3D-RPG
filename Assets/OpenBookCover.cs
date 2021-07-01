using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBookCover : MonoBehaviour
{
    public GameObject[] cover;
    public bool open = false;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 2f;
    public bool canOpenDoor;
    void Start()
    {
        canOpenDoor = true;
    }
    public void ChangeDoorState()
    {
        open = !open;
    }
    void Update()
    {
        if (GameManager.instance.isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
        }
        else
        {
            StartCoroutine(Close());
            //Quaternion targetRotation2 = Quaternion.Euler(0, 90, 0);
            //transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
        }
    }
    public IEnumerator Close()
    {

        yield return new WaitForSeconds(0.5f);
        Quaternion targetRotation2 = Quaternion.Euler(0, 90, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
    }

}
