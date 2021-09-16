using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Playables;
using UnityEngine.UI;
using System;

public class SkillManager : MonoBehaviour
{
    //FX

    public GameObject shiled;
    bool isShield;
    public GameObject attack;
    public GameObject cast;
    public GameObject teleport;
    public GameObject[] bookFx;
    public GameObject casting;
    public GameObject shieldDamage;
    public GameObject[] skillFX;
    public GameObject[] skillDamage;
    public Camera sceneCamera;
    public bool onAim;
    private string useSkillName;
    public GameObject skillIndicator;

    public SpellArrow spellArrow;
    private PlayerState playerstate;
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private string useSkill;

    public List<string> SkillIndex;
    void Awake()
    {

        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        {
            playerstate = GetComponent<PlayerState>();
        }
        SkillIndex.Add("FirePunch");
        SkillIndex.Add("EnergyBall");
        SkillIndex.Add("Meteor");
        SkillIndex.Add("Blizard");
        SkillIndex.Add("Shild");
        onAim = false;
        isShield = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (playerstate.state == Player_State.Die) return;

        if (playerstate.state != Player_State.Casting)
        {
            cast.GetComponent<ParticleSystem>().Stop();
        }

        if (useSkill != "not")
        {
            if (useSkill == "FirePunch")
            {
                if (attack && attack.name == "EnergyBall")
                {
                    for (int i = 0; i < spellArrow.skill.Count; i++)
                    {
                        if (spellArrow.skill[i].Profile.sprite.name == "EnergyBall")
                        {
                            spellArrow.skill[i].nowCount = 0;
                            break;
                        }
                    }
                }
                attack = GameObject.Find("FirePunch");
            }
            if (useSkill == "EnergyBall")
            {
                if (attack && attack.name == "FirePunch")
                {
                    for (int i = 0; i < spellArrow.skill.Count; i++)
                    {
                        if (spellArrow.skill[i].Profile.sprite.name == "FirePunch")
                        {
                            spellArrow.skill[i].nowCount = 0;
                            break;
                        }
                    }
                }
                attack = GameObject.Find("EnergyBall");
            }


            if (useSkill == "Meteor")
            {
                useSkillName = useSkill;
                playerstate.ChangeState(Player_State.Casted);
                onAim = true;
            }
            if (useSkill == "Blizard")
            {
                useSkillName = useSkill;
                playerstate.ChangeState(Player_State.Casted);
                onAim = true;
            }

            if (useSkill == "Shild")
            {
                isShield = true;
            }
            useSkill = "not";
        }


        //Aiming();

        if (onAim)
        {
            Aiming();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(SpawnSkill());
                onAim = false;
            }

        }
        else
        {
            skillIndicator.transform.position = new Vector3(1000, 1000, 1000);
        }
        StartCoroutine(Shield());

        
    }

    public void Attack()
    {
        if (attack == null) 
            return;
        sceneCamera.GetComponent<CameraShaker>().StartCineCameraShake(0.5f, 0.05f);
        attack.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < spellArrow.skill.Count; i++)
        {
            if (spellArrow.skill[i].Profile.sprite.name == attack.name)
            {
                if (spellArrow.skill[i].nowCount > 0)
                {
                    attack.GetComponent<ParticleSystem>().Play();
                    spellArrow.skill[i].nowCount -= 1;
                }
                else if (spellArrow.skill[i].nowCount <= 0)
                {
                    attack.GetComponent<ParticleSystem>().Stop();
                    attack = null;
                }
                break;
            }
        }
    }
    IEnumerator Shield()
    {
        if (isShield == false) yield break;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            sceneCamera.GetComponent<CameraShaker>().StartCineCameraShake(0.7f, 0.08f);
            shiled.GetComponent<ParticleSystem>().Play();
            shieldDamage.SetActive(true);
            isShield = false;
            for (int i = 0; i < spellArrow.skill.Count; i++)
            {
                if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == "Shild")
                {
                    if (spellArrow.skill[i].nowCount >= 0)
                        spellArrow.skill[i].nowCount -= 1;
                }
            }
            GameManager.instance.onShield = true;
            yield return new WaitForSeconds(0.1f);
            shieldDamage.SetActive(false);
            yield return new WaitForSeconds(5.5f);

            GameManager.instance.onShield = false;
        }
    }
    public void CastStart()
    {
        cast.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < bookFx.Length; i++)
        {
            bookFx[i].GetComponent<ParticleSystem>().Play();

        }

    }
    public void CastEnd()
    {
        cast.GetComponent<ParticleSystem>().Stop();
        cast.GetComponent<ParticleSystem>().loop = false;

        for (int i = 0; i < bookFx.Length; i++)
        {
            bookFx[i].GetComponent<ParticleSystem>().Stop();

        }
        cast.GetComponent<ParticleSystem>().loop = true;

    }

    public void TeleportStart()
    {
        float moveDistance =
            playerInput.v * 5;
        // 리지드바디를 통해 게임 오브젝트 위치 변경
        //playerRigidbody.MovePosition(moveDistance,0,0,);
        float moveDistance2 =
            playerInput.h * 5;
        transform.position += new Vector3(moveDistance2, 0, moveDistance);

        teleport.GetComponent<ParticleSystem>().Play();
    }

    public void UseSkill(string skill)
    {
        useSkill = skill;
    }
    public IEnumerator SpawnSkill()
    {

        playerstate.anim.SetTrigger("UseSkill");
        playerstate.ChangeState(Player_State.Idle);

        if (useSkillName == "Meteor") yield return new WaitForSeconds(1f);
        if (useSkillName == "Blizard") yield return new WaitForSeconds(2f);
        int index = 0;
        for (int i = 0; i < skillFX.Length; i++)
        {
            if (skillFX[i].name == useSkillName)
            {
                index = i;
            }
        }
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, 1 << LayerMask.NameToLayer("Ground")))
        {
            //Instantiate(skillFX[index], hit.point, Quaternion.identity);//풀링 해야함
            if (useSkillName == "Meteor") StartCoroutine(Meteor(hit.point, index));
            if (useSkillName == "Blizard") StartCoroutine(Blizard(hit.point, index));
        }
        yield break;
    }

    public void Aiming()
    {
        Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, 1 << LayerMask.NameToLayer("Ground")))
        {
            skillIndicator.transform.position = hit.point;
        }
        else
        {
            skillIndicator.transform.position = new Vector3(1000, 1000, 1000);
        }
    }
    public void CastingStart()
    {
        casting.GetComponent<ParticleSystem>().Play();
    }
    public void CastingEnd()
    {
        casting.GetComponent<ParticleSystem>().Stop();
        for(int i = 0; i < bookFx.Length; i++)
        {
            bookFx[i].SetActive(false);
        }
    }
    public IEnumerator Blizard(Vector3 point, int index)
    {
        for (int i = 0; i < spellArrow.skill.Count; i++)
        {
            if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == "Blizard")
            {
                if (spellArrow.skill[i].nowCount >= 0)
                    spellArrow.skill[i].nowCount -= 1;
            }
        }
        spellArrow.skill[index].nowCount -= 1;
        skillFX[index].transform.position = point;
        skillFX[index].GetComponent<ParticleSystem>().Play();
        //yield return new WaitForSeconds(1f);
        skillDamage[index].transform.position = point;

        sceneCamera.GetComponent<CameraShaker>().StartCineCameraShake(1f, 4f);
        yield return new WaitForSeconds(3f);
        skillFX[index].GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(0.2f);
        skillDamage[index].transform.position = new Vector3(1000, 1000, 1000);
        yield break;
    }

    public IEnumerator Meteor(Vector3 point, int index)
    {
        for (int i = 0; i < spellArrow.skill.Count; i++)
        {
            if (spellArrow.skill[i].Profile.GetComponent<Image>().sprite.name == "Meteor")
            {
                //if (spellArrow.skill[i].nowCount >= 0)
                spellArrow.skill[i].nowCount -= 1;
            }
        }
        skillFX[index].transform.position = point;
        skillFX[index].GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        skillDamage[index].transform.position = point;
        sceneCamera.GetComponent<CameraShaker>().StartCineCameraShake(1.5f, 2f);
        yield return new WaitForSeconds(5f);
        skillDamage[index].transform.position = new Vector3(1000, 1000, 1000);
        yield break;
    }



}


