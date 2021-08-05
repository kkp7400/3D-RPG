using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject KeyDownG;
    public GameObject playerCamera;
    public GameObject npcCamera;
    public GameObject mainCamera;
    public GameObject ui;
    public GameObject player;
    public DataBase DB;
    // Start is called before the first frame update
    void Start()
    {
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        KeyDownG = GameObject.Find("Canvas").transform.Find("KeyDownG").gameObject;
        playerCamera = GameObject.Find("PlayerCamera");
        npcCamera = transform.Find("NpcCamera").gameObject;
        mainCamera = GameObject.Find("Main Camera");
        if(transform.name == "SpellMaster")
		{
            ui = GameObject.Find("Canvas").transform.Find("SpellMaster").gameObject;
		}
        if (transform.name == "PotionShop")
        {
            ui = GameObject.Find("Canvas").transform.Find("Shop").gameObject;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        KeyDownG.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        DB.SaveData();
        KeyDownG.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            KeyDownG.SetActive(false);
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
        DB.SaveData();
        KeyDownG.SetActive(false);
        mainCamera.SetActive(true);
        playerCamera.SetActive(true);
        npcCamera.SetActive(false);
        player.GetComponent<PlayerState>().onNPC = false;
        ui.SetActive(false);
    }
}
