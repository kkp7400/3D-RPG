using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    public PlayerState player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.OnDamage(25);
        player.onHit = true;
    }
}
