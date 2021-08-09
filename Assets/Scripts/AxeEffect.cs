using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AxeEffect : MonoBehaviour
{
    public ParticleSystem AxeFX;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 pos = collision.contacts[0].point;
        if (collision.gameObject.tag == "Ground")
        {
            AxeFX.transform.position = pos;
            AxeFX.Play();
        }
    }
}
