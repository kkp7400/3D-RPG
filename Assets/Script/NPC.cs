using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject npcCamera;
    public GameObject mainCamera;
    public GameObject ui;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            mainCamera.SetActive(false); 
            playerCamera.SetActive(false);
            npcCamera.SetActive(true);

            
            ui.SetActive(true);
            Vector3 lookTarget = new Vector3(player.transform.position.x, 0, 0);
            transform.LookAt(lookTarget);
            player.GetComponent<PlayerState>().onNPC = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        mainCamera.SetActive(true);
        playerCamera.SetActive(true);
        npcCamera.SetActive(false);
        player.GetComponent<PlayerState>().onNPC = false;
        ui.SetActive(false);
    }
}
