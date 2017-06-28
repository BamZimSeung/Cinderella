using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    // 아이템이 가지고 있는 점수
    public int score;

    // 먹혔는지 확인
    bool isGet;

    // 스코어 보드 위치
    public Vector3 scoreBoardPos = new Vector3(-2.5f, 5, 0);

    // 스코어 보드에 접근완료를 판단하는 거리
    public float limitDist = 0.2f;

    // 스코어 보드에 접근하는 스피드
    [Range(0,1.0f)]
    public float approachSpeed = 0.1f;

	// Use this for initialization
	void Start () {
        isGet = false;
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
                GameManager.Instance.AddScore(score);

                // 먹힘
                isGet = true;

                Debug.Log("먹음");

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
