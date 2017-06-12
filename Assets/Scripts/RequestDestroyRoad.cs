using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestDestroyRoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // 오브젝트 제거 요청
            RoadManager.Instance.DestroyRoad(transform.parent.gameObject);
            Debug.Log("길 제거");
            Destroy(transform.parent.gameObject); 
        }
    }
}
