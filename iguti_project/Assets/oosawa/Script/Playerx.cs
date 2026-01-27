using UnityEngine;
using UnityEngine.SceneManagement; // シーンリロード用
using System.Collections; // コルーチン用

public class Playerx : MonoBehaviour
{
    // プレイヤーの状態を定義
    public enum PlayerState
    {
        Idle,       // 待機中 (スタート前)
        Running,    // 走行中
        Elevating,  // 上昇中
        Falling,    // 落下中
        Bouncing,   // 激突中
        Exploding,  // 爆発
        Stopped     // 停止中 (ゴールなど)
    }

    [Header("状態管理")]
    public PlayerState currentState = PlayerState.Idle;

    [Header("移動設定")]
    public float moveSpeed = 3.0f;

    [Header("スプライト設定")]
    public Sprite townSprite;    // 待機中（町の画像）
    public Sprite runningSprite; // 移動中（走る画像）
    public Sprite explosionSprite; // 爆発画像

    [Header("エレベーター設定")]
    public float elevatorSpeed = 2.0f;
    public float elevatorTargetHeightY = 5.0f; // 上昇する目標のY座標

    [Header("落下トラップ設定")]
    public float fallSpeed = 5.0f;
    public int bounceCount = 3; // ガンガンと打ち付ける回数
    public float bounceForce = 2.0f; // 打ち付け時の跳ね返り（Y方向の力）

    // コンポーネント
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    // 初期状態の保存用
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>(); // Rigidbody 2Dを取得

        // ゲーム開始時の初期位置と回転を記憶
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // 初期状態を設定
        ChangeState1(PlayerState.Idle);
    }

    // 状態を変更する（スプライトの切り替えもここで行う）
    void ChangeState1(PlayerState newState)
    {
        currentState = newState;
        Debug.Log("プレイヤー状態変更: " + newState);

        switch (currentState)
        {
            case PlayerState.Idle:
                spriteRenderer.sprite = townSprite;
                rb.bodyType = RigidbodyType2D.Kinematic; // 物理演算停止
                rb.linearVelocity = Vector2.zero;
                break;
            case PlayerState.Running:
                spriteRenderer.sprite = runningSprite;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case PlayerState.Elevating:
                spriteRenderer.sprite = townSprite; // ★上昇中は待機画像
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case PlayerState.Falling:
                spriteRenderer.sprite = runningSprite; // 落下中は走る画像（お好みで変更可）
                rb.bodyType = RigidbodyType2D.Kinematic; // KinematicのままTranslateで落とす
                break;
            case PlayerState.Bouncing:
                // ★激突中は物理演算(Dynamic)を有効にし、重力と衝突を使えるようにする
                rb.bodyType = RigidbodyType2D.Dynamic;
                break;
            case PlayerState.Exploding:
                spriteRenderer.sprite = explosionSprite; // ★爆発画像に切り替え
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
                break;
            case PlayerState.Stopped:
                spriteRenderer.sprite = townSprite;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
        }
    }

    void Update()
    {
        // 状態に応じて毎フレームの処理を分岐
        switch (currentState)
        {
            case PlayerState.Running:
                // 右に移動
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                break;

            case PlayerState.Elevating:
                Debug.Log("上だよ");
                // 上に移動
                transform.Translate(Vector3.up * elevatorSpeed * Time.deltaTime);
                // ★目標の高さに達したら
                if (transform.position.y >= elevatorTargetHeightY)
                {
                    // Y座標を正確に合わせる
                    transform.position = new Vector3(transform.position.x, elevatorTargetHeightY, transform.position.z);
                    // ★走行再開
                    ChangeState1(PlayerState.Running);
                }
                break;

            case PlayerState.Falling:
                // 下に移動
                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
                // ※床との衝突は OnCollisionEnter2D で検知する
                break;
        }
    }

    // --- 外部から呼ばれるメソッド群 ---

    // [スタートボタン用] (前回のStartGameメソッド)
    public void StartGame1()
    {
        if (currentState == PlayerState.Idle)
        {
            ChangeState1(PlayerState.Running);
        }
    }

    // [ItemSlotから呼ばれる] エレベーター開始
    public void StartElevator1()
    {
        if (currentState == PlayerState.Running)
        {
            Debug.Log("上に参ります");
            ChangeState1(PlayerState.Elevating);
        }
    }

    // [ItemSlotから呼ばれる] 落下トラップ開始
    public void StartTrapDown1()
    {
        if (currentState == PlayerState.Running)
        {
            Debug.Log("下に参ります");
            ChangeState1(PlayerState.Falling);
        }
    }

    // --- 衝突判定 ---

    // 物理的な衝突（IsTrigger=OFFのCollider）が起きた時
    // ※床オブジェクトに "Floor" タグを設定してください
    private void OnCollisionEnter2D(Collision2D coll)
    {
        // ★落下中 (Falling) に "Floor" タグのオブジェクトにぶつかったら
        if (currentState == PlayerState.Falling && coll.gameObject.CompareTag("Floor"))
        {
            Debug.Log("床に激突！");
            // 激突処理（コルーチン）を開始
            ChangeState1(PlayerState.Bouncing); // 先にStateをBouncingに変える
            StartCoroutine(BounceSequence1());
        }
    }

    // ★激突＆爆発＆リセット の一連の流れ（コルーチン）
    private IEnumerator BounceSequence1()
    {
        // RigidbodyがDynamicになっているはず

        // ★ガンガンと打ち付ける
        for (int i = 0; i < bounceCount; i++)
        {
            // 上に少し跳ね返る力を加える
            rb.linearVelocity = new Vector2(0, bounceForce);
            Debug.Log((i + 1) + "回目のバウンド");

            // 少し待つ (0.3秒)
            yield return new WaitForSeconds(0.3f);

            // 速度が十分落ちる（着地する）まで待つ
            yield return new WaitUntil(() => rb.linearVelocity.y < 0.1f);
            yield return new WaitForSeconds(0.1f); // 着地後のわずかな待機
        }

        // ★爆発
        Debug.Log("爆発！");
        ChangeState1(PlayerState.Exploding);

        // 2秒間、爆発画像を表示
        yield return new WaitForSeconds(2.0f);

        // ★リプレイ（リセット）
        ResetGame1();
    }

    // ゲームをリセットする
    public void ResetGame1()
    {
        // プレイヤーを初期位置に戻し、状態をIdleにする
        // transform.position = initialPosition;
        // transform.rotation = initialRotation;
        // ChangeState1(PlayerState.Idle);
        // (上記の方法だと、TargetSlotのアイテムがリセットされないなど問題が起きやすい)

        // ★シーン全体をリロードするのが最も簡単で確実
        Debug.Log("リプレイのためシーンをリロードします");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}