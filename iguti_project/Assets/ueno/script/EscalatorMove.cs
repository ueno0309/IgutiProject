using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EscalatorMove : MonoBehaviour
{
    [Header("エスカレーターの速度")]
    public float moveSpeed = 1.5f;

    [Header("上方向の移動量（単位ベクトル）")]
    public Vector2 moveDirection = new Vector2(0.5f, 1f);

    [HideInInspector]
    public bool isActive = false; // ← これがfalseの間は動かない！

    private void Start()
    {
        // トリガー化を保証
        var col = GetComponent<BoxCollider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isActive) return; // ← エスカレーター状態でない時は無視

        if (collision.CompareTag("pictogram")) // プレイヤーのTagを想定
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 move = moveDirection.normalized * moveSpeed * Time.deltaTime;
                rb.MovePosition(rb.position + move);
            }
        }
    }
}
