using UnityEngine;

public class RotateOnClick : MonoBehaviour
{
    private float rotationAngle = 22.5f; // 回転角度（360/8）

    private void Update()
    {
        // タッチ入力を検出（モバイル）
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            CheckTouch(Input.touches[0].position);
        }

        // マウス入力を検出（エディタやPC）
        if (Input.GetMouseButtonDown(0))
        {
            CheckTouch(Input.mousePosition);
        }
    }

    private void CheckTouch(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // オブジェクトがタッチされた場合
            if (hit.transform == transform)
            {
                RotateObject();
            }
        }
    }

    private void RotateObject()
    {
        transform.Rotate(0, rotationAngle, 0, Space.World);
    }
}
