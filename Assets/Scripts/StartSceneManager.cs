using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {

    public GameObject UIPanel;

    public Animator StartSceneAnim;

    public Animator ActorsAnim;

    public Animator PowerPrincessAnim;

    public Animator PrinceAnim;

    public Animator KnightAnim;

    public AudioClip DangerSound;
    public AudioClip HitSound;

	// Use this for initialization
	void Start () {
        StartSceneAnim = GetComponent<Animator>();
	}
	
    public void OnClickStartButton()
    {
        UIPanel.SetActive(false);

        SoundManager.Instance.PlayOnSound(DangerSound);

        // 카메라 애니메이션 트리거
        StartSceneAnim.SetTrigger("Go");
    }

    public void firstCameraMove()
    {
        ActorsAnim.SetTrigger("Do Nothing");
        PrinceAnim.SetTrigger("Idle");
    }

    public void secondCameraMove()
    {
        PowerPrincessAnim.SetTrigger("Surprise");
    }

    public void thirdCameraMove()
    {
        PowerPrincessAnim.SetTrigger("Kick");
    }

    public void KnightDown()
    {
        KnightAnim.SetTrigger("Down");
        SoundManager.Instance.PlayOnSound(HitSound);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene("Test");
    }
}
