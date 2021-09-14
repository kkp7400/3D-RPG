using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooAtObject : MonoBehaviour
{
    public GameObject Player;
    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        target.x = Player.transform.position.x;
        target.z = Player.transform.position.z;
        target.y = 0;
        transform.LookAt(target);
    }
}
