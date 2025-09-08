using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 3f;
    [SerializeField] private float leftEndX = -6f;
    
    private void Start()
    {
        leftEndX = Camera.main!.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1f;
    }
    
    private void Update()
    {
        transform.position += Vector3.left * (movingSpeed * Time.deltaTime);
        
        if(transform.position.x < leftEndX)
        {
            Destroy(gameObject);
        }
    }
}
