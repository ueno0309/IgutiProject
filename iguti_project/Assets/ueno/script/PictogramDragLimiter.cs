using UnityEngine;

/// ピクトグラムがステージ範囲外に出ないよう制限

[RequireComponent(typeof(Transform))]
public class PictogramDragLimiter : MonoBehaviour
{
    [SerializeField] private Vector2 minLimit = new Vector2(-8f, -4.5f);
    [SerializeField] private Vector2 maxLimit = new Vector2(8f, 4.5f);

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minLimit.x, maxLimit.x);
        pos.y = Mathf.Clamp(pos.y, minLimit.y, maxLimit.y);
        transform.position = pos;
    }
}
