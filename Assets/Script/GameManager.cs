using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UI_Equip equip;
    public UI_Inventory inventory;
    public UI_SpellMaster spellM;
    public Image HpBar;
    public Image MpBar;
    public Image ExpBar;
    public DataBase DB;
    public float maxExp;
    public float maxHP;
    public float nowHP;
    public float maxMP;
    public float nowMP;
    public float Speed;
    // public delegate void BB(string a, string b);
    //public event BB name;
    public bool isOpen;
    public bool onShield;
    private bool statusUpdate = true;
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
        onShield = false;
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        isOpen = false;
        maxExp = DB.info.Level * 20;
        maxHP = 100 + (equip.HP * 100);
        maxMP = 100 + (equip.MP * 100);
        nowHP = maxHP;
        nowMP = maxMP;
        Speed = 5 + (equip.SPEED * 5);
    }
    // Update is called once per frame
    void Update()
    {
        ExpBar.fillAmount = DB.info.Exp / maxExp;
        UpdateUI(); 
        if (isOpen)
        {
            nowMP -= Time.deltaTime*30f;
            if(nowMP<=0)
            {
                isOpen = false;
            }
        }
        if(DB.info.Exp >= maxExp)
        {
            DB.info.Level++;
            DB.info.Exp = 0;
            maxExp = DB.info.Level * 100;
        }
        if(nowMP<maxMP)
        {
            nowMP += Time.deltaTime*20f;
        }
        MpBar.fillAmount = nowMP / maxMP;
        HpBar.fillAmount = nowHP / maxHP;
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            nowHP = maxHP;
        }
    }

    public void UpdateUI()
    {
        Speed = 5 + (equip.SPEED * 5);
        maxHP = 100 + (equip.HP * 100);
        if (nowHP > maxHP) nowHP = maxHP;
        maxMP = 100 + (equip.MP * 100);
        if (nowMP > maxMP) nowMP = maxMP;
        maxExp = DB.info.Level * 100;
        equip.UpdateItem();
        inventory.UpdateItem();
        //spellM.UpdateUI();

    }

    public void OnDamage(float damage)
    {
        if(!onShield)
        nowHP -= damage;
    }
}