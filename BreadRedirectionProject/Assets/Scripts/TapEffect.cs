using UnityEngine;

public class TapEffect : MonoBehaviour
{
    public ParticleSystem tapEffect; // Inspectorから割り当てる

    void Update()
    {
        // タッチがあるか確認
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // タッチの位置をワールド座標に変換
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));

                // パーティクルシステムをタッチ位置に移動
                tapEffect.transform.position = pos;

                // パーティクルエフェクトを再生
                tapEffect.Play();
            }
        }
    }
}

