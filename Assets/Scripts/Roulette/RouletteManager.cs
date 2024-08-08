using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouletteManager : MonoBehaviour
{
    /// <summary>
    /// Singleton Pattern
    /// </summary>
    private static RouletteManager instance = null;
    public static RouletteManager Instance
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

    [field:SerializeField]
    public List<RoulettePieceData> roulettePieceDatas { get; private set; }
    public int count { get; private set; }

    [SerializeField]
    private Sprite defaultUserImage;

    [SerializeField]
    private GameObject contents;

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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Setup()
    {
        // Data Manager에 저장된 이전 세팅값을 가져옴
        count = 0; // TODO#: DataManager에서 세팅된 값에서 등록된 유저 수만큼 count 설정
        roulettePieceDatas = new List<RoulettePieceData>();
    }
    // 룰렛에 들어갈 유저의 데이터를 추가, UI로 보여지는게 아닌 데이터로 존재함
    // Add 버튼 클릭 시 호출되는 함수, 룰렛에 들어갈 데이터를 생성 후 리턴
    public RoulettePieceData AddUserData()
    {
        // 룰렛 피스 데이터 생성
        RoulettePieceData data = new RoulettePieceData();

        // 디폴트 이미지 추가
        //data.icon = defaultUserImage;
        data.userImage = defaultUserImage.texture;

        // 인덱스 부여
        data.index = count;

        // 디폴트 이름 부여
        data.description_name = "User " + (count + 1); 

        // 룰렛 피스 데이터들을 관리하는 리스트에 해당 데이터를 추가
        roulettePieceDatas.Add(data);

        // 인덱스 1 증가
        count++;

        return data;
    }

    // 매개변수로 받은 인덱스와 같은 데이터 삭제
    public void RemoveUserData(int index)
    {
        // 매개변수로 받은 index와 같은 index를 가진 데이터 삭제 
        roulettePieceDatas.Remove(roulettePieceDatas.Find(data => data.index == index));
    }

    public void UpdateRouletteIndex(int deleteIndex)
    {
        // 데이터를 삭제 후 프리팹들의 데이터를 초기화 해야함
        InfoData[] userDatas = GetInfoDatas(deleteIndex);

        // 데이터 갯수 초기화
        count = 0;

        // 모든 데이터의 인덱스 값 재설정
        roulettePieceDatas.ForEach(data =>
        {
            data.index = count;
            count++;
        });
        
        // 각 데이터들의 인덱스를 재설정 후 UI를 업데이트
        for (int i = 0; i < userDatas.Length; i++)
        {
            userDatas[i].UpdateIndex(roulettePieceDatas[i].index);
            userDatas[i].UpdateUserName_UI();
        }
    }

    // 삭제할 유저 데이터를 제외한 유저 데이터 배열을 얻어옴
    public InfoData[] GetInfoDatas(int deleteIndex)
    {
        // contents에 붙어있는 InfoData들을 가져와 배열로 설정
        var datas = contents.GetComponentsInChildren<InfoData>();

        // InfoData중 삭제될 인덱스와 같은 데이터를 제외하고 배열 다시 생성
        var newDatas = datas.Where(data => data.index != deleteIndex).ToArray();
        
        return newDatas;
    }

    public RoulettePieceData[] GetInfoDatas()
    {
        var infoDatas = contents.GetComponentsInChildren<InfoData>();

        // 룰렛 데이터들을 설정한 Setup패널에서 설정한 값으로 변경
        roulettePieceDatas.ForEach(data =>
        {
            data.userImage = infoDatas[data.index].userImage.texture;
            data.description_name = infoDatas[data.index].userName;
        });

        return roulettePieceDatas.ToArray();
    }
}
