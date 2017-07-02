using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // 효과음 볼륨
    public float _effectSoundVol = 1f;

    // 배경음 오디오 소스 컴포넌트 참조
    public AudioSource _bgmAudioSource;

    public static SoundManager Instance = null;

    void Awake()
    {
        _bgmAudioSource = GetComponent<AudioSource>();
      
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // 즉흥 사운드를 재생함
    public void PlayOnSound(AudioClip clip)
    {
        // 즉흥적으로 사운드를 재생함(스스로 소멸함)
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, _effectSoundVol);
    }
}
