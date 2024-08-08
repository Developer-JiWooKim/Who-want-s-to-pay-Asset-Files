using UnityEngine;

public class FadeDialogueWindow : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayFadeAnimation()
    {
        gameObject.SetActive(true);
        anim.Play("FadeAnim");
    }
    public void EndFadeAnimation()
    {
        gameObject.SetActive(false);
    }
}
