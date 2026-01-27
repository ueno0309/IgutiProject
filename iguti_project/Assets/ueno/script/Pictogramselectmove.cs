using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ステージ上に配置されたピクトをドラッグで移動可能にする
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Pictogramselectmove : MonoBehaviour, IDragHandler
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = mainCam.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        transform.position = pos;
    }
}
