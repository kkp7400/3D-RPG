using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCredit : MonoBehaviour
{
    float a = 0;
    public float fadeCount = 255;
    public Image image;
    bool fadeIn;
    bool fadeOut;
    bool textScroll;
    bool fadeText;

    public GameObject r;
    public GameObject t;
    public GameObject screenSize;
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        fadeCount = 255;
        fadeIn = true;
        fadeOut = true;
        textScroll = false;
        fadeText = true;
        size = r.GetComponent<RectTransform>().anchoredPosition.y;//screenSize.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        size = r.GetComponent<RectTransform>().anchoredPosition.y;
        if (fadeIn)
        {
            StartCoroutine(FadeInCoroutine());
            fadeIn = false;
        }
        //a += Time.deltaTime;

        //if (a > 7f)
        //{
        //    this.gameObject.transform.position += new Vector3(10 * Time.deltaTime, 0f);
        //    if (fadeOut)
        //    {
        //        StartCoroutine(FadeOutCoroutine());
        //        fadeOut = false;
        //    }
        //}
        //if (textScroll)
        //{
        //    if (size <= 400f)
        //    {
        //        r.transform.position += new Vector3(0f, 100 * Time.deltaTime);
        //    }
        //    else if (size > 400f)
        //    {
        //        if (fadeText)
        //        {
        //            StartCoroutine(FadeInText());
        //            fadeText = false;
        //        }
        //    }
        //}

    }
    public IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    public IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        r.SetActive(true);

        yield return new WaitForSeconds(2f);
        textScroll = true;
    }

    public IEnumerator FadeInText()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            t.GetComponent<Text>().color = new Color(255, 255, 255, fadeCount);
        }
        yield return new WaitForSeconds(3f);
    }
}
