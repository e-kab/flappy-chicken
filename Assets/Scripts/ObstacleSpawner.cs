using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Downward Obstacle Prefab")]
    public GameObject obstaclePrefabDown;

    [Header("Upward Obstacle Prefab")]
    public GameObject obstaclePrefabUp;

    [Header("Minimum Obstacle Distance")]
    public float distanceMin;

    [Header("Maximum Obstacle Distance")]
    public float distanceMax;

    private float _currentTimer;

    [Header("Spawn Timer")]
    public float spawnTimer;

    [Header("Obstacle Vertical Movement Speed")]
    public float movementSpeed = 1f;

    private void SpawnNewObstacles()
    {
        Vector3 spawnPositionDown = transform.position + Vector3.down * Random.Range(distanceMin, distanceMax);
        Vector3 spawnPositionUp = transform.position + Vector3.up * Random.Range(distanceMin, distanceMax);

        GameObject obstacleDown = Instantiate(obstaclePrefabDown, spawnPositionDown, Quaternion.identity);
        GameObject obstacleUp = Instantiate(obstaclePrefabUp, spawnPositionUp, Quaternion.identity);

        // Add the movement script to each obstacle
        obstacleDown.AddComponent<ObstacleMovement>().Initialize(movementSpeed, spawnPositionDown);
        obstacleUp.AddComponent<ObstacleMovement>().Initialize(movementSpeed, spawnPositionUp);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer > spawnTimer)
        {
            SpawnNewObstacles();
            _currentTimer = 0;
        }
    }
}

public class ObstacleMovement : MonoBehaviour
{
    private float _movementSpeed;
    private Vector3 _initialPosition;

    public void Initialize(float movementSpeed, Vector3 initialPosition)
    {
        _movementSpeed = movementSpeed;
        _initialPosition = initialPosition;
    }

    void Update()
    {
        // Apply sinusoidal motion to the y position
        float newY = _initialPosition.y + Mathf.Sin(Time.time * _movementSpeed) * 0.5f; // 0.5f controls the amplitude of the movement
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
