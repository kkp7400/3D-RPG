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
public class SpellBook : MonoBehaviour
{
    public string m_strCSVFileName = string.Empty;
   
    public List<Spell> spell = new List<Spell>();

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

        int a = 0;
        

    } 

    // Update is called once per frame
    void Update()
    {
	  
    }
}
