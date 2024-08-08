public class LoadingWindowRoulette : LoadingWindowBase
{
    private SoundManager.E_BGM_Name rouletteBGM = SoundManager.E_BGM_Name.ROULETTE_SETUP;
    public override void ResetPercent()
    {
        percentText.text = "0%";
        percentSlider.value = 0;
    }
    public override void EndLoadingAndStartBGM()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_BGM(rouletteBGM);
        }
    }
}
