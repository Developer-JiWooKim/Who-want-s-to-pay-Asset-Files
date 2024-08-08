using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoulettePiece : MonoBehaviour
{
    [SerializeField] 
    private RawImage userImage;

    [SerializeField]
    private TextMeshProUGUI textDescription_name; // TODO#: description Áö¿ì±â

    private TMP_InputField inputfield;

    [field:SerializeField]
    public int index { get; set; }

    public void Setup(RoulettePieceData pieceData)
    {
        userImage.texture           = pieceData.userImage;

        textDescription_name.text   = pieceData.description_name;
    }
    public void Setup(RoulettePieceData pieceData, int _index)
    {
        inputfield          = GetComponentInChildren<TMP_InputField>();

        userImage.texture   = pieceData.userImage;

        inputfield.text     = pieceData.description_name;

        index               = _index;
    }
}
