using UnityEngine;
[System.Serializable]
public class RoulettePieceData
{
    public Texture userImage;         // 룰렛 참여자 이미지, 갤러리에서 불러오기 가능
    public string description_name;     // 룰렛 참여자 이름 TODO#: description 지우기

    // 3개의 아이템 등장 확률이 100 60 40이면 등장확률의 합은 200. 100 / 200 = 50%, 60 / 200 = 30%, 40 / 200 = 20%
    [Range(1, 100)]
    public int chance = 100;            // 총 등장 확률

    // [HideInInspector]
    public int index;                   // 순번
    [HideInInspector]
    public int weight;                  // 가중치
}