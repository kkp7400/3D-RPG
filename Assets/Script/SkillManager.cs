using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Playables;

public class SkillManager : MonoBehaviour
{

    public GameObject[] skillFX;
    public Camera sceneCamera;
    private bool onAim;
    private string useSkillName;
    public GameObject skillIndicator;

    public GameObject attack;
    public GameObject cast;
    public GameObject teleport;
    //[SerializeField]
    public GameObject[] bookFx;
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
        SkillIndex.Add("Meteor");
        onAim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerstate.state != Player_State.Casting)
        {
            cast.GetComponent<ParticleSystem>().Stop();
        }

        if (useSkill != "not")
        {
            if (useSkill == "FirePunch")
            {
                attack = GameObject.Find("FX_Fire_Explosion_01");
            }
            if (useSkill == "Meteor")
            {
                useSkillName = useSkill;
                playerstate.ChangeState(Player_State.Casted);
                onAim = true;
            }
            useSkill = "not";
        }


        //Aiming();

        if (onAim)
        {
            Aiming();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SpawnSkill();
            }

        }
        else
        {
            skillIndicator.transform.position = new Vector3(1000, 1000, 1000);
        }
    }

    public void Attack()
    {
        if (attack == null) return;
        attack.GetComponent<ParticleSystem>().Play();
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
    public void SpawnSkill()
    {

        playerstate.anim.SetTrigger("UseSkill");
        playerstate.ChangeState(Player_State.Idle);

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
            Instantiate(skillFX[index], hit.point, Quaternion.identity);//풀링 해야함
        }
        onAim = false;
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
            skillIndicator.transform.position = new Vector3(1000,1000,1000);
        }
    }
}


