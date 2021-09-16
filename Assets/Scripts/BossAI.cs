using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossAI_State
{
    Idle, PhaseTwo, Slash, Chop,Strike , Death
}

public class BossAI : MonoBehaviour
{
    public BossEvent bossEvent;
    [SerializeField]
    public BossAI_State state;
    NavMeshAgent nav;
    Animator anim;
    public float MaxHP;
    public float HP;
    private GameObject player;
    public float coolTime;
    bool onPhaseTwo = false;
    public TextMesh damageText;
    public DataBase DB;
    public UI_Equip equip;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        HP = 10000f;
        MaxHP = 10000f;
        anim = this.GetComponent<Animator>();
        state = BossAI_State.Idle;
        nav = this.GetComponent<NavMeshAgent>();
        coolTime = 0f;
        bossEvent = GameObject.Find("EventManager").GetComponent<BossEvent>();
        ChangeState(state);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DB) DB = GameObject.Find("GM").GetComponent<DataBase>();
        if (!equip) equip = GameObject.Find("Canvas").GetComponent<UI_Equip>();

        if (!bossEvent.bossBattleStart) return;
        

        switch (state)
        {
            case BossAI_State.Idle: UpdateIdle(); break;
            case BossAI_State.PhaseTwo: UpdatePhaseTwo(); break;
            case BossAI_State.Slash: UpdateSlash(); break;
            case BossAI_State.Strike: UpdateStrike(); break;
            case BossAI_State.Chop: UpdateChop(); break;
            case BossAI_State.Death: UpdateDeath(); break;
        }
    }

    void UpdateIdle()
    {
        if (HP <= 5000 && !onPhaseTwo)
        {
            ChangeState(BossAI_State.PhaseTwo);
            onPhaseTwo = true;
            
        }
        coolTime -= Time.deltaTime;
        if (coolTime<=0)
        {
            int useSkill = Random.Range(2, 5);
            if(useSkill == 2) ChangeState(BossAI_State.Slash);
            else if (useSkill == 3) ChangeState(BossAI_State.Strike);
            else if (useSkill == 4) ChangeState(BossAI_State.Chop);
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                Vector3 dir = player.GetComponent<Transform>().position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, 1.5f * Time.deltaTime);
            }
        }

    }



    void UpdateSlash()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Slash"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                ChangeState(BossAI_State.Idle);
                return;
            }
        }
    }

    void UpdateChop()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Chop"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                ChangeState(BossAI_State.Idle);
                return;
            }
        }
    }

    void UpdateStrike()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Strike"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                ChangeState(BossAI_State.Idle);
                return;
            }
        }
    }
    void UpdatePhaseTwo()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.PhaseTwo"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                ChangeState(BossAI_State.Idle);
                return;
            }
        }
    }
    void UpdateDeath()
    {

    }
    void ChangeState(BossAI_State nextState)
    {
        state = nextState;
        coolTime = 4f;
        StopAllCoroutines();

        switch (state)
        {
            case BossAI_State.Idle: StartCoroutine(CoroutineIdle()); break;
            case BossAI_State.Slash: StartCoroutine(CoroutineSlash()); break;
            case BossAI_State.Strike: StartCoroutine(CoroutineStrike()); break;
            case BossAI_State.Chop: StartCoroutine(CoroutineChop()); break;
            case BossAI_State.Death: StartCoroutine(CoroutineDeath()); break;
        }
    }

    IEnumerator CoroutineIdle()
    {

        yield break;
    }

    IEnumerator CoroutineSlash()
    {

        anim.SetTrigger("Slash");
        yield break;
    }

    IEnumerator CoroutineChop()
    {

        anim.SetTrigger("Chop");
        yield break;
    }

    IEnumerator CoroutineStrike()
    {

        anim.SetTrigger("Strike");
        yield break;
    }

    IEnumerator CoroutineDeath()
    {

        yield break;
    }
    IEnumerator CoroutinePhaseTwo()
    {
        anim.SetTrigger("PhaseTwo");
        yield break;
    }

    bool onBlizard;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteor")
        {
            HP -= (DB.info.SP_Meteor * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[2].damage;
            float damage = (DB.info.SP_Meteor * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[2].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
        }
        if (other.tag == "Blizard")
        {
            onBlizard = true;
            StartCoroutine(Blizard());
        }
        if (other.tag == "Shield")
        {
            transform.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(-1000, 0, -1000));
            HP -= (DB.info.SP_Shild * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[4].damage;

            float damage = (DB.info.SP_Shild * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[4].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Blizard")
        {
            onBlizard = false;
            StopCoroutine(Blizard());
        }
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("�浹�ǵ�!");
        if (other.tag == "FirePunch")
        {
            HP -= (DB.info.SP_FirePunch * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[0].damage;

            float damage = (DB.info.SP_FirePunch * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[0].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
        }
        if (other.tag == "EnergyBall")
        {
            HP -= (DB.info.SP_EnergyBall * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[1].damage;
            float damage = (DB.info.SP_EnergyBall * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[1].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
        }
        //Rigidbody body = other.GetComponent<Rigidbody>();
        //if (body)
        //{
        //    Vector3 direction = other.transform.position - transform.position;
        //    direction = direction.normalized;
        //    body.AddForce(direction * 5);
        //}
    }


    IEnumerator Blizard()
    {
        while (onBlizard)
        {
            HP -= (DB.info.SP_Blizard * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[3].damage;
            float damage = (DB.info.SP_Blizard * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[3].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
