using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_SpellMaster : MonoBehaviour
{
    public DataBase DB;
    public Text[] text;
    public PlayerState player;
    public int beforeLv;
    public int nowLv;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerState>();
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        beforeLv = DB.info.Level;
        nowLv = DB.info.Level;
        DB.info.SP = nowLv;
        DB.info.SP -= DB.info.SP_FirePunch;
        DB.info.SP -= DB.info.SP_Blizard;
        DB.info.SP -= DB.info.SP_EnergyBall;
        DB.info.SP -= DB.info.SP_Meteor;
        DB.info.SP -= DB.info.SP_Shild;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        nowLv = DB.info.Level;
        if (nowLv>beforeLv)
        {
            DB.info.SP += nowLv - beforeLv;
            beforeLv = nowLv;
        }

    }

   public void FirePunchUp()
    {

        if (DB.info.SP > 0)
        {
            DB.info.SP_FirePunch += 1;
            DB.info.SP -= 1;
        }
        UpdateUI();
    }
    public void FirePunchDown()
    {
        if (DB.info.SP_FirePunch > 0)
        {
            DB.info.SP_FirePunch -= 1;
            DB.info.SP += 1;
        }
        UpdateUI();
    }

    public void EnergyBallUp()
    {
        if (DB.info.SP > 0)
        {
            DB.info.SP_EnergyBall += 1;
            DB.info.SP -= 1;
        }
        UpdateUI();
    }
    public void EnergyBallDown()
    {
        if (DB.info.SP_EnergyBall > 0)
        {
            DB.info.SP_EnergyBall -= 1;
            DB.info.SP += 1;
        }
        UpdateUI();
    }

    public void MeteorUp()
    {
        if (DB.info.SP > 0)
        {
            DB.info.SP_Meteor += 1;
            DB.info.SP -= 1;
        }
        UpdateUI();
    }
    public void MeteorDown()
    {
        if (DB.info.SP_Meteor > 0)
        {
            DB.info.SP_Meteor -= 1;
            DB.info.SP += 1;
        }
        UpdateUI();
    }

    public void BlizardUp()
    {
        if (DB.info.SP > 0)
        {
            DB.info.SP_Blizard += 1;
            DB.info.SP -= 1;
        }
        UpdateUI();
    }
    public void BlizardDown()
    {
        if (DB.info.SP_Blizard > 0)
        {
            DB.info.SP_Blizard -= 1;
            DB.info.SP += 1;
        }
        UpdateUI();
    }

    public void ShieldUp()
    {
        if (DB.info.SP > 0)
        {
            DB.info.SP_Shild += 1;
            DB.info.SP -= 1;
        }
        UpdateUI();
    }
    public void ShieldDown()
    {
        if (DB.info.SP_Shild > 0)
        {
            DB.info.SP_Shild -= 1;
            DB.info.SP += 1;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        text[0].text = "SP : " + DB.info.SP.ToString();
        text[1].text = DB.info.SP_FirePunch.ToString();
        text[2].text = DB.info.SP_EnergyBall.ToString();
        text[3].text = DB.info.SP_Meteor.ToString();
        text[4].text = DB.info.SP_Blizard.ToString();
        text[5].text = DB.info.SP_Shild.ToString();
    }
}
