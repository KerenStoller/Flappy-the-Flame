using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _movingSpeed = 3f;
    [SerializeField] private float leftEndX = -6f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftEndX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * (_movingSpeed * Time.deltaTime);
        
        if(transform.position.x < leftEndX)
        {
            Destroy(gameObject);
        }
    }
}
