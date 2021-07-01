using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] icon;
    public bool isOpen;
    public Image image;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            isOpen = true;
            StartCoroutine(Open());
        }
        else
		{
            isOpen= false;
            for (int i = 0; i < icon.Length; i++)
            {
                icon[i].SetActive(false);
            }
        }
    }
    public IEnumerator Open()
    {

        yield return new WaitForSeconds(1f); 
        for (int i = 0; i < icon.Length; i++)
        {
            icon[i].SetActive(true);
        }
    }
    public IEnumerator Close()
    {

        yield return new WaitForSeconds(1f); 
    }

}