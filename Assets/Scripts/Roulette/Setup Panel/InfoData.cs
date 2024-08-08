using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoData : MonoBehaviour
{
    public RawImage userImage;

    public string userName;
    public int index;

    [SerializeField]
    private TMP_InputField inputField;

    public bool isChanged = false;

    private void Awake()
    {
        inputField = GetComponentInChildren<TMP_InputField>();
    }
    public void Setup(Texture userImage, string userName, int index)
    {
        this.userImage.texture      = userImage;
        this.userName               = userName;
        this.index                  = index;
        inputField.text             = userName;

        Debug.Log(userName + " InfoData index : " + index);
    }
    /// <summary>
    /// Index를 업데이트하는 함수
    /// </summary>
    public void UpdateIndex(int newIndex)
    {
        index = newIndex;
    }
    public void UpdateUserName_UI()
    {
        // 유저가 이름을 채워넣었으면 해당 이름을 변경하지 않음
        if (isChanged)
        {
            return;
        }
        userName = "User " + (index + 1);
        inputField.text = userName;
    }
    public void InputValueEvent()
    {
        userName = inputField.text;
        isChanged = true;
    }
    public void NotChanged()
    {
        isChanged = false;
    }
}
