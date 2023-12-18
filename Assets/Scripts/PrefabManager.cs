using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Vector3 spawnPoint;
    public float spawnInterval = 1.0f;
    public int numberOfPrefabs = 5; // 一度に生成するPrefabの数
    public float moveSpeed = 5.0f;
    public Vector3 moveDirection = new Vector3(0, 0, -1); // 進行方向は下向き（-Z方向）
    public float minX = -5.0f; // X座標の最小値
    public float maxX = 5.0f; // X座標の最大値

    private void Start()
    {
        InvokeRepeating("SpawnAndMovePrefabs", 0.0f, spawnInterval);
    }

    private void SpawnAndMovePrefabs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // X座標をランダムに設定
            float randomX = Random.Range(minX, maxX);
            Vector3 randomSpawnPoint = new Vector3(randomX, spawnPoint.y, spawnPoint.z);

            // Y軸を中心にランダムな回転を生成
            float randomYRotation = Random.Range(0, 8) * 45.0f; // 0度から315度の間で45度刻み
            Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);

            GameObject newPrefab = Instantiate(prefabToSpawn, randomSpawnPoint, randomRotation);
            newPrefab.AddComponent<MoveAndDestroy>().Initialize(moveSpeed, moveDirection);
        }
    }

    private class MoveAndDestroy : MonoBehaviour
    {
        private float speed;
        private Vector3 direction;

        public void Initialize(float speed, Vector3 direction)
        {
            this.speed = speed;
            this.direction = direction.normalized;
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DeleteLine"))
            {
                Destroy(gameObject);
            }
        }
    }
}
