using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern
    /// </summary>
    private static GameManager instance = null;
    public static GameManager Instance
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

    [SerializeField]
    private List<LoadingWindowBase> loadingWindows = new List<LoadingWindowBase>();

    public enum SceneName
    {
        Robby,
        Roulette,
        Slotmachine,
    }
    public SceneName sceneName = SceneName.Robby;

    private void Awake()
    {
        SingletonSetup();
    }
    private void Start()
    {
        Setup();
    }
    /// <summary>
    /// 싱글톤 처리하는 함수
    /// </summary>
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
    private void Setup()
    {
        Application.targetFrameRate = 60;
        loadingWindows = transform.gameObject.GetComponentsInChildren<LoadingWindowBase>().ToList();
    }
    
    /// <summary>
    /// 비동기 Scene 전환 코루틴 함수(비동기 Scene 전환 시 로딩화면도 표기해주는 코루틴 함수)
    /// </summary>
    private IEnumerator AsyncLoadScene()
    {
        // 비동기 씬 전환 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncLoad.allowSceneActivation = false;

        // 이동하려는 씬과 같은 이름을 가진 LoadingWindowBase을 가진 오브젝트를 찾음
        var loadingwindow = loadingWindows.Find((loading) => loading.sceneName == sceneName);
        // 해당 오브젝트를 활성화
        loadingwindow.loadingProgress.SetActive(true);
        // 해당 오브젝트를 초기화
        loadingwindow.ResetPercent();

        int progressPercentage = 0;
        float time = 0;
        float progress;

        loadingwindow.percentText.text = "0%";

        // 현재 로딩 진행도를 표기
        while (!asyncLoad.isDone)
        {
            progress = asyncLoad.progress;
            progressPercentage = Mathf.RoundToInt(progress * 100f);
            loadingwindow.percentText.text = progressPercentage.ToString() + "%";

            loadingwindow.percentSlider.value = progress;

            time += Time.deltaTime;
            // 최소 2초가 될때까지 기다림
            if (time > 2f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            // 2초가 흐르면 로딩 진행도 텍스트를 100%로, 전환된 씬의 배경음을 재생
            if(asyncLoad.allowSceneActivation == true)
            {
                loadingwindow.percentText.text = "100%";
                loadingwindow.EndLoadingAndStartBGM();
            }
            yield return null;
        }
        // 로딩이 끝났으면 로딩화면을 비활성화
        loadingwindow.loadingProgress.SetActive(false);
        yield return null;
    }
    private IEnumerator ChangeSceneAction()
    {
        // 씬 전환 전에 할 일들 있으면 여기다
        
        // 페이드 아웃 완료 후 비동기 로드 시작
        yield return StartCoroutine(AsyncLoadScene());
    }
    public void ChangeScene(SceneName newScene)
    {
        if (sceneName != newScene)
        {
            sceneName = newScene;
        }
        // 만약 로비로 이동하는거면 로딩씬 불러오지 않고 바로 이동 -> 로드해야할 데이터가 거의 없기 때문 필요시 해당 if문 지우고 로딩화면을 따로 만들면 됨
        if (newScene == SceneName.Robby)
        {
            SceneManager.LoadScene(sceneName.ToString());
            SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.ROBBY);
            return;
        }

        StartCoroutine(ChangeSceneAction());
    }
    
    public void AwakeAction(Action action = null)
    {
        if (action != null)
        {
            action();
        }
        else
        {
            Debug.Log("GameManager.AwakeAction() : action is null!");
        }
    }
}
