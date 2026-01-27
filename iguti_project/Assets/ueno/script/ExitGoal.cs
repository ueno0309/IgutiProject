using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGoal : MonoBehaviour
{
    [SerializeField] string targetTag = "Player";
    [SerializeField] string resultSceneName = "Result";

    PictogramBase pictBase;
    bool triggered = false;

    void Awake()
    {
        pictBase = GetComponentInParent<PictogramBase>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Exitが設置されていない間は無効
        if (pictBase == null || !pictBase.isPlaced) return;
        if (triggered) return;

        if (other.CompareTag(targetTag))
        {
            triggered = true;
            Debug.Log("ゴール範囲に侵入 → リザルトへ");
            SceneManager.LoadScene(resultSceneName);
        }
    }
}
