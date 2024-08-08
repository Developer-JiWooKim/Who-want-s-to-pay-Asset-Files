using UnityEngine;

public class RobbyPanel : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AwakeAction(Setup);
    }
    private void Setup()
    {
        gameObject.SetActive(true);
    }
    public void OnClickRouletteButton()
    {
        SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROBBY_BUTTON_PRESS);
        SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.LOADING_ROULETTE);
        
        GameManager.Instance.ChangeScene(GameManager.SceneName.Roulette);
        gameObject.SetActive(false);
    }
    public void OnClickSlotMachineButton()
    {
        SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROBBY_BUTTON_PRESS);
        SoundManager.Instance.Play_BGM(SoundManager.E_BGM_Name.LOADING_SLOTMACHINE);
        gameObject.SetActive(false);
        GameManager.Instance.ChangeScene(GameManager.SceneName.Slotmachine); // ½½·Ô ¸Ó½Å ¾À Á¦ÀÛ ÈÄ Àû¿ë
    }
    public void OnClickQuitButton()
    {
        SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROBBY_BUTTON_PRESS);
        Application.Quit();
    }

}
