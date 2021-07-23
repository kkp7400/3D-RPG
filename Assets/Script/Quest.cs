using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest
{
    public int ID;
    public string script;
    public int maxPerform;
    public int nowPerform;
    public bool isAccept;
    public bool isClear;
}
public class UI_Quest : MonoBehaviour
{
    public Quest[] quest;

    public GameObject npcUI;
    public GameObject playerUI;


    bool isQuest = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < quest.Length;i++)
		{
            quest[i].isAccept = false;
            quest[i].isClear = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isQuest = !isQuest;
            npcUI.SetActive(isQuest);

            GameManager.instance.UpdateUI();
        }
    }

    public void UpdateUI()
	{
        for (int i = 0; i < quest.Length; i++)
        {
            if (!quest[i].isAccept)
			{
                //npcUI.transform.FindChild(quest[i].ID.ToString()).GetComponent<>

            }
        }
	}
}
