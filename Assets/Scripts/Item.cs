using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    // 아이템이 가지고 있는 점수
    public int score;

    public int itemIndex;
    int magnetDis=10;
    int itemSpeed=5;
    float hpFill;

    // 먹혔는지 확인
    bool isGet;

    // 스코어 보드 위치
    public Vector3 scoreBoardPos = new Vector3(-2.5f, 5, 0);

    // 스코어 보드에 접근완료를 판단하는 거리
    public float limitDist = 0.2f;

    public float recoveryTime;
    // 스코어 보드에 접근하는 스피드
    [Range(0,1.0f)]
    public float approachSpeed = 0.1f;
    float currentTime;
    bool isUsed=false;

    // 사운드 오디오 클립
    public AudioClip effectSound;

	// Use this for initialization
	void Start () {
        isGet = false;
	}
    void Update()
    {
        if(!isUsed){
            currentTime+=Time.deltaTime;
        }
        if(currentTime>2&&!isUsed){
            isUsed=true;
            GameObject[] Obs=GameObject.FindGameObjectsWithTag("Obs");
            int lth =Obs.Length;
            for(int i=0;i<lth;i++){
                if(gameObject.transform.position.x==Obs[i].transform.position.x){
                   if(Vector3.Distance(gameObject.transform.position,Obs[i].transform.position)<4.698f*GameManager.Instance.currentGameSpeed){
                       if(Obs[i].name.Contains("Maid")){
                           gameObject.transform.position+=Vector3.up;
                       }else{
                           GameManager.Instance.protectComboIndex.Add(itemIndex);
                           print("아이템파괴"+itemIndex);
                           Destroy(gameObject);
                       }
                   } 
                }
            }
        }
        if(GameManager.Instance.isFeverMode){
            GameObject player =GameObject.Find("Player");
            if(Vector3.Distance(gameObject.transform.position,player.transform.position)<magnetDis){
                Vector3 vDir=player.transform.position-gameObject.transform.position;
                gameObject.transform.Translate(vDir*Time.deltaTime*itemSpeed,Space.World);
            }
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        // 아직 먹히지 않았다면
        if (!isGet)
        {
            // 플레이어인지 확인
            if (other.gameObject.CompareTag("Player"))
            {
                // 점수를 추가
                GameManager.Instance.AddScore(score,recoveryTime,itemIndex,hpFill);
                
                // 소리 재생
                SoundManager.Instance.PlayOnSound(effectSound);

                // 먹힘
                isGet = true;

                Debug.Log("먹음"+itemIndex);

                // 이동 코루틴 시작
                StartCoroutine("MoveToScoreBoard");
            }
        }
    }

    IEnumerator MoveToScoreBoard()
    {
        // 부모 해제
        transform.parent = null;

        while(Vector3.Distance(transform.position, scoreBoardPos) > limitDist)
        {
            // 위치 이동과 크기 조절
            transform.position = Vector3.Lerp(transform.position, scoreBoardPos, approachSpeed);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.1f);
            yield return null;
        }

        Debug.Log("도착");

        // 오브젝트 파괴
        Destroy(gameObject);
    }
}
