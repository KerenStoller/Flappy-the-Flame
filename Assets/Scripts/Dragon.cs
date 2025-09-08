using UnityEngine;

public class Dragon : MonoBehaviour
{
    public static Dragon Instance {get; private set;}
    /*
     * taking care of velocity and collision
     */
    [SerializeField] private Rigidbody2D _dragonRigidBody;
    [SerializeField] private float _flapStrength = 5f;
    [SerializeField] private float _verticalLimit = 4.75f;
    [SerializeField] private float startingPositionX = -3f;
    
    public bool isAlive = true;
    
    /*
     * taking care of sprite animation
     */
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite dyingSprite;
    [SerializeField] private Sprite[] flappingSprites;
    [SerializeField] private float flappingSpeed = .12345f;
    private int currentSpriteIndex = 0;
    private float timer = 0f;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position.y < _verticalLimit)
            {
                FlapUp();
            }
        }
        
        countdownForFlapping();
    }

    public void OnEnable()
    {
        isAlive = true;
        Vector3 position = transform.position;
        position.y = 0f;
        position.x = startingPositionX;
        transform.position = position;
        transform.rotation = new Quaternion(0,0,0,0);
        _dragonRigidBody.angularVelocity = 0f;
        _dragonRigidBody.linearVelocity = Vector2.zero;
        spriteRenderer.sprite = dyingSprite;
        currentSpriteIndex = 0;
        timer = 0f;
    }

    private void countdownForFlapping()
    {
        timer += Time.deltaTime;
        if (timer >= flappingSpeed)
        {
            flapWingsSprites();
            timer = 0f;
        }
    }
    
    private void flapWingsSprites()
    {
        if (isAlive)
        {
            currentSpriteIndex++;
            if(currentSpriteIndex >= flappingSprites.Length)
            {
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = flappingSprites[currentSpriteIndex];
        }
    }

    public void FlapUp()
    {
        if (isAlive)
        {
            _dragonRigidBody.linearVelocity = Vector2.up * _flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            DieAndFallToGround();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            dragonDead();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScoreZone") && isAlive)
        {
            LogicManager.Instance.IncreaseScore();
        }
    }

    private void DieAndFallToGround()
    {
        isAlive = false;
        spriteRenderer.sprite = dyingSprite;
        _dragonRigidBody.gravityScale = 1f; // Make sure gravity is active
        _dragonRigidBody.linearVelocity = Vector2.zero; // Stop upward motion
    }

    private void dragonDead()
    {
        isAlive = false;
        spriteRenderer.sprite = dyingSprite;
        LogicManager.Instance.GameOver();
    }
}
