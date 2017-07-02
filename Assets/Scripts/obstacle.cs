using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {
    int obsSpeed=4;
	// Use this for initialization
	public int damage;
	public float reduceSpeed;

    public int pos;

    // 먹혔는지 확인
    bool isClash;


	// Use this for initialization
	void Start () {
        isClash = false;
	}

    void OnTriggerEnter(Collider other)
    {
        // 아직 먹히지 않았다면
        if (!isClash)
        {
            // 플레이어인지 확인
            if (other.gameObject.CompareTag("Player"))
            {
                // 점수를 추가
                GameManager.Instance.Clash(damage);

                // 먹힘
                isClash = true;
				if(!GameManager.Instance.isFeverMode){
                    RoadManager.Instance.Clashed(reduceSpeed);
                    Destroy(gameObject);
                }else{
                    Vector3 vDir=Vector3.zero;
                    if(pos==0){
                        vDir=new Vector3(-0.25f,0.5f,4);
                    }else if(pos==1){
                        vDir=new Vector3(0,0.5f,4);
                    }else{
                        vDir=new Vector3(0.25f,0.5f,4);
                    }
                    
                    //여기서 요 디스트로이 지우고 이펙트 넣으면 됨
                    StartCoroutine(bangle(vDir));
                }			
							
            }
        }

    }
    IEnumerator bangle(Vector3 vDir){
        float currentTime=0;
        while(currentTime<3){
            currentTime+=Time.deltaTime;
            gameObject.transform.Translate((vDir+Vector3.up)*Time.deltaTime*obsSpeed*GameManager.Instance.gameSpeed,Space.World);
            gameObject.transform.Rotate(0,0,10);
            yield return null;
        }
        Destroy(gameObject);
    }
}
