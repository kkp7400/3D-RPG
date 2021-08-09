using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{

    Coroutine runningCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCameraShake(float _amount, float _duration)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }
        runningCoroutine = StartCoroutine(Shake(0.1f, 0.07f));
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        Vector3 lastTransform = transform.localPosition;
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + transform.localPosition;

            timer += Time.deltaTime;
            yield return null;
        }
        //yield break ;
        transform.localPosition = lastTransform;
    }
}
