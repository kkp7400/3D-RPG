using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("PlayerCamera");
        var player = GameObject.Find("Player");
        player.transform.position = transform.position;

        playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = 9f;
        playerCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.y = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
