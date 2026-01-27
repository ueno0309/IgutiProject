using UnityEngine;

public class SignDrag : MonoBehaviour
{
    [Header("移動先の設定")]
    public Transform targetSlot; // ステップ1で作った SignTarget を入れる

    // 外部から「今、看板は片付けられているか？」を確認するためのフラグ
    public bool isStored = false;

    private Vector3 initialPosition;
    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        initialPosition = transform.position;
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        // クリックしたらドラッグ開始
        isDragging = true;
        isStored = false; // 動かし始めたら「片付いていない」状態に戻す
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // マウスに合わせて移動
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // ターゲット（スロット）との距離を測る
        float distance = Vector2.Distance(transform.position, targetSlot.position);

        // もし距離が近ければ（1.5以下なら）
        if (distance < 1.5f)
        {
            // スロットの位置にピタッと吸着させる
            // targetSlotの位置を取得
            Vector3 newPos = targetSlot.position;

            // z軸の値を-1する
            newPos.z -= 1.0f;

            // 変更した位置をセット
            transform.position = newPos;
            isStored = true; // 「片付け完了」にする
            Debug.Log("看板を収納しました");
        }
        else
        {
            // 遠ければ元の位置に戻る
            transform.position = initialPosition;
            isStored = false;
        }
    }
}