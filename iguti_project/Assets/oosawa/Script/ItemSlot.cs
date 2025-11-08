using UnityEngine;
using UnityEngine.EventSystems; // イベントシステムを使うために必要

public class ItemSlot : MonoBehaviour, IDropHandler // IDropHandlerインターフェースを実装
{
    // アイテムがこのスロットの上でドロップ（マウスボタンが離）された時に呼ばれる
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called");

        // ドロップされたオブジェクト（ドラッグされてきたオブジェクト）を取得
        GameObject droppedObject = eventData.pointerDrag;
        DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            // アイテムをこのスロットの子要素にする
            draggableItem.transform.SetParent(this.transform);

            // アイテムの位置をスロットの中央に合わせる
            draggableItem.transform.localPosition = Vector3.zero;

            // アイテムに「今いるスロット」を（ドロップ成功として）更新させる
            draggableItem.currentSlot = this.transform;
        }
    }
}