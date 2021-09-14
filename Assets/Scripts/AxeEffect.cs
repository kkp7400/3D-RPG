using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AxeEffect : MonoBehaviour
{
    public delegate void StartDamageBox();
    StartDamageBox startDamageBox;
    public EffectInfo[] Effects;
    public GameObject damageBoxSlash;
    public GameObject damageBoxChop;
    public GameObject damageSylStrike;
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
    //0 Strike
    //1 Chop    
    //3 Slash
    IEnumerator DamageBox(int skillNum)
    {
        float afterTime = 3f;
        if (skillNum == 0)
        {

            Transform startPos = damageBoxSlash.transform;
            damageBoxSlash.transform.position = this.transform.position;
            damageBoxSlash.transform.rotation = this.transform.rotation;
            while (afterTime >= 0)
            {
                afterTime -= Time.deltaTime;
                damageBoxSlash.transform.position += damageBoxSlash.transform.forward * 1f;
                yield return null;
            }

            damageBoxSlash.transform.position = startPos.position;
        }
        else if (skillNum == 1)
        {

            Transform startPos = damageBoxChop.transform;
            damageBoxChop.transform.position = this.transform.position;
            damageBoxChop.transform.rotation = this.transform.rotation;
            while (afterTime >= 0)
            {
                afterTime -= Time.deltaTime;
                damageBoxChop.transform.position += damageBoxChop.transform.forward * 1f;
                yield return null;
            }

            damageBoxChop.transform.position = startPos.position;
        }
        else if(skillNum == 3)
        {
            Transform startPos2 = damageSylStrike.transform;
            damageSylStrike.transform.position = this.transform.position;
            damageSylStrike.transform.rotation = this.transform.rotation;
            while (damageSylStrike.transform.localScale.x <= 17f)
            {
                damageSylStrike.transform.localScale += new Vector3(0.6f, 0, 0.6f);
                yield return null;
            }

            damageSylStrike.transform.position = new Vector3(1000,1000,1000);
            damageSylStrike.transform.localScale = new Vector3(2,5,2);
        }
    }

}
