using UnityEngine;

public class MaterialMoving : MonoBehaviour
{
    public float speed = 0.1f; // テクスチャの移動速度

    private Renderer rend;

    void Start()
    {
        // レンダラーを取得
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // 現在のテクスチャオフセットを取得
        Vector2 offset = rend.material.mainTextureOffset;

        // Z方向にオフセットを移動
        offset.y += speed * Time.deltaTime;

        // 新しいオフセットを設定
        rend.material.mainTextureOffset = offset;
    }
}
