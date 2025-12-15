using UnityEngine;

public class CrosswalkTrap : MonoBehaviour
{
    public TrafficLightController trafficLight; // 信号機への参照
    public GameObject carObject; // 車のオブジェクト

    private void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが入ってきたら
        if (other.CompareTag("Player"))
        {
            // 信号が赤ならアウト
            if (trafficLight.isRed)
            {
                // 車を表示
                if (carObject != null) carObject.SetActive(true);

                // プレイヤーのやられ処理を呼ぶ
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.Crash();
                }
            }
            else
            {
                Debug.Log("青信号なのでセーフ");
            }
        }
    }
}