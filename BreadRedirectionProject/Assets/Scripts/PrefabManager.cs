using UnityEngine;
using UnityEngine.UI;

public class PrefabManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject correctIndicatorPrefab;
    public Vector3 spawnPoint;
    public Vector3 correctIndicatorPosition;
    public float spawnInterval = 1.0f;
    public int numberOfPrefabs = 5;
    public float moveSpeed = 5.0f;
    public Vector3 moveDirection = new Vector3(0, 0, -1);
    public float minX = -5.0f;
    public float maxX = 5.0f;

    [SerializeField] private Quaternion correctRotation;

    public Text correctText;
    public Text incorrectText;

    private int correctCount = 0;
    private int incorrectCount = 0;

    private void Start()
    {
        correctRotation = Quaternion.Euler(0, Random.Range(0, 8) * 45.0f, 0);
        Instantiate(correctIndicatorPrefab, correctIndicatorPosition, correctRotation);
        InvokeRepeating("SpawnAndMovePrefabs", 0.0f, spawnInterval);

        correctText.text = ": 0";
        incorrectText.text = ": 0";
    }

    private void SpawnAndMovePrefabs()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            float randomX = Random.Range(minX, maxX);
            Vector3 randomSpawnPoint = new Vector3(randomX, spawnPoint.y, spawnPoint.z);
            float randomYRotation = Random.Range(0, 8) * 45.0f;
            Quaternion randomRotation = Quaternion.Euler(0, randomYRotation, 0);

            GameObject newPrefab = Instantiate(prefabToSpawn, randomSpawnPoint, randomRotation);
            newPrefab.AddComponent<PrefabBehavior>().Initialize(moveSpeed, moveDirection, correctRotation, this);
        }
    }

    public void IncrementCorrectCount()
    {
        correctCount++;
        correctText.text = ": " + correctCount;
    }

    public void IncrementIncorrectCount()
    {
        incorrectCount++;
        incorrectText.text = ": " + incorrectCount;
    }
}

public class PrefabBehavior : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private Quaternion correctRotation;
    private PrefabManager prefabManager;

    public void Initialize(float speed, Vector3 direction, Quaternion correctRotation, PrefabManager manager)
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
