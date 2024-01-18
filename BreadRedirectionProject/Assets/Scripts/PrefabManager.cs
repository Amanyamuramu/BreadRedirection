using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject correctIndicatorPrefab;
    public Vector3 spawnPoint;
    public Vector3 correctIndicatorPosition;
    public float spawnInterval = 2.0f;
    public int numberOfPrefabs = 1;
    public float moveSpeed = 0.5f;
    Vector3 moveDirection = new Vector3(0, 0, -1);
    float minX = -2.0f;
    float maxX = 2.0f;
    Quaternion correctRotation;

    public int correctCount = 0;
    public int incorrectCount = 0;

    public AudioClip correctSound;
    public AudioClip incorrectSound;
    private AudioSource audioSource;

    private float elapsedTime = 0.0f; 

    private void Start()
    {
        correctRotation = Quaternion.Euler(0, Random.Range(0, 8) * 45.0f, 0);
        Instantiate(correctIndicatorPrefab, correctIndicatorPosition, correctRotation);
        InvokeRepeating("SpawnAndMovePrefabs", 0.0f, spawnInterval);
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }

     private void Update()
    {
        elapsedTime += Time.deltaTime;

        // 経過時間に応じてnumberOfPrefabsの値を更新
        if (elapsedTime >= 30.0f)
        {
            numberOfPrefabs = 3;
        }
        else if (elapsedTime >= 15.0f)
        {
            numberOfPrefabs = 2;
        }
        else if (elapsedTime >= 0.0f)
        {
            numberOfPrefabs = 1;
        }
    }

    private void SpawnAndMovePrefabs()
    {
        List<Vector3> usedPositions = new List<Vector3>();

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float randomX;
            Vector3 randomSpawnPoint;
            bool positionValid;

            // 重複しない位置を探す
            do
            {
                randomX = Random.Range(minX, maxX);
                randomSpawnPoint = new Vector3(randomX, spawnPoint.y, spawnPoint.z);
                positionValid = true;

                // すでに使用されている位置との間隔をチェック
                foreach (Vector3 usedPos in usedPositions)
                {
                    if (Vector3.Distance(randomSpawnPoint, usedPos) < 1.0f) // 1.0f は最小間隔
                    {
                        positionValid = false;
                        break;
                    }
                }
            } while (!positionValid);

            usedPositions.Add(randomSpawnPoint);

            float randomYRotation = Random.Range(0, 8) * 45.0f;
            Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);

            GameObject newPrefab = Instantiate(prefabToSpawn, randomSpawnPoint, randomRotation);
            newPrefab
                .AddComponent<PrefabBehavior>()
                .Initialize(moveSpeed, moveDirection, correctRotation, this);
        }
    }

    public void IncrementCorrectCount()
    {
        correctCount++;
        audioSource.PlayOneShot(correctSound);
    }

    public void IncrementIncorrectCount()
    {
        incorrectCount++;
        audioSource.PlayOneShot(incorrectSound);
    }
}

public class PrefabBehavior : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private Quaternion correctRotation;
    private PrefabManager prefabManager;

    public void Initialize(
        float speed,
        Vector3 direction,
        Quaternion correctRotation,
        PrefabManager manager
    )
    {
        this.speed = speed;
        this.direction = direction.normalized;
        this.correctRotation = correctRotation;
        prefabManager = manager;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeleteLine"))
        {
            if (Quaternion.Angle(transform.rotation, correctRotation) < 1.0f)
            {
                prefabManager.IncrementCorrectCount();
            }
            else
            {
                prefabManager.IncrementIncorrectCount();
            }
            Destroy(gameObject);
        }
    }
}
