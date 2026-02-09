using UnityEngine;

public class resultend : MonoBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // エディタ再生停止
#else
Application.Quit(); // ビルド後アプリを終了
#endif
    }
}