using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("スロット設定")]
    [SerializeField] private Image slotImage;

    private GameObject storedObj; // 保管しているピクトの実体
    private Sprite storedSprite;  // UI表示用スプライト
    private Camera mainCam;       // メインカメラ
    private bool isDragging;      // ドラッグ中フラグ

    public bool HasItem => storedObj != null;

    void Awake()
    {
        if (slotImage == null)
            slotImage = GetComponent<Image>();
        mainCam = Camera.main;

        slotImage.color = Color.white;
        slotImage.sprite = null;
    }

    /// <summary>
    /// スロットに実体を登録（拾ったときに呼ばれる）
    /// </summary>
    public void SetExistingItem(GameObject obj, string pictogramName)
    {
        storedObj = obj;

        var sr = storedObj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            storedSprite = sr.sprite;
            slotImage.sprite = storedSprite;
            slotImage.color = Color.white;
        }

        storedObj.SetActive(false); // 保管中は非表示
        Debug.Log($"{pictogramName} をスロットに格納しました");
    }

    // ======== ドラッグ開始 ========
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!HasItem)
        {
            Debug.Log("スロットが空です。ドラッグできません。");
            return;
        }

        isDragging = true;
        storedObj.SetActive(true); // ステージ上で再表示
        Debug.Log($"{storedObj.name} を取り出し中...");
    }

    // ======== ドラッグ中 ========
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging || storedObj == null) return;

        // マウス位置に追従
        Vector3 pos = mainCam.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        storedObj.transform.position = pos;
    }

    // ======== ドラッグ終了 ========
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragging) return;
        isDragging = false;

        if (storedObj != null)
        {
            storedObj.GetComponent<PictogramBase>()?.OnPlaced();
            Debug.Log($"{storedObj.name} をステージに設置しました。");
        }

        // スロットは空にする（白背景のまま）
        slotImage.sprite = null;
        slotImage.color = Color.white;
        storedObj = null;
    }
}
