using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour {

	void Start () {
        Invoke("MoveScene", 5f);
	}

    void MoveScene()
    {
        SceneManager.LoadScene("Start");
    }
}
