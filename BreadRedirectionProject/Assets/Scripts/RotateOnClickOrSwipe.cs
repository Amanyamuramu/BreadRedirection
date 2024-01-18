using UnityEngine;

public class RotateOnClickOrSwipe : MonoBehaviour
{
    private float rotationAngle = 45f; // クリック時の回転角度
    private float swipeRotationAngle = 180f; // スワイプ時の回転角度
    private Vector2 startTouchPosition; // スワイプ開始位置
    private Vector2 endTouchPosition; // スワイプ終了位置
    private bool swipeDetected = false; // スワイプ検出フラグ

    public AudioClip tapSound;
    public AudioClip swipeSound;
    private AudioSource audioSource;

    private void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // タッチ入力を検出（モバイル）
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    if (Vector2.Distance(startTouchPosition, endTouchPosition) > 20) 
                    // スワイプ距離のしきい値
                    {
                        swipeDetected = true;
                        CheckSwipe(endTouchPosition);
                    }
                    break;
            }
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
                RotateObject(rotationAngle);
                audioSource.PlayOneShot(tapSound);
            }
        }
    }

    private void CheckSwipe(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // オブジェクトがスワイプ終了位置でタッチされた場合
            if (hit.transform == transform && swipeDetected)
            {
                RotateObject(swipeRotationAngle);
                audioSource.PlayOneShot(swipeSound);
                swipeDetected = false;
            }
        }
    }

    private void RotateObject(float angle)
    {
        transform.Rotate(0, angle, 0, Space.World);
    }
}
