using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour {

    public Text scoreTxt;

	void Start () {
        scoreTxt.text = "Score : " + PlayerPrefs.GetInt("Score").ToString();
        Invoke("GoToStartScene", 4f);
	}
	
    void GoToStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
