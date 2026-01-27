using UnityEngine;
using UnityEngine.EventSystems;

public class PictogramPickup : MonoBehaviour, IPointerClickHandler
{
    [Header("ピクト設定")]
    [SerializeField] private string pictogramName = "Escarater";
    [SerializeField] private GameObject pictogramPrefab;

    // ← これを追加（EscalatorChangerから参照できるようにする）
    public string PictogramName => pictogramName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ItemSlotManager.Instance != null)
        {
            ItemSlotManager.Instance.AddItem(pictogramName, pictogramPrefab);
            Debug.Log($"{pictogramName} をスロットに登録");
        }
        else
        {
            Debug.LogWarning("ItemSlotManagerがシーン上に存在しません。");
        }

        gameObject.SetActive(false); // 拾ったので消す
    }
}
