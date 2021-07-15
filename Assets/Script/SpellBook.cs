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
    public int Equip_Head;
    public int Equip_Foot;
    public int Equip_Staff;
    public int Equip_Body;
    public List<int> Inventory = new List<int>();
    public List<int> itemCount = new List<int>();
}


public class Item
{
    public int ID;
    public string name;
    public string type;
    public string EquipPart;
    public float HP;
    public float MP;
    public float ATK;
    public float SPEED;
    public float RecoverAmount;
    public string ToolTip;
}

public class SpellBook : MonoBehaviour
{
    public string m_strCSVFileName = string.Empty;
    public string m_strCSVFileName2 = string.Empty;
    public string m_strCSVFileName3 = string.Empty;

    public List<Item> itemDB = new List<Item>();
    public List<Spell> spell = new List<Spell>();
    public PlayerInfo info = new PlayerInfo();
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

            //char[] typeNum = spell[i].type.ToCharArray();
            //for (int k = 0; k < typeNum.Length; k++)
            //{
            //    if (k%2 == 0)
            //    {
            //        if(typeNum[k] == '|')
            //        {

            //        }
            //    }
            //}
            //spell[i].type.Split('|');
            ////1|2|3
            ////string b;
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
            info.Level = int.Parse((m_dictionaryData2[i]["Level"].ToString()));
            info.HP = int.Parse((m_dictionaryData2[i]["HP"].ToString()));
            info.MP = int.Parse((m_dictionaryData2[i]["MP"].ToString()));
            info.ATK = int.Parse((m_dictionaryData2[i]["ATK"].ToString()));
            info.SPEED = int.Parse((m_dictionaryData2[i]["SPEED"].ToString()));
            info.GOLD = int.Parse((m_dictionaryData2[i]["GOLD"].ToString()));
            info.SP = int.Parse((m_dictionaryData2[i]["SP"].ToString()));
            info.SP_FirePunch = int.Parse((m_dictionaryData2[i]["SP_FirePunch"].ToString()));
            info.SP_EnergyBall = int.Parse((m_dictionaryData2[i]["SP_EnergyBall"].ToString()));
            info.SP_Meteor = int.Parse((m_dictionaryData2[i]["SP_Meteor"].ToString()));
            info.SP_Blizard = int.Parse((m_dictionaryData2[i]["SP_Blizard"].ToString()));
            info.SP_Shild = int.Parse((m_dictionaryData2[i]["SP_Shild"].ToString()));
            info.Equip_Head = int.Parse((m_dictionaryData2[i]["Equip_Head"].ToString()));
            info.Equip_Foot = int.Parse((m_dictionaryData2[i]["Equip_Foot"].ToString()));
            info.Equip_Staff = int.Parse((m_dictionaryData2[i]["Equip_Staff"].ToString()));
            info.Equip_Body = int.Parse((m_dictionaryData2[i]["Equip_Body"].ToString()));
            string tempInventory = m_dictionaryData2[i]["Inventory"].ToString();
            string[] tempInventory2 = tempInventory.Split('|');

            for(int k = 0; k < tempInventory2.Length; k++)
            {
                info.Inventory.Add(int.Parse(tempInventory2[k]));
            }

            string itemCount = m_dictionaryData2[i]["ItemCount"].ToString();
            string[] itemCount2 = itemCount.Split('|');

            for (int k = 0; k < itemCount2.Length; k++)
            {
                info.itemCount.Add(int.Parse(itemCount2[k]));
            }
        }

        List<Dictionary<string, object>> m_dictionaryData3 = CSVReader.Read(m_strCSVFileName3);
        for (int i = 0; i < m_dictionaryData3.Count; i++)
        {
            itemDB.Add(new Item());
            itemDB[i].ID = int.Parse((m_dictionaryData3[i]["ID"].ToString()));
            itemDB[i].name = m_dictionaryData3[i]["Name"].ToString();
            itemDB[i].type = m_dictionaryData3[i]["Type"].ToString();
            if (itemDB[i].type == "Equipment")
            {
                itemDB[i].EquipPart = m_dictionaryData3[i]["EquipPart"].ToString();
                if (itemDB[i].EquipPart == "Body") itemDB[i].HP = float.Parse((m_dictionaryData3[i]["HP"].ToString()));
                if (itemDB[i].EquipPart == "Head") itemDB[i].MP = float.Parse((m_dictionaryData3[i]["MP"].ToString()));
                if (itemDB[i].EquipPart == "Staff") itemDB[i].ATK = float.Parse((m_dictionaryData3[i]["ATK"].ToString()));
                if (itemDB[i].EquipPart == "Foot") itemDB[i].SPEED = float.Parse((m_dictionaryData3[i]["SPEED"].ToString()));
            }
            else if(itemDB[i].type == "Consumable")
            {
                itemDB[i].RecoverAmount = float.Parse((m_dictionaryData3[i]["RecoverAmount"].ToString()));
            }
            itemDB[i].ToolTip = m_dictionaryData3[i]["ToolTip"].ToString();

        }        
    }

    // Update is called once per frame
    void Update()
    {
	  
    }
}
