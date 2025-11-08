using UnityEngine;
using UnityEngine.UI; // UIコンポーネント（Buttonなど）を使うために必要

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float moveSpeed = 3.0f; // プレイヤーの移動速度

    [Header("スプライト設定")]
    public Sprite waitSprite; // 待機中のスプライト
    public Sprite runningSprite; // 移動中のスプライト

    [Header("UI設定")]
    public Button startButton; // スタートボタン

    private SpriteRenderer spriteRenderer;
    private bool isMoving = false; // 移動中かどうかを判定するフラグ

    // ゲーム開始時に一度だけ呼ばれる
    void Start()
    {
        // 自身のSpriteRendererコンポーネントを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期状態を設定
        isMoving = false;

        // 初期スプライトを「待機」に設定
        if (spriteRenderer != null && waitSprite != null)
        {
            spriteRenderer.sprite = waitSprite;
        }
    }

    // 毎フレーム呼ばれる
    void Update()
    {
        // もし isMoving が true なら
        if (isMoving)
        {
            // 右（Vector3.right）へ移動し続ける
            // Time.deltaTimeを掛けることで、どのPCでも同じ速度で動くように調整
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    // --- ここからがボタンで呼び出すメソッド ---

    /// <summary>
    /// ゲーム（移動）を開始する
    /// </summary>
    public void StartGame()
    {
        // 1. 移動フラグを立てる
        isMoving = true;

        // 2. スプライトを「走る画像」に差し替え
        if (spriteRenderer != null && runningSprite != null)
        {
            spriteRenderer.sprite = runningSprite;
        }

        // 3. スタートボタンを非表示にする（一度押したら消す場合）
        if (startButton != null)
        {
            startButton.gameObject.SetActive(false);
            // または、押せなくするだけなら: startButton.interactable = false;
        }
    }
}