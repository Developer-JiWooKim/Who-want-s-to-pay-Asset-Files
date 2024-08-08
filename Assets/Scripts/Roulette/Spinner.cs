using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    [SerializeField]
    private Roulette roulette;
    [SerializeField]
    private Button buttonSpin;

    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private GameObject roulettePanel;

    private void Awake()
    {
        buttonSpin.onClick.AddListener(() =>
        {
            SpinStartSFX_Play();

            // 룰렛이 회전하는 동안은 버튼 다시 누를 수 없게 설정
            buttonSpin.interactable = false;

            // 회전 시작, 회전 끝나면 다시 버튼 누를 수 있게 바꾸는 Action함수 전달
            roulette.Spin(EndOfSpin);
        });
    }
    private void SpinStartSFX_Play()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_SPIN_START_BUTTON_PRESS);
        }
    }
    private void ResultBGM_Play()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.ROULETTE_RESULT);
        }
    }

    private void EndOfSpin(RoulettePieceData selectedData)
    {
        ResultBGM_Play();

        buttonSpin.interactable = true;
      
        // 스핀 종료 후 룰렛 패널 비활성화
        roulettePanel.SetActive(false);
        // 결과 창 활성화
        resultPanel.SetActive(true);
    }


}
