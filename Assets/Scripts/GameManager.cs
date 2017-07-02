using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    // 점수 변수
    public int score = 0;

    // 점수 텍스트UI
    public Text scoreTxt;

	void Start () {
		if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        score = 0;
        scoreTxt.text = score.ToString();

    }

    public void AddScore(int scorePoint)
    {
        score += scorePoint;
        scoreTxt.text = score.ToString();
    }	
}
