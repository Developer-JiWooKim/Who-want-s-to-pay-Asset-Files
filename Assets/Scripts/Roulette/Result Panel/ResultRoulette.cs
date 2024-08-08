using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultRoulette : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI resultText;
    [SerializeField]
    private RawImage resultImage;

    [SerializeField]
    private Roulette roulette;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GetResult();
    }

    private void GetResult()
    {
        resultText.text = roulette.resultData.description_name;
        resultImage.texture = roulette.resultData.userImage;
    }
    public void OnClickReplayButton()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_COMPLETE_BUTTON_PRESS);
            SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.ROULETTE_BOARD);
        }
    }
    public void OnClickSettingButton()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_ADD_IMAGE_BUTTON_PRESS);
            SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.ROULETTE_SETUP);
        }
    }
}
