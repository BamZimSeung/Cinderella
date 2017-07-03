using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    public bool isPlaying=true;

    public float currentGameSpeed=1.0f;

    public float gameSpeed=1.0f;



    // 점수 변수
    public int score = 0;

    // 점수 텍스트UI
    public Text scoreTxt;

    // 콤보 텍스트 UI
    public Text comboTxt;

    // 콤보 이미지 UI
    public Image comboImg;

    // 콤보 UI 애니매이터
    public Animator comboAnim;

    public float restTime;
    public float maxTime;

    int pastIndex= -2;

    int combo=0;

    int feverCount=0;

    public int maxFeverCount=100;

    int feverStep = 0;

    public bool isFeverMode=false;

    public int feverDecRate;
    public float feverSpeed;

    // 콤보 UI 색깔
    public byte3 imgColor;
    public byte3 txtColor;

    public byte colorUIAlpha;

    public List<int> protectComboIndex;

    //현재 난이도
    public difficulty currentDifficulty;
    public int difficultyStep =10000;
    
    public difficulty[] levels=new difficulty[4];
    
    public int level=0;

    public Slider HPbar;
    public Image feverBar;
    public bool isEasyMode=false;
    float currentTime=0;

    bool isCrazyModePlayed=false;
    
    
	void Start () {
		if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        //레벨별 난이도 설정
        if(isEasyMode){
            gameSpeed=1.0f;
            feverDecRate=10;
            maxFeverCount=10;
            difficultyStep=1000;
            maxTime=200;
        }

        currentGameSpeed=gameSpeed;
        score = 0;
        scoreTxt.text = score.ToString();
        restTime=maxTime;

        // UI 색깔 초기화
        colorUIAlpha = 0;
        imgColor = new byte3((byte)Random.Range(50, 200), (byte)Random.Range(50, 200), (byte)Random.Range(50, 200));
        txtColor = new byte3((byte)Random.Range(150, 256), (byte)Random.Range(150, 256), (byte)Random.Range(150, 256));

        comboImg.color = new Color32(imgColor.r, imgColor.g, imgColor.b, colorUIAlpha);
        comboTxt.color = new Color32(txtColor.r, txtColor.g, txtColor.b, colorUIAlpha);
        
        levels[0]=new difficulty(1.0f,false,false);
        levels[1]=new difficulty(2f,false,false);
        levels[2]=new difficulty(3f,true,false);
        levels[3]=new difficulty(4f,true,true);
        currentDifficulty=levels[level];
        
        
    }

    void Update()
    {
        restTime-=Time.deltaTime;
        if(restTime<=0){
            isPlaying=false;
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("End");
        }
        HPbar.value=restTime/maxTime;
        feverBar.fillAmount=(float)feverCount/(float)maxFeverCount;
        if(score>=(level+1)*difficultyStep&&level<2){
            level++;
        }
        currentDifficulty=levels[level];
        if(!isCrazyModePlayed&&level==2&&score>(level+1)*difficultyStep){
            level=3;
            isCrazyModePlayed=true;
        }
        if(currentDifficulty.isCrazyMode){
            currentGameSpeed=gameSpeed*1.5f;

            currentTime +=Time.deltaTime;
            if(currentTime>=10){
                currentTime=0;
                level=2;
                currentGameSpeed=gameSpeed;
            }
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
               feverStep=0;
           }
        }else{
            combo=1;
            feverStep=0;
        }
        pastIndex=indexNow;
        float comboWeight=1.0f;
        if(combo>=100){
            comboWeight=3.5f;
        }else if(combo>=80){
            comboWeight=3.0f;
        }else if(combo>=60){
            comboWeight=2.5f;
        }else if(combo>40){
            comboWeight=2.0f;
        }else if(combo>20){
            comboWeight=1.5f;
        }
        score += (int)(scorePoint*comboWeight);

        restTime+=recoveryTime/gameSpeed;
        if(restTime>=maxTime){
            restTime=maxTime;
        }

        scoreTxt.text = score.ToString();
        comboTxt.text = "Combo " + combo;

        StopCoroutine("ShowCombo");
        StartCoroutine("ShowCombo");
        if(!isFeverMode){
            fever(combo);
        }
    }	

    public void fever(int cmb){
        if(cmb<=100+feverStep){
            feverCount+=(cmb-feverStep)/10 +1;
        }else{
            feverCount+=10;
        }
        print(feverCount);
        if(feverCount>maxFeverCount){
            StartCoroutine("feverMode");
        }
    }

    IEnumerator feverMode(){
        isFeverMode=!isFeverMode;
        float FC=100;
        currentGameSpeed=feverSpeed;
        while(FC>=0){
            FC-=Time.deltaTime*feverDecRate;
            yield return null;
        }
        print("피버 끝");
        isFeverMode=!isFeverMode;
        feverStep=combo;
        feverCount =0;
        currentGameSpeed=gameSpeed;
    }

    public void Clash(int damage){
        if(isFeverMode){
            score += damage*10;

        }else{
            restTime-=damage;
            combo=0;
        }
    }

    IEnumerator ShowCombo()
    {
        colorUIAlpha = 255;

        imgColor = new byte3((byte)Random.Range(50, 200), (byte)Random.Range(50, 200), (byte)Random.Range(50, 200));
        txtColor = new byte3((byte)Random.Range(150, 256), (byte)Random.Range(150, 256), (byte)Random.Range(150, 256));

        Color32 tmpColor = new Color32();

        comboAnim.SetTrigger("ComboChange");

        while (colorUIAlpha > 0)
        {
            tmpColor.r = imgColor.r;
            tmpColor.g = imgColor.g;
            tmpColor.b = imgColor.b;
            tmpColor.a = colorUIAlpha;

            comboImg.color = tmpColor;

            tmpColor.r = txtColor.r;
            tmpColor.g = txtColor.g;
            tmpColor.b = txtColor.b;

            comboTxt.color = tmpColor;

            yield return null;
            colorUIAlpha -= 5;
        }

        tmpColor.a = 0;
        comboImg.color = tmpColor;
        comboTxt.color = tmpColor;
    }

    public class byte3
    {
        public byte r;
        public byte g;
        public byte b;

        public byte3(byte _r, byte _g, byte _b)
        {
            this.r = _r;
            this.g = _g;
            this.b = _b;
        }
    }
    
    //난이도 조절 필요 변수
    //1.현재 난이도(변수의 인덱스)
    //2.장애물 생성 빈도
    //3.장애물 동시등장 여부
    public class difficulty{
        public float obsSpwanSpeed;
        public bool isMultiObs;
        public bool isCrazyMode;
        public difficulty(float _obsSpwanSpeed, bool _isMultiObs,bool _isCrazyMode){
            this.obsSpwanSpeed=_obsSpwanSpeed;
            this.isMultiObs=_isMultiObs;
            this.isCrazyMode=_isCrazyMode;
        }
    }
}
