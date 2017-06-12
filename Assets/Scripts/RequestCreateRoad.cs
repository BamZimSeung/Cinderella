using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCreateRoad : MonoBehaviour {

    bool isRequest = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (!isRequest)
            {
                isRequest = true;
                RoadManager.Instance.CreateRoad(transform.parent.gameObject);
            }
        }
    }
}
