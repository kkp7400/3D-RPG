using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Ãæµ¹");
        //Rigidbody body = other.GetComponent<Rigidbody>();
        //if (body)
        //{
        //    Vector3 direction = other.transform.position - transform.position;
        //    direction = direction.normalized;
        //    body.AddForce(direction * 5);
        //}
    }
}
