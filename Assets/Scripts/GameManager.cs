using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public bool isPlaying=true;

    public float gameSpeed=1.0f;

    // 점수 변수
    public int score = 0;

    // 점수 텍스트UI
    public Text scoreTxt;

    public float restTime;

    int pastIndex= -2;

    int combo=0;

    public List<int> protectComboIndex;
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
    void Update()
    {
        restTime-=Time.deltaTime;
        if(restTime<=0){
            isPlaying=false;
        }
    }

    public void AddScore(int scorePoint,float recoveryTime,int indexNow)
    {
        if(indexNow-pastIndex==1){
            combo++;
        }else if(protectComboIndex.Count!=0){
            for(int i=1;i<protectComboIndex.Count;i++){
                if(protectComboIndex[i]-protectComboIndex[i-1]==1){
                    protectComboIndex.RemoveAt(i-1);
                    i--;
                }
            }
           for(int i=1;i<protectComboIndex.Count;i++){
               if(protectComboIndex[i]-indexNow<-1){
                 protectComboIndex.RemoveAt(i);  
                 i--;
               }
           }
           if(protectComboIndex[0]-indexNow==-1){
            combo++;
            protectComboIndex.RemoveAt(0);
           }else{
               combo=1;
           }
        }
        pastIndex=indexNow;
        score += scorePoint;
        restTime+=recoveryTime;

        scoreTxt.text = score.ToString()+"   콤보"+combo;
    }	

    public void Clash(int damage){
        restTime-=damage;
        combo=0;
    }
}
