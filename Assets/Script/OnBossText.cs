using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class OnBossText : MonoBehaviour
{
    public bool onBossText = false;

    public Text[] bossText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(onBossText)
        {
            StartCoroutine(PowerStart());
            onBossText = false;
        }
    }

    public IEnumerator PowerStart()
    {
        bossText[0].gameObject.SetActive(true);
        bossText[0].GetComponent<PlayableDirector>().Play();
        while (bossText[0].GetComponent<PlayableDirector>().time < 0.29f)
        {
            yield return null;
        }
        float currtime0 = 0;
        while (currtime0 < 0.15f)
        {
            currtime0 += Time.deltaTime;
            yield return null;
        }

        bossText[1].gameObject.SetActive(true);
        bossText[1].GetComponent<PlayableDirector>().Play();
        while (bossText[1].GetComponent<PlayableDirector>().time < 0.29f)
        {
            yield return null;
        }
        float currtime1 = 0;
        while (currtime1 < 0.15f)
        {
            currtime1 += Time.deltaTime;
            yield return null;
        }

        bossText[2].gameObject.SetActive(true);
        bossText[2].GetComponent<PlayableDirector>().Play();
        while (bossText[2].GetComponent<PlayableDirector>().time < 0.29f)
        {
            yield return null;
        }
        float currtime2 = 0;
        while (currtime2 < 0.15f)
        {
            currtime2 += Time.deltaTime;
            yield return null;
        }

        bossText[3].gameObject.SetActive(true);
        bossText[3].GetComponent<PlayableDirector>().Play();
        while (bossText[3].GetComponent<PlayableDirector>().time < 0.29f)
        {
            yield return null;
        }

        
        float currtime3 = 0;
        while (currtime3 < 1.3f)
        {
            currtime3 += Time.deltaTime;
            yield return null;
        }
        for (int i = 0; i < bossText.Length; i++)
        {
            bossText[i].gameObject.SetActive(false);
        }
    }
}
