using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {
    Animator anim;

    public float rotationLimit;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
}
