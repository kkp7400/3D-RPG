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
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Ghost") atk = 3;
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Skeleton") atk = 2;
        if (transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.tag == "Dog") atk = 1;

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
