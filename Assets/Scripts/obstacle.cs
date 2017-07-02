using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	// Use this for initialization
	public int damage;
	public float reduceSpeed;

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
								
								RoadManager.Instance.Clashed(reduceSpeed);
								Destroy(gameObject);
                Debug.Log("충돌");
								
            }
        }
    }
}
