using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWindowBase : MonoBehaviour
{
    public GameManager.SceneName    sceneName;
    public TextMeshProUGUI          percentText;
    public GameObject               loadingProgress;
    public Slider                   percentSlider;
    public virtual void ResetPercent() { }
    public virtual void EndLoadingAndStartBGM() { }
}
