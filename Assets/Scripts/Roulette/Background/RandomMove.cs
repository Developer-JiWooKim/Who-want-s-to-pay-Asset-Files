using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomMove : MonoBehaviour
{
    [SerializeField]    
    private float moveSpeed;

    private float offset_x;
    private float offset_y;

    private Material background;

    private void Awake()
    {
        Setup();
        StartCoroutine(AutoMove());
    }
    private void Setup()
    {
        background = GetComponent<Image>().material;
    }
    private IEnumerator AutoMove()
    {
        while (true)
        {
            offset_x += (moveSpeed * Time.deltaTime);
            offset_y += (moveSpeed * Time.deltaTime);
            Vector2 offset = new Vector2(offset_x, offset_y);
            background.SetTextureOffset("_MainTex", offset);

            yield return null;
        }
    }
}
