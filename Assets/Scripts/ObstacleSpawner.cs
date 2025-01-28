using System.IO;
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

        Transform chickenTransform = GameObject.FindWithTag("Chicken").transform;

        // Add the movement script to each obstacle
        obstacleDown.AddComponent<ObstacleMovement>().Initialize(movementSpeed, spawnPositionDown, chickenTransform);
        obstacleUp.AddComponent<ObstacleMovement>().Initialize(movementSpeed, spawnPositionUp, chickenTransform);
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
    private float _phaseOffset;
    private Transform _chickenTransform; // Reference to the chicken/player

    public void Initialize(float movementSpeed, Vector3 initialPosition, Transform chickenTransform)
    {
        _movementSpeed = movementSpeed;
        _initialPosition = initialPosition;
        _phaseOffset = Random.Range(0f, Mathf.PI * 2);
        _chickenTransform = chickenTransform; // Store the reference to the chicken

    }

    void Update()
    {
        // Apply sinusoidal motion to the y position
        float newY = _initialPosition.y + Mathf.Sin(Time.time * _movementSpeed + _phaseOffset) * 0.5f; // 0.5f controls the amplitude of the movement
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        // Destroy the obstacle if it is far behind the chicken
        if (_chickenTransform != null && (_chickenTransform.position.x - transform.position.x) > 15)
        {
            Destroy(gameObject);
        }

    }
}
