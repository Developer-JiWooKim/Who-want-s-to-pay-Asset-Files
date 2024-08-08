using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonAction : MonoBehaviour
{
    public GameManager.SceneName goSceneName;
    private void BackButtonPressSFX_Play()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_BACK_BUTTON_PRESS);
        }
    }
    public void ChangeScene()
    {
        if (GameManager.Instance != null)
        {
            BackButtonPressSFX_Play();
            GameManager.Instance.ChangeScene(goSceneName);
        }
    }
}
