using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern
    /// </summary>
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }
    public enum E_SFX_Name
    {
        ROBBY_BUTTON_PRESS,
        ROULETTE_BACK_BUTTON_PRESS,
        ROULETTE_ADD_BUTTON_PRESS,
        ROULETTE_ADD_IMAGE_BUTTON_PRESS,
        ROULETTE_DELETE_BUTTON_PRESS,
        ROULETTE_COMPLETE_BUTTON_PRESS,
        ROULETTE_FAILED_COMPLETE,
        ROULETTE_SPIN_START_BUTTON_PRESS,
        ROULETTE_SPINNING,
        ROULETTE_SETTING_BUTTON_PRESS,
    }

    public enum E_BGM_Name
    {
        ROBBY,
        LOADING_ROULETTE,
        LOADING_SLOTMACHINE,
        ROULETTE_SETUP,
        ROULETTE_BOARD,
        ROULETTE_RESULT,
        SLOTMACHINE,
    }

    [SerializeField]
    private AudioClip[]     bgm                     = null;
    [SerializeField]
    private AudioClip[]     sfx                     = null;

    [SerializeField]
    private AudioSource     bgm_Player              = null;
    [SerializeField]
    private AudioSource[]   sfx_Player              = null;

    private const float     __DEFAULT_VOLUME_VALUE  = 0.7f;

    private void Awake()
    {
        SingletonSetup();
    }
    private void Start()
    {
        Setup();
    }
    private void SingletonSetup()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// !!__주의__!!
    /* 추후 유지보수 필요 내용
     * 이 프로젝트에서는 효과음이 많지 않아서 사운드 매니저에서 게임 로딩 시에 모든 효과음을 로드해 가지고 있음
     * 하지만 프로젝트가 커져서 많은 양의 효과음이 생겨버리면 관리도 힘들고 로딩시에 많은 시간이 소요됨
     * 그러므로 나중에는 씬마다 효과음을 가지고 있을 스크립트를 만들고 사운드 매니저가 씬 전환시에 해당 스크립트를 로드해서 사용할 수 있게 바꿔야됨
     * 그러므로 이 Setup을 public으로 바꾸고 씬 전환할 때마다 해당 씬에서 스크립트를 찾아서 로드하는 코드를 Setup에 작성하면 됨
     */
    private void Setup()
    {
        // 사운드 플레이어 설정, 타이틀 BGM재생
        {
            bgm_Player.playOnAwake = true;
            bgm_Player.loop = true;
            Play_BGM(E_BGM_Name.ROBBY);
            SetDefaultVolume();
        }
    }
    /// <summary>
    /// 볼륨 크기를 __DEFAULT_VOLUME_VALUE(.5f)로 설정하는 함수
    /// </summary>
    private void SetDefaultVolume()
    {
        SetVolume_BGM(__DEFAULT_VOLUME_VALUE);
        SetVolume_SFX(1);
    }
    public void Play_BGM(E_BGM_Name bgm_Name)
    {
        bgm_Player.clip = bgm[(int)bgm_Name];
        bgm_Player.Play();
    }
    public void SetVolume_BGM(float _volume)
    {
        bgm_Player.volume = _volume;
    }
    public void SetVolume_SFX(float _volume)
    {
        for (int i = 0; i < sfx_Player.Length; i++)
        {
            sfx_Player[i].volume = _volume;
        }
    }
    public void Stop_BGM()
    {
        bgm_Player.Stop();
    }
    public void Play_SFX(E_SFX_Name sfx_Name)
    {
        for (int j = 0; j < sfx_Player.Length; j++)
        {
            // SFX 플레이어 중 재생 중이지 않은 AudioSource를 발견하면
            if (!sfx_Player[j].isPlaying)
            {
                sfx_Player[j].clip = sfx[(int)sfx_Name];
                sfx_Player[j].Play();
                return;
            }
        }
        Debug.Log("All SFX Player is Playing!!");
    }
}
