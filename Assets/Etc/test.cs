using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("�浹�ǵ�!");
        if (other.tag == "Shield")
        {

           Debug.Log("���̽�!");
        }
        //Rigidbody body = other.GetComponent<Rigidbody>();
        //if (body)
        //{
        //    Vector3 direction = other.transform.position - transform.position;
        //    direction = direction.normalized;
        //    body.AddForce(direction * 5);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Shield")
        {
            rb.AddForce(new Vector3(-5,0,-5));
            Debug.Log("���̽�!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyBall")
        {

            Debug.Log("���̽�!");
        }
    }

    private void OnParticleTrigger()
    {

        Debug.Log("�ǳ�?");
    }
}
