using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;

    public AudioSource BGMSource;
    public AudioClip[] sceneBGM;

    public AudioSource SESource;
    public AudioClip[] effectClip;

    public enum SoundEffect
    {
        ATTACK = 0,
        DESTROY = 1,
        WIN = 2,
        LOSE = 3
    }
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (SoundManager.instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    public void PlayBGM(int num)
    {
        BGMSource.clip = sceneBGM[num];
        BGMSource.Stop();
        BGMSource.Play();
    }
    public void PlaySE(SoundEffect e)
    {
        int num = (int)e;
        SESource.clip = effectClip[num];
        SESource.Play();
    }
}
