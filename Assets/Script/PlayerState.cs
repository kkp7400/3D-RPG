using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[SerializeField]
public enum Player_State
{
    Idle, Move, SpellMove, Attack, Casting, Casted, Skill, Teleport, Hit, Down, Back, Die
}
public class PlayerState : MonoBehaviour
{
    [Serializable]
    public class FlashIcon
    {
        public string name;
        public GameObject icon;
        public GameObject coolIcon;
        public float cool;
        public float MaxCool;
    }
    public Camera sceneCamera;
    public FlashIcon flash;
    public Animator anim;
    PlayerInput input;
    PlayerMovement movement;
    [SerializeField]
    public GameObject[] book;
    public Player_State state;
    public bool onNPC;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();

        state = Player_State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetAnimatorTransitionInfo(0).IsUserName("AttackToIdle"))
            book[1].SetActive(true);

        if(flash.cool >0)
        {
            flash.cool -= Time.deltaTime;
        }
        if(flash.cool<=0)
        {
            flash.icon.GetComponent<Image>().fillAmount = 1f;
            flash.icon.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            flash.coolIcon.SetActive(false);
        }
        else if(flash.cool >0)
        {
            flash.icon.GetComponent<Image>().fillAmount = flash.cool/flash.MaxCool;
            flash.icon.GetComponent<Image>().color = new Color(255, 255, 255, 0.5f);
            flash.coolIcon.SetActive(true) ;
            flash.coolIcon.GetComponent<Text>().text = flash.cool.ToString();
        }
        switch (state)
        {
            case Player_State.Idle: UpdateIdle(); break;
            case Player_State.Move: UpdateMove(); break;
            case Player_State.SpellMove: UpdateSpellMove(); break;
            case Player_State.Attack: UpdateAttack(); break;
            case Player_State.Casting: UpdateCasting(); break;
            case Player_State.Casted: UpdateCasted(); break;
            case Player_State.Skill: UpdateSkill(); break;
            case Player_State.Teleport: UpdateTeleport(); break;
            case Player_State.Hit: UpdateHit(); break;
            case Player_State.Down: UpdateDown(); break;
            case Player_State.Back: UpdateBack(); break;
            case Player_State.Die: UpdateDie(); break;

        }
    }
    public void ChangeState(Player_State nextState)
    {
        state = nextState;

        anim.SetBool("IsCast", false);
        anim.SetBool("IsRun", false);
        anim.SetBool("IsSkill", false);

        book[1].SetActive(false);
        movement.isMove = false;
        // anim.SetBool("Run", false);
        // anim.SetBool("Attack", false);

        StopAllCoroutines();

        switch (state)
        {
            case Player_State.Idle: StartCoroutine(CoroutineIdle()); break;
            case Player_State.Move: StartCoroutine(CoroutineMove()); break;
            case Player_State.SpellMove: StartCoroutine(CoroutineSpellMove()); break;
            case Player_State.Attack: StartCoroutine(CoroutineAttack()); break;
            case Player_State.Casting: StartCoroutine(CoroutineCasting()); break;
            case Player_State.Casted: StartCoroutine(CoroutineCasted()); break;
            case Player_State.Skill: StartCoroutine(CoroutineSkill()); break;
            case Player_State.Teleport: StartCoroutine(CoroutineTeleport()); break;
            case Player_State.Hit: StartCoroutine(CoroutineHit()); break;
            case Player_State.Down: StartCoroutine(CoroutineDown()); break;
            case Player_State.Back: StartCoroutine(CoroutineBack()); break;
            case Player_State.Die: StartCoroutine(CoroutineDie()); break;

        }
    }

    void UpdateIdle()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            ChangeState(Player_State.Move);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0)&& !onNPC)
        {
            ChangeState(Player_State.Attack);
            return;
        }
        if (GameManager.instance.isOpen && !onNPC)
        {

            ChangeState(Player_State.Casting);
            return;
        }
    }
    void UpdateMove()
    {
        if (anim.GetAnimatorTransitionInfo(0).IsUserName("toRun"))
            movement.isMove = true;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            ChangeState(Player_State.Idle);
            return;
        }
        if (Input.GetKey(KeyCode.Mouse0) && !onNPC)
        {
            ChangeState(Player_State.Attack);
            return;
        }
        if (GameManager.instance.isOpen && !onNPC)
        {

            ChangeState(Player_State.Casting);
            return;
        }
        if (Input.GetKey(KeyCode.Space)&&flash.cool <= 0f && !onNPC)
        {
            flash.icon.GetComponent<Image>().fillAmount = 0f;
            flash.cool = flash.MaxCool;
            ChangeState(Player_State.Teleport);
            return;
        }
    }
    void UpdateSpellMove()
    {
        movement.moveSpeed = 2f;
    }
    void UpdateAttack()
    {

        movement.isMove = false;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray cameraRay = sceneCamera.ScreenPointToRay(Input.mousePosition);

            Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;

            if (GroupPlane.Raycast(cameraRay, out rayLength))

            {

                Vector3 pointTolook = cameraRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

            }
            anim.SetTrigger("OnCombo");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            anim.ResetTrigger("OnCombo");
            ChangeState(Player_State.Idle);
            return;
        }
        else
        {
            //anim.ResetTrigger("OnCombo");
        }
    }
    void UpdateCasting()
    {
        if (!GameManager.instance.isOpen)
        {

            book[0].SetActive(false);
            ChangeState(Player_State.Idle);
            return;
        }
    }
    void UpdateCasted()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ChangeState(Player_State.Idle);
        }
    }
    void UpdateSkill()
    {

    }
    void UpdateTeleport()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            ChangeState(Player_State.Idle);
            return;
        }
    }
    void UpdateHit()
    {

    }
    void UpdateDown()
    {

    }
    void UpdateBack()
    {

    }
    void UpdateDie()
    {

    }


    IEnumerator CoroutineIdle()
    {
        book[1].SetActive(true);
        yield break;
    }
    IEnumerator CoroutineMove()
    {
        book[1].SetActive(true);
        anim.SetBool("IsRun", true);
        yield break;
    }
    IEnumerator CoroutineSpellMove()
    {
        yield break;
    }
    IEnumerator CoroutineAttack()
    {
        for (int i = 0; i < book.Length; i++)
        {
            book[i].SetActive(false);
        }
        Ray cameraRay = sceneCamera.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))

        {

            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

        }
        anim.SetTrigger("OnCombo");
        yield break;
    }
    IEnumerator CoroutineCasting()
    {

        book[0].SetActive(true);
        book[1].SetActive(false);
        anim.SetBool("IsCast", true);
        yield break;
    }
    IEnumerator CoroutineCasted()
    {
        anim.SetBool("IsSkill", true);
        yield break;
    }
    IEnumerator CoroutineSkill()
    {
        yield break;
    }
    IEnumerator CoroutineTeleport()
    {
        anim.SetTrigger("OnTeleport");
        yield break;
    }
    IEnumerator CoroutineHit()
    {
        yield break;
    }
    IEnumerator CoroutineDown()
    {
        yield break;
    }
    IEnumerator CoroutineBack()
    {
        yield break;
    }
    IEnumerator CoroutineDie()
    {
        yield break;
    }
}
