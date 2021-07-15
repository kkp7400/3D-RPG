using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_SpellMaster : MonoBehaviour
{
    public SpellBook data;
    public Text[] text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        text[0].text = "SP : " + data.info.SP.ToString();
        text[1].text = data.info.SP_FirePunch.ToString();
        text[2].text = data.info.SP_EnergyBall.ToString();
        text[3].text = data.info.SP_Meteor.ToString();
        text[4].text = data.info.SP_Blizard.ToString();
        text[5].text = data.info.SP_Shild.ToString();

    }

   public void FirePunchUp()
    {
        if (data.info.SP > 0)
        {
            data.info.SP_FirePunch += 1;
            data.info.SP -= 1;
        }
    }
    public void FirePunchDown()
    {
        if (data.info.SP_FirePunch > 0)
        {
            data.info.SP_FirePunch -= 1;
            data.info.SP += 1;
        }
    }

    public void EnergyBallUp()
    {
        if (data.info.SP > 0)
        {
            data.info.SP_EnergyBall += 1;
            data.info.SP -= 1;
        }
    }
    public void EnergyBallDown()
    {
        if (data.info.SP_EnergyBall > 0)
        {
            data.info.SP_EnergyBall -= 1;
            data.info.SP += 1;
        }
    }

    public void MeteorUp()
    {
        if (data.info.SP > 0)
        {
            data.info.SP_Meteor += 1;
            data.info.SP -= 1;
        }
    }
    public void MeteorDown()
    {
        if (data.info.SP_Meteor > 0)
        {
            data.info.SP_Meteor -= 1;
            data.info.SP += 1;
        }
    }

    public void BlizardUp()
    {
        if (data.info.SP > 0)
        {
            data.info.SP_Blizard += 1;
            data.info.SP -= 1;
        }
    }
    public void BlizardDown()
    {
        if (data.info.SP_Blizard > 0)
        {
            data.info.SP_Blizard -= 1;
            data.info.SP += 1;
        }
    }

    public void ShieldUp()
    {
        if (data.info.SP > 0)
        {
            data.info.SP_Shild += 1;
            data.info.SP -= 1;
        }
    }
    public void ShieldDown()
    {
        if (data.info.SP_Shild > 0)
        {
            data.info.SP_Shild -= 1;
            data.info.SP += 1;
        }
    }
}
