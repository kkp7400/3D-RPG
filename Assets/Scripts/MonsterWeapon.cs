using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    public float atk;
    public DataBase DB;
    // Start is called before the first frame update
    void Start()
    {
        DB = GameObject.Find("GM").GetComponent<DataBase>();
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Ghost") atk = 0.3f;
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Skeleton") atk = 0.2f;
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Dog") atk = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.OnDamage(atk);
        }
    }
}
