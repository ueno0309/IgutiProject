using UnityEngine;

/// <summary>
/// ピクトグラム共通の基底クラス
/// </summary>
public class PictogramBase : MonoBehaviour
{

    [HideInInspector] public bool isPlaced = false; // 配置済みかどうか
    [HideInInspector] public bool isPicked = false; // 拾われたかどうか

    public virtual void OnPlaced()
    {
        isPlaced = true;
        Debug.Log($"{name} がステージに配置されました");
    }

    public virtual void OnPicked()
    {
        isPicked = true;
        Debug.Log($"{name} を拾いました");
    }
}
