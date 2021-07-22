using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDungeon : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LoadingSceneManager.LoadScene("Dungeon");
        }
        
    }
}
