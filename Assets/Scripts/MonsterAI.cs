using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_State
{
    Idle, Trace, Attack, Hit, Death
}

public class MonsterAI : MonoBehaviour
{
    public TextMesh damageText;
    public GameObject Coin;
    public GameObject Star;
    public GameObject Damage;
    public float HP;
    public DataBase DB;
    public UI_Equip equip;
    public bool isDead = false;
    
    [SerializeField]
    AI_State state;

    Animator anim;
    public GameObject enemy;
    public NavMeshAgent nav;
    // Move
    float moveValue = 0f;
    public EnemySpawner spawner;
    // State Patrol
    public GameObject targetObject;
    Vector3 targetPos;
    float patrolMoveSpeed = 1f;
    float patrolRotateSpeed = 1f;

    // State Trace
    float traceMoveSpeed = 2f;
    float traceRotateSpeed = 3f;

    public ObjectPool objPool;

    // Start is called before the first frame update
    void Start()
    {
        
        if (this.tag == "Dog") HP = 500f;
        if (this.tag == "Skeleton") HP = 1500f;
        if (this.tag == "Ghost") HP = 2000f;

        nav = transform.GetComponent<NavMeshAgent>();
        spawner = GameObject.Find("DungeonManager").GetComponent<EnemySpawner>();
        // Component
        anim = GetComponent<Animator>();
        objPool = GameObject.Find("DungeonManager").GetComponent<ObjectPool>();
        state = AI_State.Idle;

        Coin = transform.Find("Coin").gameObject;
        Star = transform.Find("Star").gameObject;
        damageText = transform.Find("DamageText").GetComponent<TextMesh>();
        ChangeState(state);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), true);
        //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Monster"), LayerMask.NameToLayer("Monster"), true);

    }

    // Update is called once per frame
    void Update()
    {
        if (!DB) DB = GameObject.Find("GM").GetComponent<DataBase>();
        if (!equip) equip = GameObject.Find("Canvas").GetComponent<UI_Equip>();
        if (!enemy) enemy = GameObject.Find("Player");

        if (HP <= 0 && !isDead)
        {
            ChangeState(AI_State.Death);
            isDead = true;
        }

        if (state != AI_State.Hit)
            damageText.gameObject.SetActive(false);
        var enemyPos = enemy.transform.position;
        enemyPos.y = transform.position.y;
        targetPos = enemyPos;
        // �� ������ ����Ǵ� �ڵ�
        switch (state)
        {
            case AI_State.Idle: UpdateIdle(); break;
            case AI_State.Trace: UpdateTrace(); break;
            case AI_State.Attack: UpdateAttack(); break;
            case AI_State.Hit: UpdateHit(); break;
            case AI_State.Death: UpdateDeath(); break;
        }
        anim.SetFloat("MoveValue", moveValue);
    }

    void UpdateHit()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Hit"))
        {
            float normalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normalizedTime >= 0.8f)
            {
                ChangeState(AI_State.Idle);
                return;
            }
        }
    }
    void UpdateIdle()
    {
        if (FindEnemy())
        {
            ChangeState(AI_State.Trace);
            return;
        }
    }



    void UpdateTrace()
    {
        if (isDead) return;
        Vector3 dir = targetPos - transform.position;
        float dist = dir.magnitude;

        if (dist < 2f)
        {
            ChangeState(AI_State.Attack);
            return;
        }
        //nav.SetDestination(targetPos);
        moveValue = Mathf.Clamp(moveValue + Time.deltaTime, 0f, 1f);

        Quaternion targetRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, traceRotateSpeed * Time.deltaTime);

        transform.position += transform.forward * moveValue * traceMoveSpeed * Time.deltaTime;

    }

    void UpdateAttack()
    {
        Damage.GetComponent<BoxCollider>().enabled = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
        {
            float normalizedTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (normalizedTime >= 0.8f)
            {
                ChangeState(AI_State.Idle);
                return;
            }
        }
    }

    void UpdateDeath()
    {

    }
    void ChangeState(AI_State nextState)
    {
        state = nextState;
        Damage.GetComponent<BoxCollider>().enabled = false;
        anim.SetBool("Idle", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Hit", false);

        if (isDead) return;
        StopAllCoroutines();

        switch (state)
        {
            case AI_State.Idle: StartCoroutine(CoroutineIdle()); break;
            case AI_State.Trace: StartCoroutine(CoroutineTrace()); break;
            case AI_State.Attack: StartCoroutine(CoroutineAttack()); break;
            case AI_State.Hit: StartCoroutine(CoroutineHit()); break;
            case AI_State.Death: StartCoroutine(CoroutineDeath()); break;
        }
    }

    IEnumerator CoroutineHit()
    {
        // ���ʿ� �ѹ� ����Ǵ� �ڵ�
        anim.SetTrigger("Hit");
        yield break;
    }
    IEnumerator CoroutineIdle()
    {
        // ���ʿ� �ѹ� ����Ǵ� �ڵ�
        anim.SetBool("Idle", true);

        moveValue = 0f;
        yield break;
    }


    IEnumerator CoroutineDeath()
    {
       // gameObject.GetComponent<CapsuleCollider>().enabled = false;
        anim.SetTrigger("Death");
        spawner.deathMonsterAmount++;
        isDead = true;
        yield return new WaitForSeconds(1f);
       //float x = Random.Range(-0.1f, 0.1f);
       //float z = Random.Range(-0.1f, 0.1f);
       //Coin.transform.position = new Vector3(x, Coin.transform.position.y, z);
       //Star.transform.position = new Vector3(x, Star.transform.position.y, z);
        Coin.SetActive(true);
        Star.SetActive(true);
        Star.GetComponent<ParticleSystem>().Play();
        if (this.tag == "Skeleton")
        {
            DB.info.GOLD += 100;
            DB.info.Exp += 1;
        }
        if (this.tag == "Dog") 
        {
            DB.info.GOLD += 50;
            DB.info.Exp += 2;
        }
        if (this.tag == "Ghost")
        {
            DB.info.GOLD += 30;
            DB.info.Exp += 3;
        }
        yield return new WaitForSeconds(2f);
        // gameObject.GetComponent<CapsuleCollider>().enabled = true;
        isDead = false;
        objPool.ReturnToPool(gameObject.tag, gameObject);
        yield break;
    }

    IEnumerator CoroutineTrace()
    {
        // ���ʿ� �ѹ� ����Ǵ� �ڵ�
        Vector3 dir = targetPos - transform.position;
        float dist = dir.magnitude;
        if(dist >= 2f) anim.SetBool("Run", true);

        // Ÿ�� ��ġ�� ���� ��ġ�� ����
        

        yield break;
        //while (true)
        //{
        //    yield return new WaitForSeconds(1f);

        //    ChangeState(AI_State.Attack);
        //    yield break;
        //}
    }

    IEnumerator CoroutineAttack()
    {
        // ���ʿ� �ѹ� ����Ǵ� �ڵ�
        anim.SetBool("Attack", true);

        moveValue = 0f;

        while (true)
        {

            yield break;
            // TODO : �ִϸ��̼� ���� Ȯ�� �� ���� ��ȯ
            //yield return new WaitWhile(()=>);
            //ChangeState(AI_State.Idle);
        }
    }



    private bool FindEnemy()
    {

        enemy = GameObject.Find("Player");
        if (enemy)
        {
            return true;
        }
        else
        {
            return false;
        }
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
            if (!isDead) ChangeState(AI_State.Hit);
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
            if (!isDead) ChangeState(AI_State.Hit);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Blizard")
        {
            onBlizard = false;
            ChangeState(AI_State.Idle);
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
            damageText.text = "-"+System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true);
            if (!isDead) ChangeState(AI_State.Hit);
        }
        if (other.tag == "EnergyBall")
        {
            HP -= (DB.info.SP_EnergyBall * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[1].damage;
            float damage = (DB.info.SP_EnergyBall * 0.1f + 1f) * (equip.ATK + 1f) * DB.spell[1].damage;
            damageText.text = "-" + System.Math.Round(damage).ToString();
            damageText.gameObject.SetActive(true); 
            if (!isDead) ChangeState(AI_State.Hit);
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
            if(!isDead) ChangeState(AI_State.Hit);
            yield return new WaitForSeconds(1f);
        }
    }


}
