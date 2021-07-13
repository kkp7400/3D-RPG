using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spell
{

    public int index;
    public string name;
    public string type;
    public float damage;
    public int count;
    public float cool;
    public int totalArrow;
    public List<string> arrow = new List<string>();

}

public class PlayerInfo
{

    public int Level;
    public int HP;
    public int MP;
    public int ATK;
    public int SPEED;
    public int GOLD;
    public int SP;
    public int SP_FirePunch;
    public int SP_EnergyBall;
    public int SP_Meteor;
    public int SP_Blizard;
    public int SP_Shild;
}
public class SpellBook : MonoBehaviour
{
    public string m_strCSVFileName = string.Empty;
    public string m_strCSVFileName2 = string.Empty;

    public List<Spell> spell = new List<Spell>();
    public PlayerInfo playerInfo = new PlayerInfo();
    // Start is called before the first frame update
    void Awake()
    {
        List<Dictionary<string, object>> m_dictionaryData = CSVReader.Read(m_strCSVFileName);
        for (int i = 0; i < m_dictionaryData.Count; i++)

        {

            spell.Add(new Spell());

            spell[i].index = int.Parse((m_dictionaryData[i]["Index"].ToString()));

            spell[i].name = m_dictionaryData[i]["Name"].ToString();

            spell[i].type = m_dictionaryData[i]["Type"].ToString();

            spell[i].damage = float.Parse(m_dictionaryData[i]["Damage"].ToString());

            spell[i].count = int.Parse(m_dictionaryData[i]["Count"].ToString());

            spell[i].cool = float.Parse(m_dictionaryData[i]["Cool"].ToString());

            spell[i].totalArrow = int.Parse(m_dictionaryData[i]["TotalArrow"].ToString());
          //  string b;
            for (int j = 0; j < 8; j++)
            {
                //b = "ArrowNum_" + j.ToString();
                if (spell[i].totalArrow-1 < j) break;
                spell[i].arrow.Add(m_dictionaryData[i]["ArrowNum_"+ j.ToString()].ToString());
            }
        }


        List<Dictionary<string, object>> m_dictionaryData2 = CSVReader.Read(m_strCSVFileName2);
        for (int i = 0; i < m_dictionaryData2.Count; i++)
        {
            playerInfo.Level = int.Parse((m_dictionaryData2[i]["Level"].ToString()));
            playerInfo.HP = int.Parse((m_dictionaryData2[i]["HP"].ToString()));
            playerInfo.MP = int.Parse((m_dictionaryData2[i]["MP"].ToString()));
            playerInfo.ATK = int.Parse((m_dictionaryData2[i]["ATK"].ToString()));
            playerInfo.SPEED = int.Parse((m_dictionaryData2[i]["SPEED"].ToString()));
            playerInfo.GOLD = int.Parse((m_dictionaryData2[i]["GOLD"].ToString()));
            playerInfo.SP = int.Parse((m_dictionaryData2[i]["SP"].ToString()));
            playerInfo.SP_FirePunch = int.Parse((m_dictionaryData2[i]["SP_FirePunch"].ToString()));
            playerInfo.SP_EnergyBall = int.Parse((m_dictionaryData2[i]["SP_EnergyBall"].ToString()));
            playerInfo.SP_Meteor = int.Parse((m_dictionaryData2[i]["SP_Meteor"].ToString()));
            playerInfo.SP_Blizard = int.Parse((m_dictionaryData2[i]["SP_Blizard"].ToString()));
            playerInfo.SP_Shild = int.Parse((m_dictionaryData2[i]["SP_Shild"].ToString()));

        }
    }
    int a = 014224;

    // Update is called once per frame
    void Update()
    {
	  
    }
}
