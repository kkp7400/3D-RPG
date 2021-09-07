using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AxeEffect : MonoBehaviour
{
    public delegate void StartDamageBox();
    StartDamageBox startDamageBox;
    public EffectInfo[] Effects;
    public GameObject damageBox;
    public GameObject damageSyl;
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
        if (skillNum == 0 || skillNum == 1)
        {

            Transform startPos = damageBox.transform;
            damageBox.transform.position = this.transform.position;
            damageBox.transform.rotation = this.transform.rotation;
            while (afterTime >= 0)
            {
                afterTime -= Time.deltaTime;
                damageBox.transform.position += damageBox.transform.forward * 1f;
                yield return null;
            }

            damageBox.transform.position = startPos.position;
        }
        else if(skillNum == 3)
        {
            Transform startPos2 = damageSyl.transform;
            damageSyl.transform.position = this.transform.position;
            damageSyl.transform.rotation = this.transform.rotation;
            while (damageSyl.transform.localScale.x <= 14f)
            {
                damageSyl.transform.localScale += new Vector3(0.6f, 0, 0.6f);
                yield return null;
            }

            damageSyl.transform.position = new Vector3(1000,1000,1000);
            damageSyl.transform.localScale = new Vector3(2,5,2);
        }
    }

}
