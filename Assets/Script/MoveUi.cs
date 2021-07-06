using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MoveUi : MonoBehaviour
{
    public float targetPosX;
    public float startPosX;
    public float startSpeed;
    public float EndSpeed;
    private bool isClose;
    // Start is called before the first frame update
    void Start()
    {
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isOpen)
        {
            isClose = true;
            if (startPosX > targetPosX)
            {
                if (gameObject.transform.localPosition.x >= targetPosX)
                {
                    gameObject.transform.Translate(Vector3.left * Time.deltaTime * startSpeed);
                }
            }
            if (startPosX < targetPosX)
            {
                if (gameObject.transform.localPosition.x <= targetPosX)
                {
                    gameObject.transform.Translate(Vector3.right * Time.deltaTime * startSpeed);
                }
            }
            return;            
        }
        else
        {
            if (isClose)
            {
                StartCoroutine(Close());
               
            }
        }
    }
    public IEnumerator Close()
    {
        yield return new WaitForSeconds(1f);
        if (startPosX > targetPosX)
        {
            if (gameObject.transform.localPosition.x <= startPosX)
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * EndSpeed);
            }
        }
        if (startPosX < targetPosX)
        {
            if (gameObject.transform.localPosition.x >= startPosX)
            {
                gameObject.transform.Translate(Vector3.left * Time.deltaTime * EndSpeed);
            }
        }
        isClose = false;
    }
}
