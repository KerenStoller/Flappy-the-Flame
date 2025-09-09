using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon Instance {get; private set;}
    
    /*
     * taking care of velocity and collision
     */
    [SerializeField] private Rigidbody2D dragonRigidBody;
    [SerializeField] private float flapStrength = 5f;
    [SerializeField] private float verticalLimit = 4.75f;
    [SerializeField] private float startingPositionX = -3f;
    /*
     * taking care of sprite animation
     */
    [SerializeField] private SpriteRenderer dragonSpriteRenderer;
    [SerializeField] private Sprite[] flyingSprites;
    [SerializeField] private float flappingSpeed = .12345f;
    private int _currentSpriteIndex; // initialized to 0 by default
    private float _timer; // initialized to 0f by default
    
    public bool isAlive = true;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            if (transform.position.y < verticalLimit) // not allowing to flap above the screen
            {
                dragonRigidBody.linearVelocity = Vector2.up * flapStrength;
            }
        }
        
        CountdownForFlapping();
    }

    public void OnEnable()  // being called from LogicManager when dragon is enabled
    {
        isAlive = true;
        Vector3 position = transform.position;
        position.y = 0f;
        position.x = startingPositionX;
        transform.position = position;
        transform.rotation = new Quaternion(0,0,0,0);
        dragonRigidBody.angularVelocity = 0f;
        dragonRigidBody.linearVelocity = Vector2.zero;
        dragonSpriteRenderer.sprite = flyingSprites[0];
        _currentSpriteIndex = 0;
        _timer = 0f;
    }

    private void CountdownForFlapping()
    {
        _timer += Time.deltaTime;
        if (_timer >= flappingSpeed)
        {
            FlapWingsSprites();
            _timer = 0f;
        }
    }
    
    private void FlapWingsSprites()
    {
        if (isAlive)
        {
            _currentSpriteIndex++;
            if(_currentSpriteIndex >= flyingSprites.Length)
            {
                _currentSpriteIndex = 0;
            }
            
            dragonSpriteRenderer.sprite = flyingSprites[_currentSpriteIndex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constans.ObstacleTag))
        {
            DieAndFallToGround();
        }

        if (collision.gameObject.CompareTag(Constans.GroundTag))
        {
            DragonDead();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Constans.ScoreZoneTag) && isAlive)
        {
            LogicManager.Instance.IncreaseScore();
        }
    }

    private void DieAndFallToGround()
    {
        isAlive = false;
        dragonSpriteRenderer.sprite = flyingSprites[0];
        dragonRigidBody.gravityScale = 1f; // Make sure gravity is active
        dragonRigidBody.linearVelocity = Vector2.zero; // Stop upward motion
    }

    private void DragonDead()
    {
        isAlive = false;
        dragonSpriteRenderer.sprite = flyingSprites[0];
        LogicManager.Instance.GameOver();
    }
}
