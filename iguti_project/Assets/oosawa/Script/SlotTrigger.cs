// SlotTrigger.cs
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    private ItemType currentItemType = ItemType.None;
    private PlayerController player;
    private BoxCollider2D triggerCollider;

    [Header("落石トラップ用設定")] 
    public GameObject rockObject; // 落ちてくる岩のオブジェクト

    void Start()
    {
        // プレイヤーの参照を自動で取得
        player = FindObjectOfType<PlayerController>();

        // 自身が持つトリガー用のColliderを取得（初期状態では無効化）
        triggerCollider = GetComponent<BoxCollider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }

    // ItemSlot (TargetSlot_UI) から呼ばれる
    public void Activate(ItemType type)
    {
        Debug.Log("トリガーがアクティベートされました: " + type);
        currentItemType = type;
        // アイテムが置かれたら、プレイヤー検知用のトリガーを有効化する
        if (triggerCollider != null)
        {
            triggerCollider.enabled = true;
        }
    }

    // ItemSlot (DraggableItemから) 呼ばれる
    public void Deactivate()
    {
        Debug.Log("トリガーが非アクティブ化されました");
        currentItemType = ItemType.None;
        // アイテムがなくなったら、トリガーを無効化する
        if (triggerCollider != null)
        {
            triggerCollider.enabled = false;
        }
    }

    // プレイヤーがこのエリアに侵入した時
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 侵入したのがプレイヤーで、かつプレイヤーが参照できていれば
        if (player != null && other.gameObject == player.gameObject)
        {
            bool triggerUsed = false;

            // セットされているアイテムタイプに応じてプレイヤーの処理を呼び出す
            switch (currentItemType)
            {
                case ItemType.ElevatorUp:
                    Debug.Log("エレベーター作動！");
                    player.StartElevator();
                    triggerUsed = true;
                    break;

                case ItemType.TrapDown:
                    Debug.Log("落下トラップ作動！");
                    player.StartTrapDown();
                    triggerUsed = true;
                    break;

                case ItemType.FallingRockSign:
                    Debug.Log("落石！");

                    // 岩を表示して落とす
                    if (rockObject != null)
                    {
                        rockObject.SetActive(true);
                        Rigidbody2D rockRb = rockObject.GetComponent<Rigidbody2D>();
                        if (rockRb != null) rockRb.bodyType = RigidbodyType2D.Dynamic;
                    }

                    // プレイヤー即死
                    player.Crash();
                    triggerUsed = true;
                    break;
            }

            if (triggerUsed)
            {
                // ギミックは1回使ったら無効化
                Deactivate();

                // (オプション) 見た目上のスロットからもアイテムを消す
                // FindObjectOfType<ItemSlot>() などで TargetSlot_UI を見つけ、
                // その子要素の DraggableItem を Destroy(item.gameObject) しても良いでしょう。
            }
        }
    }
}