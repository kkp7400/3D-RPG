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
    public int PotionSlot;
    public List<int> Inventory = new List<int>();
    public List<int> itemCount = new List<int>();
    public float Exp;
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
    public int Price;
    public string ToolTip;
}

public class DataBase : MonoBehaviour
{
    public string m_strCSVFileName = string.Empty;
    public string m_strCSVFileName2 = string.Empty;
    public string m_strCSVFileName3 = string.Empty;
    public string m_strCSVFileName4 = string.Empty;
    public string m_strCSVFileName5 = string.Empty;

    public UI_Inventory inventory;
    public UI_Equip equip;

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
                if (spell[i].totalArrow - 1 < j) break;
                spell[i].arrow.Add(m_dictionaryData[i]["ArrowNum_" + j.ToString()].ToString());
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
            info.PotionSlot = int.Parse((m_dictionaryData2[i]["PotionSlot"].ToString()));
            string tempInventory = m_dictionaryData2[i]["Inventory"].ToString();
            string[] tempInventory2 = tempInventory.Split('|');
            for (int k = 0; k < tempInventory2.Length; k++)
            {
                info.Inventory.Add(int.Parse(tempInventory2[k]));
            }

            string itemCount = m_dictionaryData2[i]["ItemCount"].ToString();
            string[] itemCount2 = itemCount.Split('|');

            for (int k = 0; k < itemCount2.Length; k++)
            {
                info.itemCount.Add(int.Parse(itemCount2[k]));
            }

            info.Exp = int.Parse((m_dictionaryData2[i]["EXP"].ToString()));
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
            else if (itemDB[i].type == "Consumable")
            {
                itemDB[i].RecoverAmount = float.Parse((m_dictionaryData3[i]["RecoverAmount"].ToString()));
            }
            itemDB[i].Price = int.Parse((m_dictionaryData3[i]["Price"].ToString()));
            itemDB[i].ToolTip = m_dictionaryData3[i]["ToolTip"].ToString();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.End))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            ResetData();
        }
    }
    public void SaveData()
    {
        using (var writer = new CsvFileWriter("Assets/Resources/PlayerInfo.csv"))
        {
            List<string> columns = new List<string>() { "Level", "HP", "MP", "ATK", "SPEED", "GOLD", "SP", "SP_FirePunch", "SP_EnergyBall", "SP_Meteor", "SP_Blizard", "SP_Shild", "Equip_Head", "Equip_Foot", "Equip_Staff", "Equip_Body","PotionSlot", "Inventory", "ItemCount", "EXP" };// making Index Row
            writer.WriteRow(columns);
            columns.Clear();

            columns.Add(info.Level.ToString()); // Level
            columns.Add(info.HP.ToString());  // HP
            columns.Add(info.MP.ToString()); // MP
            columns.Add(info.ATK.ToString()); // ATK
            columns.Add(info.SPEED.ToString()); // SPEED
            columns.Add(info.GOLD.ToString()); // GOLD
            columns.Add(info.SP.ToString()); // SP
            columns.Add(info.SP_FirePunch.ToString()); // SP_FirePunch
            columns.Add(info.SP_EnergyBall.ToString()); // SP_EnergyBall
            columns.Add(info.SP_Meteor.ToString()); // SP_Meteor
            columns.Add(info.SP_Blizard.ToString()); // SP_Blizard
            columns.Add(info.SP_Shild.ToString()); // SP_Shild
            columns.Add(equip.slot[0].itemID.ToString()); // Equip_Head
            columns.Add(equip.slot[3].itemID.ToString()); // Equip_Foot
            columns.Add(equip.slot[1].itemID.ToString()); // Equip_Staff
            columns.Add(equip.slot[2].itemID.ToString()); // Equip_Body
            columns.Add(inventory.potionSlot.GetComponent<UI_Inventory_Slot>().itemID.ToString()); // PotionSlot
            string tempInventory = "";
            for(int i = 0; i< inventory.slot.Length;i++ )
            {                
                tempInventory += inventory.slot[i].itemID.ToString();
                if (i < inventory.slot.Length-1) tempInventory += '|';
            }
            columns.Add(tempInventory);
            string tempItemCount = "";
            for (int i = 0; i < inventory.slot.Length; i++)
            {
                tempItemCount += inventory.slot[i].itemCount.ToString();
                if (i < inventory.slot.Length-1) tempItemCount += '|';
            }
            columns.Add(tempItemCount);
            columns.Add(info.Exp.ToString()); // EXP


            writer.WriteRow(columns);
        }
    }

    public void ResetData()
    {
        List<Dictionary<string, object>> m_dictionaryData2 = CSVReader.Read(m_strCSVFileName4);
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
            info.PotionSlot = int.Parse((m_dictionaryData2[i]["PotionSlot"].ToString()));
            string tempInventory = m_dictionaryData2[i]["Inventory"].ToString();
            string[] tempInventory2 = tempInventory.Split('|');
            for (int k = 0; k < tempInventory2.Length; k++)
            {
                info.Inventory.Add(int.Parse(tempInventory2[k]));
            }

            string itemCount = m_dictionaryData2[i]["ItemCount"].ToString();
            string[] itemCount2 = itemCount.Split('|');

            for (int k = 0; k < itemCount2.Length; k++)
            {
                info.itemCount.Add(int.Parse(itemCount2[k]));
            }

            info.Exp = int.Parse((m_dictionaryData2[i]["EXP"].ToString()));
        }
    }
    public void CheatData()
    {
        List<Dictionary<string, object>> m_dictionaryData2 = CSVReader.Read(m_strCSVFileName5);
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
            info.PotionSlot = int.Parse((m_dictionaryData2[i]["PotionSlot"].ToString()));
            string tempInventory = m_dictionaryData2[i]["Inventory"].ToString();
            string[] tempInventory2 = tempInventory.Split('|');
            for (int k = 0; k < tempInventory2.Length; k++)
            {
                info.Inventory.Add(int.Parse(tempInventory2[k]));
            }

            string itemCount = m_dictionaryData2[i]["ItemCount"].ToString();
            string[] itemCount2 = itemCount.Split('|');

            for (int k = 0; k < itemCount2.Length; k++)
            {
                info.itemCount.Add(int.Parse(itemCount2[k]));
            }

            info.Exp = int.Parse((m_dictionaryData2[i]["EXP"].ToString()));
        }
    }
}
