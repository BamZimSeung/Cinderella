using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Transform[] movePositions;

    public int curPos = 1;

    public float moveHorizontalSpeed;

    Animator anim;

    public float rotationLimit;
    public int jumpSpeed;

	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
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
        if (Input.GetButtonDown("Jump")&&gameObject.transform.position.y<0.85)
        {
            anim.SetTrigger("Jump");
            gameObject.GetComponent<Rigidbody>().velocity=Vector3.up*jumpSpeed;
            
        }
    }

    IEnumerator Move(int posNum)
    {
        float x = transform.position.x;

        while (Vector3.Distance(movePositions[posNum].position, transform.position) > 0.1f)
        {
            x = Mathf.Lerp(transform.position.x, movePositions[posNum].position.x, moveHorizontalSpeed);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);

            yield return null;
        }

        transform.position = movePositions[posNum].position;
    }
}
