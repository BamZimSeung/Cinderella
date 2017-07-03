using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour {

    public Text scoreTxt;
    public AudioClip failSound;

	void Start () {
        scoreTxt.text = "Score : " + PlayerPrefs.GetInt("Score").ToString();
        Invoke("MakeSomeNoise", 0.5f);
        Invoke("GoToStartScene", 4f);
	}
	
    void GoToStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    void MakeSomeNoise()
    {
        SoundManager.Instance.PlayOnSound(failSound);
    }
}
