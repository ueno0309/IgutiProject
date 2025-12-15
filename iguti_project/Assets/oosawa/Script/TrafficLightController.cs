using UnityEngine;
using System.Collections;

public class TrafficLightController : MonoBehaviour
{
    [Header("設定")]
    public float interval = 3.0f; // 切り替わる秒数
    public Sprite redSprite;
    public Sprite greenSprite;

    // 現在の状態（外部から確認できるようにpublicにする）
    public bool isRed = true;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(TrafficLoop());
    }

    IEnumerator TrafficLoop()
    {
        while (true)
        {
            // 赤信号にする
            isRed = true;
            spriteRenderer.sprite = redSprite;
            yield return new WaitForSeconds(interval);

            // 青信号にする
            isRed = false;
            spriteRenderer.sprite = greenSprite;
            yield return new WaitForSeconds(interval);
        }
    }
}