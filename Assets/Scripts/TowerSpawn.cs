using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float heightOffset = 3f;
    private float _timer; // initialized to 0 by default
    
    private void Start()
    {
        SpawnTower();
    }
    
    private void Update()
    {
        if(_timer < spawnRate)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            SpawnTower();
            _timer = 0f;
        }
    }
    
    private void SpawnTower()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomY = Random.Range(lowestPoint, highestPoint);
        
        Vector3 newPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(towerPrefab, newPosition, transform.rotation);
    }
}