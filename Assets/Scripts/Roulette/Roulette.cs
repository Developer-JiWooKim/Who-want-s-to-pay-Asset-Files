using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Roulette : MonoBehaviour
{
    [SerializeField] private Transform              piecePrefab;            // 룰렛에 표시되는 정보 프리팹
    [SerializeField] private Transform              linePrefab;             // 정보들을 구분하는 선 프리팹
    [SerializeField] private Transform              pieceParent;            // 정보들이 배치되는 부모 Transform
    [SerializeField] private Transform              lineParent;             // 선들이 배치되는 부모 Transform
    [SerializeField] private RoulettePieceData[]    roulettePieceData;      // 룰렛에 표시되는 정보 배열

    [SerializeField] private int                    spinDuration;           // 회전 시간
    [SerializeField] private RectTransform          spinningRoulette;       // 실제 회전하는 회전판 Transform
    [SerializeField] private AnimationCurve         spinningCurve;          // 회전 속도 제어를 위한 그래프

    private float   pieceAngle;                     // 정보 하나가 배치되는 각도
    private float   halfPieceAngle;                 // 정보 하나가 배치되는 각도의 절반 크기
    private float   halfPieceAngleWithPaddings;     // 선의 굵기를 고려한 Padding이 포함된 절반 크기

    private int     accumulatedWeight;              // 가중치 계산을 위한 변수
    private bool    isSpinning = false;             // 현재 회전중인지 검사하는 변수
    private int     selectedIndex = 0;              // 룰렛에서 선택된 Piece

    /// <summary>
    /// Result패널에서 얻어갈 결과 데이터들
    /// </summary>
    public RoulettePieceData resultData { get; private set; }
    private void Awake()
    {
        //Setup();
        //SpawnPiecesAndLines();
        //CalculateWeightAndIndices();

        // Debug.Log($"Index : {GetRandomIndex()}");
    }

    private void OnEnable()
    {
        Setup();
        SpawnPiecesAndLines();
    }
    private void OnDisable()
    {
        RemovePieces();
    }
    private void Setup()
    {
        // 결과 데이터 초기화
        if (resultData != null)
        {
            resultData = null;
        }

        // 룰렛 매니저로부터 세팅한 값을 얻어옴
        roulettePieceData = RouletteManager.Instance.GetInfoDatas();

        // 룰렛 조각 하나의 각도
        pieceAngle                  = 360 / roulettePieceData.Length;

        // 룰렛 조각의 절반 각도
        halfPieceAngle              = pieceAngle * .5f;

        // Padding이 포함된 조각의 절반 크기
        halfPieceAngleWithPaddings  = halfPieceAngle - (halfPieceAngle * .25f);
    }
    /// <summary>
    /// 생성된 피스들 제거
    /// </summary>
    private void RemovePieces()
    {
        var pieces = pieceParent.gameObject.GetComponentsInChildren<Transform>();
        var lines = lineParent.gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < pieces.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Destroy(pieces[i].gameObject);
        }
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }
            Destroy(lines[i].gameObject);

        }
    }
    private void SpawnPiecesAndLines()
    {
        Transform piece;
        Transform line;

        // 룰렛 위치 초기화
        spinningRoulette.rotation = Quaternion.Euler(0, 0, 0);

        for (int i = 0; i < roulettePieceData.Length; i++)
        {
            piece   = null;
            line    = null;

            /// !!! Tip !!!
            /// Instantiate()로 오브젝트 생성 시 Transform 타입의 프리팹을 생성 시 반환 값이 Transform 타입으로 반환됨

            // 룰렛 조각 프리팹을 피스 부모 위치에 생성
            piece = Instantiate(piecePrefab, pieceParent.position, Quaternion.identity, pieceParent);

            // 생성한 룰렛 조각의 정보 설정(아이콘, 설명)
            piece.GetComponent<RoulettePiece>().Setup(roulettePieceData[i]);

            // 생성한 룰렛 조각 회전
            piece.RotateAround(pieceParent.position, Vector3.back, (pieceAngle * i));

            // 룰렛 조각을 구분하는 선을 부모 선 위치에 생성
            line = Instantiate(linePrefab, lineParent.position, Quaternion.identity, lineParent);

            // 생성한 선 회전
            line.RotateAround(lineParent.position, Vector3.back, (pieceAngle * i) + halfPieceAngle);
        }
    }
    /// <summary>
    /// 가중치 부여 함수 이 함수를 초기화때 호출하면 가중치 부여가 가능
    /// </summary>
    private void CalculateWeightAndIndices()
    {
        for (int i = 0; i < roulettePieceData.Length; i++)
        {
            roulettePieceData[i].index = i;

            // 예외 처리 : 혹시라도 chance값이 0 이하면 1로 설정 되게
            if (roulettePieceData[i].chance <= 0)
            {
                roulettePieceData[i].chance = 1;
            }
            accumulatedWeight += roulettePieceData[i].chance;
            roulettePieceData[i].weight = accumulatedWeight;

            Debug.Log($"({roulettePieceData[i].index}){roulettePieceData[i].description_name} : {roulettePieceData[i].weight}");
        }
    }
    /// <summary>
    /// 가중치 부여했을 때 호출할 랜덤 선택 함수
    /// </summary>
    private int GetRandomIndex() 
    {
        int weight = UnityEngine.Random.Range(0, accumulatedWeight);
        for (int i = 0; i < roulettePieceData.Length; i++)
        {
            if (roulettePieceData[i].weight > weight)
            {
                return i;
            }
        }
        return 0;
    }
    /// <summary>
    /// 가중치 없이 랜덤으로 유저들 중 한명 선택하는 함수
    /// </summary>
    private int GetRandomIndex(int userCount)
    {
        int weight = UnityEngine.Random.Range(0, userCount);
        return weight;
    }
    private IEnumerator OnSpin(float end, UnityAction<RoulettePieceData> action)
    {
        float current = 0;
        float percent = 0;

        float rotationZ = 0;
        spinningRoulette.rotation = Quaternion.Euler(0, 0, 0);

        while (percent <= 1) // 
        {
            current += Time.deltaTime;
            percent = current / spinDuration;

            // 시작 각도 0부터 end(targetAngle)까지 Z축을 회전
            rotationZ = Mathf.Lerp(0, end, spinningCurve.Evaluate(percent));
            spinningRoulette.rotation = Quaternion.Euler(0, 0, rotationZ);

            yield return null;
        }
        isSpinning = false;

        if (action != null)
        {
            action.Invoke(roulettePieceData[selectedIndex]);
        }
    }
    private void SpinningSFX_Play()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play_SFX(SoundManager.E_SFX_Name.ROULETTE_SPINNING);
        }
    }
    public void Spin(UnityAction<RoulettePieceData> action = null)
    {
        if( isSpinning == true )
        {
            return;
        }

        // 룰렛 회전 효과음 재생
        SpinningSFX_Play();

        // 룰렛의 결과 값 선택
        selectedIndex = GetRandomIndex(roulettePieceData.Length);
        // 결과에 선택된 데이터 저장
        resultData = roulettePieceData[selectedIndex];

        // 선택된 결과의 중심 각도
        float   angle           = pieceAngle * selectedIndex;

        // 정확히 중심이 아닌 결과 값 범위 안의 임의의 각도 선택
        float leftOffset = (angle - halfPieceAngleWithPaddings) % 360;
        float rightOffset = (angle + halfPieceAngleWithPaddings) % 360;

        // 룰렛 조각의 양쪽 끝점 사이에서 멈출 수 있게 범위 설정
        float   randomAngle     = UnityEngine.Random.Range(leftOffset, rightOffset);

        // 목표 각도(targetAngle) = 결과 각도 + 360 * 회전 시간 * 회전 속도
        int     rotateSpeed     = 2;
        float   targetAngle     = randomAngle + (360 * spinDuration * rotateSpeed);
        
        isSpinning = true;
        StartCoroutine(OnSpin(targetAngle, action));
    }
}
