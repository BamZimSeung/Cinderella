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

    // 콤보 텍스트 UI
    public Text comboTxt;

    // 콤보 이미지 UI
    public Image comboImg;

    // 콤보 UI 애니매이터
    public Animator comboAnim;

    public float restTime;

    int pastIndex= -2;

    int combo=0;

    // 콤보 UI 색깔
    public byte3 imgColor;
    public byte3 txtColor;

    public byte colorUIAlpha;

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

        // UI 색깔 초기화
        colorUIAlpha = 0;
        imgColor = new byte3((byte)Random.Range(50, 200), (byte)Random.Range(50, 200), (byte)Random.Range(50, 200));
        txtColor = new byte3((byte)Random.Range(150, 256), (byte)Random.Range(150, 256), (byte)Random.Range(150, 256));

        comboImg.color = new Color32(imgColor.r, imgColor.g, imgColor.b, colorUIAlpha);
        comboTxt.color = new Color32(txtColor.r, txtColor.g, txtColor.b, colorUIAlpha);
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
        }else{
            combo=1;
        }
        pastIndex=indexNow;
        score += scorePoint;
        restTime+=recoveryTime;

        scoreTxt.text = score.ToString();
        comboTxt.text = "Combo " + combo;

        StopCoroutine("ShowCombo");
        StartCoroutine("ShowCombo");
    }	

    public void Clash(int damage){
        restTime-=damage;
        combo=0;

        comboTxt.text = "띠용?!";

        StopCoroutine("ShowCombo");
        StartCoroutine("ShowCombo");
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
}
