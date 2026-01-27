using UnityEngine;

/// <summary>
/// 再生・停止・リセットを制御する「動くピクト」用ベースクラス
/// </summary>
public class PictogramPlaybackBase : MonoBehaviour
{
    protected bool isPlaying = false;   // 再生中フラグ
    private Vector3 startPos;           // 初期位置記録用

    private void Start()
    {
        startPos = transform.position;
    }

    public virtual void OnStartPlayback()
    {
        isPlaying = true;
        Debug.Log($"{name}：再生開始");
    }

    public virtual void OnStopPlayback()
    {
        isPlaying = false;
        Debug.Log($"{name}：再生停止");
    }

    public virtual void OnReset()
    {
        isPlaying = false;
        transform.position = startPos;
        Debug.Log($"{name}：位置リセット");
    }

    // 外部参照用のプロパティ
    public bool IsPlaying => isPlaying;
}
