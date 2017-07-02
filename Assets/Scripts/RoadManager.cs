using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

    public GameObject[] roadPrefabs;
    public GameObject startRoad;
    public List<GameObject> roads = new List<GameObject>();

    public float roadMoveSpeed;

    public static RoadManager Instance = null;
    public float addSpeedRatePerTime;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            roads.Add(startRoad);
        }
    }

	void Update () {
        // 리스트 내의 모든 로드를 움직인다.
        if(GameManager.Instance.isPlaying){
            int roadCount = roads.Count;
            for(int i = 0; i < roadCount; i++){
                roads[i].transform.Translate(Vector3.back * roadMoveSpeed * Time.deltaTime*GameManager.Instance.currentGameSpeed);
            }
        }
        
	}

    // 로드 생성을 요청한 로드의 "SpawnPos"의 위치에 새 로드를 생성
    public void CreateRoad(GameObject requestRoad)
    {
        // 랜덤 프리팹 생성
        Debug.Log("길 생성");
        int ranNum = Random.Range(0, roadPrefabs.Length);
        GameObject newRoad = Instantiate(roadPrefabs[ranNum], requestRoad.transform.FindChild("SpawnPos").transform.position, Quaternion.identity);
        roads.Add(newRoad);
    }

    // 로드를 제거
    public void DestroyRoad(GameObject target)
    {
        int roadCount = roads.Count;
        roads.Remove(target);
        Debug.Log("리스트에서 길 제거");
    }

    public void Clashed(float reduceSpeed){
        StartCoroutine(ClashedCoroutine(reduceSpeed));
    }
    public IEnumerator ClashedCoroutine(float reduceSpeed)
    {
        GameManager.Instance.currentGameSpeed=reduceSpeed;
        while(GameManager.Instance.currentGameSpeed<=1){
           GameManager.Instance.currentGameSpeed+=Time.deltaTime*addSpeedRatePerTime;
            yield return null;
        }
        GameManager.Instance.currentGameSpeed=GameManager.Instance.gameSpeed;
        
    }
}
