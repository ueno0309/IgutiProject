using UnityEngine;
using UnityEngine.SceneManagement; // シーン遷移用

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // 次のシーン名をInspectorで指定

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"何かに触れた: {collision.name}");

        if (collision.CompareTag("pictogram"))
        {
            Debug.Log("Pictogramタグのオブジェクトに触れた！ → シーン移動");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("タグが一致しません");
        }
    }

}
