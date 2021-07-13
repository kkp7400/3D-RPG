using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    public SpellArrow spellArrow;
    public PlayerState playerState;
    [Serializable]
    public class SkillIcon
    {
        public string name;
        public GameObject icon;
        public GameObject cool;
        public GameObject count;
    }
    public SkillIcon[] Icon;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spellArrow.skill.Count; i++)
        {
            for (int j = 0; j < Icon.Length; j++)
            {
               // if (spellArrow.skill[i].Profile.name == Icon[j].name)
                {
                    if (spellArrow.skill[i].nowCount > 0)//활성
                    {

                        if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == Icon[j].name)
                        {
                            Icon[j].icon.GetComponent<Image>().fillAmount = 1f;
                            Icon[j].icon.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
                            Icon[j].count.SetActive(true);
                            Icon[j].cool.SetActive(false);
                            Icon[j].count.GetComponent<Text>().text = spellArrow.skill[i].nowCount.ToString();
                        }

                    }
                    else if (spellArrow.skill[i].nowCoolTime > 0 && spellArrow.skill[i].nowCount <= 0)//쿨
                    {
                        if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == Icon[j].name)
                        {
                            Icon[j].icon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
                            Icon[j].icon.GetComponent<Image>().fillAmount = spellArrow.skill[i].nowCoolTime / spellArrow.skill[i].coolTime;
                            Icon[j].count.SetActive(false);
                            Icon[j].cool.SetActive(true);
                            Icon[j].cool.GetComponent<Text>().text = spellArrow.skill[i].nowCoolTime.ToString();
                        }
                    }
                    else if (spellArrow.skill[i].nowCoolTime <= 0 && spellArrow.skill[i].nowCount <= 0)//기본 상태
                    {
                        if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == Icon[j].name)
                        {
                            Icon[j].icon.GetComponent<Image>().fillAmount = 0f;
                            Icon[j].count.SetActive(false);
                            Icon[j].cool.SetActive(false);
                        }
                    }
                }
            }
        }

    }
}
