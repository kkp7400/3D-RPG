using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isStart = true;
            transform.position = new Vector3(700, 700, 700);
        }
    }
}
