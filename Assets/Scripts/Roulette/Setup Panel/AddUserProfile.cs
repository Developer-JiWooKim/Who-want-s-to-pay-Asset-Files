using UnityEngine;

public class AddUserProfile : MonoBehaviour
{
    public GameObject userProfilePrefab;
    public Transform contentsTransform;
    private void AddButtonPressSFX()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_ADD_BUTTON_PRESS);
        }
    }
    // 게임 매니저에 데이터를 추가, 눈에 보여지는 UI추가
    public void Add()
    {
        // 추가 버튼 효과음 재생
        AddButtonPressSFX();

        // 룰렛 매니저에 새로운 유저 프로필을 추가
        RoulettePieceData userData = RouletteManager.Instance.AddUserData();

        // 프로필 프리팹을 생성
        GameObject prefab = Instantiate(userProfilePrefab);

        // 해당 프로필에 부여될 인덱스
        int index = userData.index;

        // 해당 프리팹에 디폴트 이름부여
        //prefab.GetComponent<RoulettePiece>().Setup(userData, index);
        prefab.GetComponent<InfoData>().Setup(userData.userImage, userData.description_name, index);

        // 스크롤뷰에 붙이는 작업
        prefab.transform.SetParent(contentsTransform, false);
    }
    
}
