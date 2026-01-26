// ItemSlot.cs
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler // IDropHandlerインターフェースを実装
{
    [Header("ギミック連携 (ターゲットスロットUIのみ)")]
    // ★連携する物理トリガーをインスペクタで設定
    public Slot linkedTrigger;

    // ゲーム開始時にアイテムが最初から置いてあるかチェック
    void Start()
    {
        // 自分の子要素（アイテム）があるか確認
        DraggableItem item = GetComponentInChildren<DraggableItem>();
        if (item != null)
        {
            // アイテムがあれば、そのアイテム情報を初期設定する
            item.currentSlot = this.transform;

            // 連携トリガーがあれば有効化する
            if (linkedTrigger != null)
            {
                linkedTrigger.Activate(item.itemType);
            }
        }
    }

    // アイテムがこのスロットの上でドロップ（マウスボタンが離）された時に呼ばれる
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called on: " + gameObject.name);

        GameObject droppedObject = eventData.pointerDrag;
        DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            // アイテムをこのスロットの子要素にする
            draggableItem.transform.SetParent(this.transform);

            // アイテムの位置をスロットの中央に合わせる (UIなので localPosition)
            draggableItem.transform.localPosition = Vector3.zero;

            // アイテムに「今いるスロット」を（ドロップ成功として）更新させる
            draggableItem.currentSlot = this.transform;

            // --- ★ここから変更★ ---
            // もし、このスロットが連携するトリガー (linkedTrigger) を持っていたら
            if (linkedTrigger != null)
            {
                // トリガーに「アイテムが置かれた」ことを通知
                linkedTrigger.Activate(draggableItem.itemType);
            }
        }
    }
}