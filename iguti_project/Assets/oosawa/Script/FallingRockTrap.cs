using UnityEngine;

public class FallingRockTrap : MonoBehaviour
{
    public GameObject signObject; // 看板オブジェクト
    public GameObject rockObject; // 岩オブジェクト

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 看板がアクティブ（＝回収されていない）なら岩を落とす
            if (signObject != null && signObject.activeSelf)
            {
                if (rockObject != null)
                {
                    rockObject.SetActive(true); // 岩を表示

                    // 岩を落とす（物理演算をONにする）
                    Rigidbody2D rb = rockObject.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }

                    // プレイヤーのクラッシュ処理を呼ぶ
                    // （岩の衝突判定で呼ぶのが正確ですが、今回は演出として即座に呼びます）
                    PlayerController player = other.GetComponent<PlayerController>();
                    if (player != null)
                    {
                        player.Crash();
                    }
                }
            }
            else
            {
                Debug.Log("看板が片付けられているので安全です");
            }
        }
    }
}