using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillManager : MonoBehaviour
{
    public GameObject attack;
    public GameObject cast;
    public GameObject teleport;
    //[SerializeField]
    public GameObject[] bookFx;
    private PlayerState playerstate;
    private PlayerInput playerInput; // �÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody playerRigidbody; // �÷��̾� ĳ������ ������ٵ�
    public List<string> SkillIndex = new List<string>();
    void Awake()
    {
        // ����� ������Ʈ���� ������ ��������
        playerInput = GetComponent<PlayerInput>();
        {
            playerstate = GetComponent<PlayerState>();
        }
        SkillIndex.Add("FirePunch");
        SkillIndex.Add("Meteor");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerstate.state != Player_State.Casting)
        {
            cast.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void Attack()
    {
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
        // ������ٵ� ���� ���� ������Ʈ ��ġ ����
        //playerRigidbody.MovePosition(moveDistance,0,0,);
        float moveDistance2 =
            playerInput.h * 5;
        transform.position += new Vector3(moveDistance2, 0, moveDistance);

        teleport.GetComponent<ParticleSystem>().Play();
    }
}

   
