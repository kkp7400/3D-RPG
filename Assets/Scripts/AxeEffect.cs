using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AxeEffect : MonoBehaviour
{
    public EffectInfo[] Effects;
    public GameObject damageBox;
    [System.Serializable]

    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
    }
    void Start()
    {

    }

    void PassEffectInfo(int effectNum)
    {
        StartCoroutine(PassPlayEffect(Effects[effectNum]));
        StartCoroutine(DamageBox(effectNum));

    }

    IEnumerator PassPlayEffect(EffectInfo effect)
    {
        Transform startPos = effect.Effect.transform;
        float afterTime = effect.DestroyAfter;

        effect.Effect.transform.position = effect.StartPositionRotation.position;
        effect.Effect.transform.rotation = effect.StartPositionRotation.rotation;

        ParticleSystem[] particle;

        particle = effect.Effect.transform.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Play();
        }

        while (afterTime >= 0)
        {
            afterTime -= Time.deltaTime;
            yield return null;
        }

        effect.Effect.transform.position = startPos.position;
        effect.Effect.transform.rotation = startPos.rotation;

        yield break;
    }
    IEnumerator DamageBox(int skillNum)
    {
        
        float afterTime = 3f;
        Transform startPos = damageBox.transform;
        damageBox.transform.position = this.transform.position;
        damageBox.transform.rotation = this.transform.rotation;
        while (afterTime >= 0)
        {
            afterTime -= Time.deltaTime;
            damageBox.transform.position += Vector3.forward * 0.5f;
            yield return null;
        }
        damageBox.transform.position = startPos.position;
    }

}
