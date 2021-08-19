using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShaker : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin shakeValue;
    float shakeValueAmp;
    float shakeValueFre;
    Coroutine runningCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        if(vcam) shakeValue = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //if(shakeValue == null)
        //{
        //    Debug.Log("¾ø´Âµ­");
        //}
        //shakeValue.m_NoiseProfile = Resources.Load<NoiseSettings>("Noise/6D Shake");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCineCameraShake(float _amount, float _duration)
    {
       if (runningCoroutine != null)
       {
           StopCoroutine(runningCoroutine);
       }
        
        runningCoroutine = StartCoroutine(CineShake(_amount, _duration));
    }
    public void StartCameraShake(float _amount, float _duration)
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        runningCoroutine = StartCoroutine(Shake(_amount, _duration));
    }

    public IEnumerator CineShake(float _amount, float _duration)
    {
        //Vector3 lastTransform = transform.localPosition;
        float timer = 0;
        while (timer <= _duration)
        {
            //transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + transform.localPosition;
            shakeValue.m_AmplitudeGain = _amount;
            shakeValue.m_FrequencyGain = _amount;

             timer += Time.deltaTime;
            yield return null;
        }
        //yield break ;
       // transform.localPosition = lastTransform;
        shakeValue.m_AmplitudeGain = 0f;
        shakeValue.m_FrequencyGain = 0f;
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        //int a = 0;
        Vector3 lastTransform = transform.localPosition;
        float timer = 0;
        while (timer <= _duration)
        {
            transform.localPosition = (Vector3)UnityEngine.Random.insideUnitCircle * _amount + transform.localPosition;
            //Debug.Log("Èçµé¸°´Ù" + a);
            //a++;
            timer += Time.deltaTime;
            yield return null;
        }
        //yield break ;
        transform.localPosition = lastTransform;
    }
}
