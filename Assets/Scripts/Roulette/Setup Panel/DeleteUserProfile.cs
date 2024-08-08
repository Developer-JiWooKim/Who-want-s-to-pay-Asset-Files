using UnityEngine;

public class DeleteUserProfile : MonoBehaviour
{
    private GameObject userProfile;

    private void Awake()
    {
        Setup();
    }
    private void Setup()
    {
        userProfile = gameObject.transform.parent.gameObject;
    }
    private void DeleteSFX_Play()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_DELETE_BUTTON_PRESS);
        }
        
    }
    public void Delete()
    {
        if (userProfile == null)
        {
            Debug.Log("null");

            return;
        }

        // 삭제 효과음 재생
        DeleteSFX_Play();

        // 룰렛 매니저의 데이터 리스트에서 같은 인덱스를 가진 데이터 삭제
        RouletteManager.Instance.RemoveUserData(userProfile.gameObject.GetComponent<InfoData>().index);
        RouletteManager.Instance.UpdateRouletteIndex(userProfile.gameObject.GetComponent<InfoData>().index);

        // 이 프리팹 삭제
        Destroy(userProfile);
    }
}
