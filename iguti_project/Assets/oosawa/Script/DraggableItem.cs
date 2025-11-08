using UnityEngine;
using UnityEngine.EventSystems; // イベントシステム
using UnityEngine.UI; // Imageなど

[RequireComponent(typeof(CanvasGroup))] // 必要なコンポーネントを自動で追加
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    public Transform currentSlot; // アイテムが現在入っているスロット

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // 最初の親（スロット）を記憶する
        currentSlot = transform.parent;
    }

    // --- ドラッグ開始時 ---
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        // 1. レイキャスト（当たり判定）を無効にする
        //    (これをしないと、OnDropが自分自身（アイテム）で隠れてしまい、
        //     下のスロットで検出できなくなる)
        canvasGroup.blocksRaycasts = false;

        // 2. ドラッグ中はCanvasの最前面に表示するため、一時的に親をCanvas直下にする
        transform.SetParent(transform.root); // transform.root は最も親のCanvasを指す
    }

    // --- ドラッグ中 ---
    public void OnDrag(PointerEventData eventData)
    {
        // マウス（または指）の位置にアイテムを追従させる
        transform.position = eventData.position;
    }

    // --- ドラッグ終了時（ドロップ時） ---
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // 1. レイキャストを元に戻す
        canvasGroup.blocksRaycasts = true;

        // 2. 親がCanvasのまま（＝スロットの上でドロップされなかった）場合
        if (transform.parent == transform.root)
        {
            // 元のスロット（currentSlot）に戻す
            transform.SetParent(currentSlot);
            transform.localPosition = Vector3.zero;
        }

        // OnDropが呼ばれた場合は、ItemSlot側で currentSlot が
        // 新しいスロットに更新され、親も変更されているので、
        // ここでのIF文には入らない。
    }
}