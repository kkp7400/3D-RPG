using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour
{
    public int nowStage;
    [Serializable]
    public class Wave
    {
        public bool isEnemySpawn = true;
        public GameObject enemyPos;
        public int spawnAmount;
        public bool isClear = false;
    }

    [Serializable]
    public class Stage
    {
        public int nowWave = 0;
        public Wave[] wave;
        public String enemyType;
        public bool isClear = false;
        public StageTrigger stageTrigger;

        public GameObject nextStageWall;

        //public List<bool> waveStart = new List<bool>();

    }
    [SerializeField]
    public Stage[] stage;

    public List<bool> stageStart = new List<bool>();


    public int deathMonsterAmount;
    public ObjectPool objPool;
    // Start is called before the first frame update
    void Start()
    {
        deathMonsterAmount = 0;
           nowStage = 0;
        for (int i = 0; i < stage.Length; i++)
        {
            stage[i].isClear = false;
            stage[i].nowWave = 0;
            for(int j = 0; j < stage[i].wave.Length; j++)
            {
                stage[i].wave[j].isEnemySpawn = true;
                stage[i].wave[j].isClear = false;
            }
        }



        
    }
    // Update is called once per frame
    void Update()
    {
        int currWave = stage[nowStage].nowWave;
        if (stage[nowStage].stageTrigger.isStart && stage[nowStage].wave[currWave].isEnemySpawn == true)
        {
            for (int i = 0; i < stage[nowStage].wave[currWave].spawnAmount; i++)
            {
                float SpawnPosX =
                UnityEngine.Random.Range(
                    stage[nowStage].wave[currWave].enemyPos.transform.position.x - stage[nowStage].wave[currWave].enemyPos.transform.localScale.x / 2,
                    stage[nowStage].wave[currWave].enemyPos.transform.position.x + stage[nowStage].wave[currWave].enemyPos.transform.localScale.x / 2);
                float SpawnPosY = stage[nowStage].wave[currWave].enemyPos.transform.position.y;
                float SpawnPosZ =
                UnityEngine.Random.Range(
                        stage[nowStage].wave[currWave].enemyPos.transform.position.z - stage[nowStage].wave[currWave].enemyPos.transform.localScale.z / 2,
                        stage[nowStage].wave[currWave].enemyPos.transform.position.z + stage[nowStage].wave[currWave].enemyPos.transform.localScale.z / 2);
               
                objPool.SpawnFromPool(stage[nowStage].enemyType,
                    new Vector3(
                        SpawnPosX,
                        SpawnPosY,
                        SpawnPosZ),
                   Quaternion.identity);               
            }
            stage[nowStage].wave[currWave].isEnemySpawn = false;
        }
        if(deathMonsterAmount >= stage[nowStage].wave[currWave].spawnAmount)
        {
            deathMonsterAmount = 0;
               stage[nowStage].nowWave++;
            if(stage[nowStage].nowWave>2)
            {
                stage[nowStage].nextStageWall.SetActive(false);
                nowStage++;
            }
        }




        //                   Quaternion.identity));
        //if (newStageArry < 3)
        //{
        //    if (nowWave > lastWave)
        //    {
        //        stage[newStageArry].wave[nowWave].enemySpawn = true;
        //        hostageSpawner.LinkEnemy(newStageArry, nowWave);

        //    }
        //    // if (stage[newStageArry].waveStart)
        //    {
        //        //   for (int j = 0; j < stage[newStageArry].wave.Length; j++)
        //        {
        //            if (stage[newStageArry].wave[nowWave].enemySpawn == true)
        //            {
        //                enemyList.Add(objPool.SpawnFromPool(stage[newStageArry].wave[nowWave].enemyType, new Vector3(stage[newStageArry].wave[nowWave].enemyPos.position.x, stage                 [newStageArry].wave[nowWave].enemyPos.position.y, stage[newStageArry].wave[nowWave].enemyPos.position.z),
        //                   Quaternion.identity));
        //                stage[newStageArry].wave[nowWave].enemySpawn = false;
        //            }
        //        }
        //        //stage[i].waveStart = false;

        //    }


        //    lastWave = nowWave;

        //    NextStage();
        //}
        //else if (newStageArry >= 3 && DeadEye == true)
        //{
        //    for (int j = 0; j < 10; j++)
        //    {
        //        enemyList.Add(objPool.SpawnFromPool(stage[3].wave[j].enemyType, new Vector3(stage[3].wave[j].enemyPos.position.x, stage[3].wave[j].enemyPos.position.y, stage[3].wave[j].enemyPos.position.z),
        //           Quaternion.identity));
        //    }
        //    DeadEye = false;
        //}
    }

    //void NextStage()
    //{

    //    for (int i = 0; i < enemyList.Count; i++)
    //    {
    //        if (enemyList[i].GetComponent<Enemy>().isDead == false) return;
    //    }
    //    if (nowWave < stage[newStageArry].wave.Length)
    //        nowWave++;
    //    enemyList.Clear();
    //}

    //public void HighNoon()
    //{
    //    for (int i = 0; i < enemyList.Count; i++)
    //    {
    //        enemyList[i].GetComponent<Enemy>().Die();
    //    }
    //}
}