using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理機能を使うために必要

public class GoalScript : MonoBehaviour
{
    [Header("遷移先シーン名")]
    public string resultSceneName = "ResultScene"; // ステップ4で保存したシーン名

    // 他の Collider 2D が Trigger (自分) に侵入した瞬間に呼ばれる
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 侵入してきたオブジェクトのタグが "Player" かどうかを判定
        if (other.CompareTag("Player"))
        {
            // デバッグログをコンソールに出力
            Debug.Log("プレイヤーがゴールに到達しました！");

            // リザルトシーンへ遷移
            SceneManager.LoadScene(resultSceneName);
        }
    }

    // (補足) Is Triggerを有効にしたColliderの範囲をSceneビューで可視化する
    private void OnDrawGizmos()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f); // 緑色の半透明
            Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
        }
    }
}