using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private float _heightOffset = 3f;
    private float _timer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTower();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(_timer < _spawnRate)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            SpawnTower();
            _timer = 0f;
        }
    }
    
    void SpawnTower()
    {
        float lowestPoint = transform.position.y - _heightOffset;
        float highestPoint = transform.position.y + _heightOffset;
        float randomY = Random.Range(lowestPoint, highestPoint);
        
        Vector3 newPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        
        GameObject tower = Instantiate(_towerPrefab, newPosition, transform.rotation);
    }
}