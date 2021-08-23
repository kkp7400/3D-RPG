using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class BossEvent : MonoBehaviour
{
    GameObject playerCharacter;
    public GameObject playerCamera;
    public GameObject StartTrigger;
    public GameObject[] StartCam;
    public Transform playerEventPos;
    GameObject BookObj;
    GameObject PlayerUI;
    public GameObject boss;
    GameObject bossText;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GameObject.Find("Player").gameObject;
        BookObj = GameObject.Find("BookObj").gameObject;

        PlayerUI = GameObject.Find("PlayerUI").gameObject;
        playerCamera = GameObject.Find("PlayerCamera").gameObject;
        bossText = GameObject.Find("BossText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTrigger.GetComponent<StageTrigger>().isStart == true)
        {
            StartCoroutine(StartEvent());
        }
    }

    public IEnumerator StartEvent()
    {
        BookObj.SetActive(false);
        PlayerUI.SetActive(false);

        playerCharacter.transform.position = playerEventPos.position;
        GameManager.instance.keyLock = true;
        StartTrigger.GetComponent<StageTrigger>().isStart = false;
        playerCamera.SetActive(false);


        StartCam[0].SetActive(true);
        StartCam[0].GetComponent<PlayableDirector>().Play();
        while (StartCam[0].GetComponent<PlayableDirector>().time < 4.45f)
        {
            yield return null;
        }
        StartCam[0].SetActive(false);


        StartCam[1].SetActive(true);
        StartCam[1].GetComponent<PlayableDirector>().Play();
        while (StartCam[1].GetComponent<PlayableDirector>().time < 3.45f)
        {
            yield return null;
        }
        StartCam[1].SetActive(false);


        StartCam[2].SetActive(true);
        StartCam[2].GetComponent<PlayableDirector>().Play();
        float currtime = 0f;
        
        while (StartCam[2].GetComponent<PlayableDirector>().time < 1.9f)
        {
            yield return null;
        }
        bossText.GetComponent<OnBossText>().onBossText = true;
        while (currtime < 3f)
        {
            currtime += Time.deltaTime;
            yield return null;
        }
        StartCam[2].SetActive(false);


        StartCam[3].SetActive(true);
        StartCam[3].GetComponent<PlayableDirector>().Play();
        bool shake1 = true;
        while (StartCam[3].GetComponent<PlayableDirector>().time < 2.9f)
        {
            if(StartCam[3].GetComponent<PlayableDirector>().time < 1f && shake1)
            {
                shake1 = false;
                StartCam[3].GetComponent<CameraShaker>().StartCameraShake(10f, 2.5f);
                boss.GetComponent<Animator>().SetTrigger("OnStart");
            }
            yield return null;
        }
        while (currtime < 4.5f)
        {
            currtime += Time.deltaTime;
            yield return null;
        }
        StartCam[3].SetActive(false);
        playerCamera.SetActive(true);

        while (playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y <= 15f)
        {
        
            //if(playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y <= 15f)
            playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y +=1.5f * Time.deltaTime;
            //if(playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y <= 10f)
            playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y += 1f * Time.deltaTime;
            yield return null;
        }
        //playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 15;
        //playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = 12;
        GameManager.instance.keyLock = false;
        BookObj.SetActive(true);
    }
}
