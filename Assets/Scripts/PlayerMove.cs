using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public Transform[] movePositions;

    public int curPos = 1;

    public float moveHorizontalSpeed;

    Animator anim;

    public float rotationLimit;

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
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Jump");
        }

        // 애니메이션 후 캐릭터가 회전하길래 임시적으로 막음
        if (!(Mathf.Abs(transform.eulerAngles.y) < rotationLimit || Mathf.Abs(transform.eulerAngles.y) > 360 - rotationLimit))
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            Debug.Log("회전 재조정");
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
