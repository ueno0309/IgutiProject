using UnityEngine;

public class RockTrap : MonoBehaviour
{
    [Header("判定する看板のスクリプト")]
    public SignDrag signScript; // 看板についているスクリプトを指定

    [Header("岩の設定")]
    public GameObject rockObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 看板が「収納されていない（isStored == false）」なら危険！
            if (signScript != null && !signScript.isStored)
            {
                // 岩を表示して落とす
                if (rockObject != null)
                {
                    rockObject.SetActive(true);
                    Rigidbody2D rb = rockObject.GetComponent<Rigidbody2D>();
                    if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
                }

                // プレイヤーをやられ状態にする
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Crash();
                }
            }
            else
            {
                Debug.Log("看板は片付けられているので安全です");
            }
        }
    }
}