using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossEvent : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject StartTrigger;
    public GameObject[] StartCam;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTrigger.GetComponent<StageTrigger>().isStart == true)
        {
            
            StartCam[0].SetActive(true);
            StartCam[0].GetComponent<PlayableDirector>().Play();
            StartCoroutine(StartEvent());
            playerCamera.SetActive(false);
            StartTrigger.GetComponent<StageTrigger>().isStart = false;
        }
        




    }

    public IEnumerator StartEvent()
    {
        float currtime = 0f;
        Debug.Log("??");
        GameManager.instance.keyLock = true;
        while (currtime < 2f)
        {
            currtime += Time.deltaTime;
            yield return null;
        }
        while (StartCam[0].GetComponent<PlayableDirector>().time < 2.0f)
        {            
            yield return null;
        }

        StartCam[0].SetActive(false);
        StartCam[1].SetActive(true);
        StartCam[1].GetComponent<PlayableDirector>().Play();
        while (StartCam[1].GetComponent<PlayableDirector>().time < 2.9f)
        {
            yield return null;
        }

        StartCam[1].SetActive(false);
        GameManager.instance.keyLock = false;

        playerCamera.SetActive(true);
    }
}
