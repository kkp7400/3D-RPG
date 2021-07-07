using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class SpellArrow : MonoBehaviour
{
    // Start is called before the first frame update
    [Serializable]
    public struct Spell
    {
        public Image[] arrow;
        public bool isSpell;
        public int isClear;
    }
    public GameObject[] skills;
    public Spell[] spell;
    private int inputNum;
    public float bookCool;
    public PlayerState playerState;
    void Start()
    {
        for (int i = 0; i < spell.Length; i++)
        {
            spell[i].isSpell = true;
            spell[i].isClear = 0;

        }
        inputNum = 0;
        bookCool = 0f;
    }

    // Update is called once per frame
    void Update()
    {
            if (GameManager.instance.isOpen == false && Input.GetKeyDown(KeyCode.LeftControl)&& 
            (playerState.state == Player_State.Idle || playerState.state == Player_State.Move))
            {
                if (bookCool <= 0)
                {
                    GameManager.instance.isOpen = true;
                    StartCoroutine(Open());
                    bookCool = 2f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl)&& playerState.state == Player_State.Casting)
            {
                inputNum = 0;
                for (int i = 0; i < spell.Length; i++)
                {
                    spell[i].isSpell = true;
                    for (int j = 0; j < spell[i].arrow.Length; j++)
                    {
                        spell[i].arrow[j].color = new Color32(255, 255, 255, 255);
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
        //Debug.Log(bookCool);
    }    
    public void UseSkill(string key)
    {
        for (int i = 0; i < spell.Length; i++)
        {
            if (spell[i].isSpell == false) continue;
            else if (inputNum > spell[i].arrow.Length)
            {
                closeSkill(i);
                spell[i].isSpell = false;
            }
            if (spell[i].arrow[inputNum].sprite.name == key)
            {
                spell[i].arrow[inputNum].color = new Color32(0, 255, 255, 255);
                spell[i].isClear++;
                //spell[i].arrow[j].CrossFadeColor(new Color(120, 120, 120), 3, false, false);
            }
            if (spell[i].arrow.Length - 1 == spell[i].isClear) ;//이부분에 스킬 사용
            else if (spell[i].arrow[inputNum].sprite.name != key)
            {

                closeSkill(i);
                spell[i].isSpell = false;
            }
        }
        inputNum++;
    }
    public void closeSkill(int spellNum)
    {
        for (int i = 0; i < spell[i].arrow.Length; i++)
        {
            spell[spellNum].arrow[i].color = new Color32(50, 50, 50, 255);
        }
    }
    public IEnumerator Open()
    {

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].SetActive(true);
        }
    }
    public IEnumerator Close()
    {

        yield return new WaitForSeconds(1f);
    }
}
