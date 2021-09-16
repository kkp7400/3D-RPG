using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterWeapon : MonoBehaviour
{
    public float atk;
    public GameObject Center;
    public DataBase DB;
    public float knockPower;
    public float knockCool;
    public PlayerState player;
    // Start is called before the first frame update
    void Start()
    {
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        if (transform.parent)
        {
            if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Ghost") atk = 0.3f;
            if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Skeleton") atk = 0.2f;
            if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Dog") atk = 0.1f;
        }
        if (this.tag == "Slash") atk = 20f;
        if (this.tag == "Chop") atk = 30f;
        if (this.tag == "Strike") atk = 40f;
        Center = GameObject.Find("Boss");
        knockCool = 0f;
        player = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockCool >= 0f)
        {
            knockCool -= Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && knockCool <= 0f)
        {
            GameManager.instance.OnDamage(atk);
            if ((this.tag == "Slash" || this.tag == "Strike" || this.tag == "Chop") )
            {
                StartCoroutine(KnockBack(other.gameObject));
                knockCool = 3f;
                player.onHit = true;
            }
        }

    }

    IEnumerator KnockBack(GameObject other)
    {
        float knockTime = 1;
        while (/*!other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Hit")*/knockTime>=0f)
        {
            Vector3 dir = other.transform.position - Center.transform.position;
            dir.y = 0f;
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir.normalized * knockPower, ForceMode.Impulse);
           // Debug.Log("³Ë¹é?");
            knockTime -= Time.deltaTime;
            yield return null;
        }
    }
}
