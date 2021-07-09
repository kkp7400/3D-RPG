using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class SpellArrow : MonoBehaviour
{


	public List<Spell> spell;
	// Start is called before the first frame update
	[Serializable]
	public class Skill
	{
		public Image Profile;
		public Image[] arrow = new Image[8];
		public bool isSpell;
		public int isClear;
		public int maxClear;
	}

	public GameObject bookClearFx;
	public GameObject[] skills;
	public List<Skill> skill = new List<Skill>();
	public int inputNum;
	public float bookCool;
	public PlayerState playerState;
	public SpellBook spellBook;
	public SkillManager skillMgr;
	void Start()
	{
		for (int i = 0; i < skillMgr.SkillIndex.Count; i++)
		{
			for (int j = 0; j < spellBook.spell.Count; j++)
			{
				if (skillMgr.SkillIndex[i] == spellBook.spell[j].name)
				{
					for (int k = 0; k < spellBook.spell[j].arrow.Count; k++)
					{
						if (spellBook.spell[j].arrow[k] == "Up") skill[i].arrow[k].sprite = Resources.Load<Sprite>("Up");
						if (spellBook.spell[j].arrow[k] == "Down") skill[i].arrow[k].sprite = Resources.Load<Sprite>("Down");
						if (spellBook.spell[j].arrow[k] == "Left") skill[i].arrow[k].sprite = Resources.Load<Sprite>("Left");
						if (spellBook.spell[j].arrow[k] == "Right") skill[i].arrow[k].sprite = Resources.Load<Sprite>("Right");
					}
					if (spellBook.spell[j].name == "FirePunch") skill[i].Profile.sprite = Resources.Load<Sprite>("FirePunch");
					if (spellBook.spell[j].name == "Meteor") skill[i].Profile.sprite = Resources.Load<Sprite>("Meteor");
					skill[i].isSpell = true;
					skill[i].maxClear = spellBook.spell[j].totalArrow;
					skill[i].isClear = 0;

				}
			}
		}
		skillMgr = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillManager>();
		spell = spellBook.spell;
		//for (int i = 0; i < skill.Count; i++)
		//{
		//    skill.Add(new Skill());
		//    skill[i].isSpell = true;
		//    skill[i].isClear = 0;

		//}
		inputNum = 0;
		bookCool = 0f;

	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance.isOpen == false && Input.GetKeyDown(KeyCode.LeftControl) &&
		(playerState.state == Player_State.Idle || playerState.state == Player_State.Move))
		{
			//inputNum = 0;
			if (bookCool <= 0)
			{
				GameManager.instance.isOpen = true;
				StartCoroutine(Open());
				bookCool = 2f;
			}
		}
		else if (Input.GetKeyUp(KeyCode.LeftControl) && playerState.state == Player_State.Casting || GameManager.instance.isOpen == false)
		{
			inputNum = 0;
			for (int i = 0; i < skill.Count; i++)
			{
				skill[i].isSpell = true;
				skill[i].isClear = 0;
				for (int j = 0; j < skill[i].arrow.Length; j++)
				{
					skill[i].arrow[j].color = new Color32(255, 255, 255, 255);
				}
			}
			GameManager.instance.isOpen = false;
			for (int i = 0; i < skills.Length; i++)
			{
				skills[i].SetActive(false);
			}
		}
		if (GameManager.instance.isOpen)
		{
			if (Input.GetKeyDown(KeyCode.W))
				UseSkill("Up");
			if (Input.GetKeyDown(KeyCode.A))
				UseSkill("Left");
			if (Input.GetKeyDown(KeyCode.S))
				UseSkill("Down");
			if (Input.GetKeyDown(KeyCode.D))
				UseSkill("Right");
		}
		if (bookCool >= 0 && !GameManager.instance.isOpen)
		{
			bookCool -= Time.deltaTime;
		}
		//for(int i = 0;i < skill.Count; i++)
  //      {
			
  //      }
		//Debug.Log(bookCool);
	}
	public void UseSkill(string key)
	{
		for (int i = 0; i < skill.Count; i++)
		{
			//if (skill[i].arrow.Length > inputNum) continue;
			if (skill[i].isSpell == false) continue;
			else if (inputNum > skill[i].arrow.Length)
			{
				closeSkill(i);
				skill[i].isSpell = false;
			}

			if (skill == null || skill[i].arrow[inputNum] == null || skill[i].arrow[inputNum].sprite == null)
			{
				continue;
				Debug.Log("Bug");
			}
			else
			{

				if (skill[i].arrow[inputNum].sprite.name == key)
				{
					skill[i].arrow[inputNum].color = new Color32(0, 255, 255, 255);
					skill[i].isClear++;
					//spell[i].arrow[j].CrossFadeColor(new Color(120, 120, 120), 3, false, false);
				}
				else if (skill[i].arrow[inputNum].sprite.name != key)
				{

					closeSkill(i);
					skill[i].isSpell = false;
				}

				if (skill[i].isClear == skill[i].maxClear)
				{
					skillMgr.UseSkill(skill[i].Profile.sprite.name);
					bookClearFx.GetComponent<ParticleSystem>().Play();
					GameManager.instance.isOpen = false;
				}
			}
			
		}
		inputNum++;
	}
	public void closeSkill(int spellNum)
	{
		for (int i = 0; i < skill[spellNum].arrow.Length; i++)
		{
			skill[spellNum].arrow[i].color = new Color32(50, 50, 50, 255);
		}
		skill[spellNum].isClear = 0;
		skill[spellNum].isSpell = false;
	}
	public IEnumerator Open()
	{

		yield return new WaitForSeconds(1f);
		for (int i = 0; i < skills.Length; i++)
		{
			if (skill[i].Profile.sprite != null)
				skills[i].SetActive(true);
		}
		for (int i = 0; i < skill.Count; i++)
		{
			if (skill[i].Profile.sprite == null) skill[i].Profile.enabled = false;
			else if(skill[i].Profile.sprite != null) skill[i].Profile.enabled = true;
			for (int j = 0; j < skill[i].arrow.Length; j++)
			{
				if (skill[i].arrow[j].sprite == null)
					skill[i].arrow[j].enabled = false;
				else if(skill[i].arrow[j].sprite != null) skill[i].arrow[j].enabled = true;
			}
		}

	}
	public IEnumerator Close()
	{

		yield return new WaitForSeconds(1f);
	}
}

