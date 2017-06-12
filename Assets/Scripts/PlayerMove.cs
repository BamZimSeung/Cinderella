using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Transform[] movePositions;

    public int curPos = 1;

    public float moveHorizontalSpeed;

	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curPos--;
            if (curPos < 0)
            {
                curPos = 0;
            }
            StopCoroutine("Move");
            StartCoroutine("Move", curPos);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            curPos++;
            if (curPos > 2)
            {
                curPos = 2;
            }
            StopCoroutine("Move");
            StartCoroutine("Move", curPos);
        }
    }

    IEnumerator Move(int posNum)
    {
        while (Vector3.Distance(movePositions[posNum].position, transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, movePositions[posNum].position, moveHorizontalSpeed);

            yield return null;
        }

        transform.position = movePositions[posNum].position;
    }
}
