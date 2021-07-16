using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public UI_Equip equip;
    public UI_Inventory inventory;
    public static GameManager instance;
   // public delegate void BB(string a, string b);
    //public event BB name;
    public bool isOpen;
    private void Awake()
    {
        //name += AXW;
        //name += AXWs;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
       // name();
    }

    public void UpdateUI()
    {
        equip.UpdateItem();
        inventory.UpdateItem();

    }
}