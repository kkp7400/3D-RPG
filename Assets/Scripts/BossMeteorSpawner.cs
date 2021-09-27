using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMeteorSpawner : MonoBehaviour
{
    public GameObject rangeObject;
    public float SpawnPosX;
    public float SpawnPosY;
    public float SpawnPosZ;
   // public ObjectPool objPool;
    public float coolTime;
    public GameObject indicator;
    ParticleSystem effect;
    public GameObject hitBox;
    public bool isBossHpHalf;
    // Start is called before the first frame update
    void Start()
    {
        isBossHpHalf = false;
        coolTime = Random.Range(5,10);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        indicator = transform.Find("CircleIndicator").gameObject;
        effect = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossHpHalf)
        {
            if (coolTime >= 0f) coolTime -= Time.deltaTime;
            if (coolTime <= 0f)
            {
                StartCoroutine(Fire());
                coolTime = Random.Range(5, 10);
            }
        }
    }

    IEnumerator Fire()
    {
        indicator.SetActive(true);
        float SpawnPosX =
                   UnityEngine.Random.Range(
                       rangeObject.transform.position.x - rangeObject.transform.localScale.x / 2,
                       rangeObject.transform.position.x + rangeObject.transform.localScale.x / 2);
        float SpawnPosY = rangeObject.transform.position.y;
        float SpawnPosZ =
        UnityEngine.Random.Range(
                rangeObject.transform.position.z - rangeObject.transform.localScale.z / 2,
                rangeObject.transform.position.z + rangeObject.transform.localScale.z / 2);
        transform.position = new Vector3(SpawnPosX, SpawnPosY, SpawnPosZ);

        Vector3 showRagePos = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 200f, 1 << LayerMask.NameToLayer("Ground")))
        {
            showRagePos = hit.point;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log(hit.transform.name);
        }
        float showRangeTime = 3.5f;
        while(showRangeTime>=0)
        {           
            indicator.transform.position = showRagePos;
            showRangeTime -= Time.deltaTime;
            yield return null;
        }
        effect.Play();
        indicator.SetActive(false);
        float rateTime = 0.8f;
        while (rateTime >= 0)
        {
            rateTime -= Time.deltaTime;
            yield return null;
        }

        hitBox.transform.position = showRagePos;
        float hitBoxTime = 0.25f;
        hitBox.SetActive(true);
        while (hitBoxTime >= 0)
        {
            hitBoxTime -= Time.deltaTime;
            yield return null;
        }
        hitBox.SetActive(false);
    }
}
