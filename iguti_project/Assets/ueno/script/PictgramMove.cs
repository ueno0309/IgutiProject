using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PictogramMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private PictogramPlaybackBase playback; // ← ここで再生状態を参照

    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playback = GetComponent<PictogramPlaybackBase>(); // ← 追加
    }

    void FixedUpdate()
    {
        if (playback != null && playback.IsPlaying)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
