using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	//아이템 프리펩 배열
	public GameObject[] itemsPrefab;
	public int maxItemCount;

	//아이템 생성 시간차
	public float ItemGenerateGap;
	
	//아이템 생성 프레임당 시간
	public float generateFrame;

	//아이템 생성을 위한 시간 누적 변수;
	float itemTime=10;
	//현재 포지션
	int currentPos;

	//장애물 프리펩
	public GameObject obsPrefab;
	//장애물 생성 시간차
	public float obsGenerateGap;
	//장애물 생성을 위한 시간 누적 변수;
	float obsTime;
	//장애물 위치
	int obsPos;
	GameObject currObs;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		itemTime+=Time.deltaTime;
		obsTime+=Time.deltaTime;
		if(itemTime>=ItemGenerateGap){
			currentPos=Random.Range(0,3);
			StartCoroutine(itemGenerate());
			itemTime=0;
		}
		if(obsTime>=obsGenerateGap){
			obsCreate();
			obsTime=0;
		}
	}

	IEnumerator itemGenerate(){
		for(int i=0;i<maxItemCount;i++){
			GameObject item=null;
			//3이 아닐경우 코인 생성, 3일경우 보석 생성
			if(i!=3){
				item = Instantiate(itemsPrefab[0]);
			}
			else if(i==3){
				item = Instantiate(itemsPrefab[1]);
			}
			//currentPos에 따라 위치 지정,다음 currentPos지정
			if(currentPos==0){
				currentPos=Random.Range(0,2);
				item.transform.position=transform.position+Vector3.left*2;
			}else if(currentPos==1){
				currentPos=Random.Range(0,3);
				item.transform.position=transform.position;
			}else{
				currentPos=Random.Range(1,3);
				item.transform.position=transform.position+Vector3.right*2;
			}
			if(currObs!=null&& Vector3.Distance(currObs.transform.position,item.transform.position)<2){
				item.transform.position+=Vector3.up;
			}
			item.transform.parent=RoadManager.Instance.roads[RoadManager.Instance.roads.Count-1].transform;
			yield return new WaitForSeconds(generateFrame);
		}
	}
	
	void obsCreate(){
		obsPos=Random.Range(0,3);
		print(obsPos);
		GameObject obs= Instantiate(obsPrefab);
		currObs=obs;
		obs.transform.position=transform.position;
		if(obsPos==0){
				obs.transform.position+=transform.position+Vector3.left*2;
		}else if(obsPos==2){
			obs.transform.position+=Vector3.right*2;
		}
			obs.transform.parent=RoadManager.Instance.roads[RoadManager.Instance.roads.Count-1].transform;
	}
}
