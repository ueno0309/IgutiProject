using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "NextStage"; // 次のシーン名

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pictogram"))
        {
            Debug.Log("ゴールに到達！");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
