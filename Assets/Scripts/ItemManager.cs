﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    //아이템 프리펩 배열
    public GameObject[] itemsPrefabs;

    //보석생성여부
    bool isGem = false;
    //아이템 생성 시간차
    public float ItemGenerateGap;

    int gemCount;

    public int maxGemCount;
    public int minGemCount;

    int indexNow;



    //아이템 생성을 위한 시간 누적 변수;
    float itemTime = 10;
    //현재 포지션
    int currentPos;

    //장애물 프리펩
    public GameObject[] obsPrefabs;
    int currObsIndex;
    //장애물 생성 시간차
    public float obsGenerateGap;
    //장애물 생성을 위한 시간 누적 변수;
    float obsTime;
    //장애물 위치
    int[] obsPos = new int[2];
    // Use this for initialization
    void Start()
    {
        currentPos = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        itemTime += Time.deltaTime;
        obsTime += Time.deltaTime;
        if (itemTime * GameManager.Instance.currentGameSpeed >= ItemGenerateGap)
        {
            itemGenerate();
            itemTime = 0;
        }
        if (obsTime * GameManager.Instance.currentGameSpeed * GameManager.Instance.currentDifficulty.obsSpwanSpeed >= obsGenerateGap)
        {
            Vector3 posNow = transform.position;
            obsPos[0] = Random.Range(0, 3);
            obsPos[1] = Random.Range(0, 3);
            if (GameManager.Instance.currentDifficulty.isCrazyMode)
            {
                int underObsPos = Random.Range(0, 3);
                for (int i = 0; i < 3; i++)
                {
                    if (i == underObsPos)
                    {
                        obsCreateCrazy(posNow, i, 0);
                    }
                    else
                    {
                        obsCreateCrazy(posNow, i, 1);
                    }
                }

            }
            else
            {
                if (GameManager.Instance.currentDifficulty.isMultiObs && obsPos[0] != obsPos[1])
                {
                    obsCreate(posNow, obsPos[1]);
                }
                obsCreate(posNow, obsPos[0]);
            }
            obsTime = 0;
        }
    }

    void itemGenerate()
    {
        GameObject item = null;
        //3이 아닐경우 코인 생성, 3일경우 보석 생성
        if (gemCount == 0)
        {
            isGem = true;
            gemCount = Random.Range(minGemCount, maxGemCount + 1);
        }
        if (!isGem)
        {
            item = Instantiate(itemsPrefabs[0]);
            gemCount--;

        }
        else
        {
            item = Instantiate(itemsPrefabs[Random.Range(1, itemsPrefabs.Length)]);

            isGem = !isGem;
        }
        item.transform.gameObject.GetComponent<Item>().itemIndex = indexNow++;
        //currentPos에 따라 위치 지정,다음 currentPos지정
        if (currentPos == 0)
        {
            currentPos = Random.Range(0, 2);
            item.transform.position = transform.position + Vector3.left * 2;
        }
        else if (currentPos == 1)
        {
            currentPos = Random.Range(0, 3);
            item.transform.position = transform.position;
        }
        else
        {
            currentPos = Random.Range(1, 3);
            item.transform.position = transform.position + Vector3.right * 2;
        }
        // if(currObs!=null&& 
        // (currObs.transform.position.z-item.transform.position.z)*(currObs.transform.position.z-item.transform.position.z)<6.25f
        // &&(currObs.transform.position.x-item.transform.position.x)*(currObs.transform.position.x-item.transform.position.x)<1){
        // 	if(currObsIndex==0){
        // 		item.transform.position+=Vector3.up;
        // 	}else{
        // 		Destroy(item);
        // 		indexNow--;
        // 		print("아이템파괴");
        // 	}
        // }
        item.transform.parent = RoadManager.Instance.roads[RoadManager.Instance.roads.Count - 1].transform;

    }

    void obsCreate(Vector3 posNow, int _obsPos)
    {

        currObsIndex = Random.Range(0, obsPrefabs.Length);
        GameObject obs = Instantiate(obsPrefabs[currObsIndex]);
        obs.GetComponent<obstacle>().pos = _obsPos;
        obs.transform.position = posNow;
        if (_obsPos == 0)
        {
            obs.transform.position += Vector3.left * 2;
        }
        else if (_obsPos == 2)
        {
            obs.transform.position += Vector3.right * 2;
        }
        obs.transform.position += Vector3.down;
        obs.transform.parent = RoadManager.Instance.roads[RoadManager.Instance.roads.Count - 1].transform;
    }
    void obsCreateCrazy(Vector3 posNow, int _obsPos, int currObsIndex)
    {
        GameObject obs = Instantiate(obsPrefabs[currObsIndex]);
        obs.GetComponent<obstacle>().pos = _obsPos;
        obs.transform.position = posNow;
        if (_obsPos == 0)
        {
            obs.transform.position += Vector3.left * 2;
        }
        else if (_obsPos == 2)
        {
            obs.transform.position += Vector3.right * 2;
        }
        obs.transform.position += Vector3.down;
        obs.transform.parent = RoadManager.Instance.roads[RoadManager.Instance.roads.Count - 1].transform;
    }
}
