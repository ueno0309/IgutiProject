using UnityEngine;

public class GoalCreator : MonoBehaviour
{
    [SerializeField] private GameObject goalPrefab; // 作るゴール（プレハブ）
    [SerializeField] private string requiredItemName = "ExitDoor"; // 必要アイテム名

    private bool goalCreated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (goalCreated) return;

        // アイテムピクトが触れた時だけ発動
        if (collision.CompareTag("ItemPictogram"))
        {
            var pickup = collision.GetComponent<PictogramPickup>();
            if (pickup != null && pickup.PictogramName == requiredItemName)
            {
                Debug.Log("非常口ピクトを使用！ → 新しいゴールを生成しました。");
                Instantiate(goalPrefab, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                goalCreated = true;
            }
        }
    }
}
