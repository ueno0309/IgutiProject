using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    void Awake()
    {
        Instance = this;
    }

    public void OnGoal()
    {
        Debug.Log("ステージクリア！");
        // リザルト表示・SE・次ステージ
    }
}
