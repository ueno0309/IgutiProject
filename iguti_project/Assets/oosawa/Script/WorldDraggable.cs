using UnityEngine;

public class WorldDraggable : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isDragging = false;
    private Camera mainCamera;

    // アイテム欄に入った時の見た目（UI用のスプライトなどがあれば）
    // 今回は単純にステージから消すだけにします

    void Start()
    {
        initialPosition = transform.position;
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // マウスの位置に看板を追従させる
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // アイテム欄（画面下部）にあるか判定
        // 簡易的に、マウスのY座標が画面の下の方なら「回収」とみなす
        Vector3 mouseScreenPos = Input.mousePosition;

        // 画面の高さの下20%のエリアをアイテム欄エリアと仮定
        if (mouseScreenPos.y < Screen.height * 0.2f)
        {
            Debug.Log("看板を回収しました");

            // 看板をステージから消す（非表示にする）
            gameObject.SetActive(false);

            // ★発展：ここでUIのアイテム欄にアイコンを表示する処理を入れても良いです
        }
        else
        {
            // アイテム欄以外なら元の位置に戻る
            transform.position = initialPosition;
        }
    }
}