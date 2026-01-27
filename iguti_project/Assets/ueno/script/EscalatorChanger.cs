using UnityEngine;

public class EscalatorChanger : MonoBehaviour
{
    [Header("変化後の見た目")]
    [SerializeField] private Sprite changedSprite;

    [Header("反応するピクト名")]
    [SerializeField] private string requiredPictogramName = "Escarater";

    private SpriteRenderer sr;
    private bool isChanged = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChanged) return;

        if (collision.CompareTag("ItemPictogram"))
        {
            var pictogram = collision.GetComponent<PictogramPickup>();
            if (pictogram != null && pictogram.PictogramName == requiredPictogramName)
            {
                Debug.Log($"[EscalatorChanger] 階段に {pictogram.PictogramName} が設置されました！");
                ChangeToEscalator();
                Destroy(collision.gameObject);
            }
        }
    }

    private void ChangeToEscalator()
    {
        isChanged = true;

        // スプライトをエスカレーターに変更
        if (sr != null && changedSprite != null)
            sr.sprite = changedSprite;

        // 段差（Square）削除
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Square"))
                Destroy(child.gameObject);
        }

        // エスカレーター移動機能を有効化
        var move = GetComponent<EscalatorMove>();
        if (move != null)
        {
            move.isActive = true; // ← ここがポイント
            Debug.Log("[EscalatorChanger] EscalatorMoveを有効化しました");
        }
    }
}
