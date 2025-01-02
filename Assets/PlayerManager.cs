// Manage the player movement and collision.

using UnityEngine;
using TMPro;

// Manage various aspects of the player
public class PlayerManager : MonoBehaviour
{
    public int speed = 5; // Speed of the player
    public TextMeshProUGUI sizeText; // Text that shows player size
    public SpriteRenderer background; // Background bounds for the player
    public EntityManager entityManager; // Manage entities
    public GameManager gameManager; // Manage the game
    private Rigidbody2D rb; // Rigidbody2D for player collision
    private SpriteRenderer spriteRenderer; // SpriteRenderer to depict player
    private int size = 5; // Size of player
    private Vector2 movementInput; // Move the player
    private Vector2 backgroundMinBounds; // Bounds for movement
    private Vector2 backgroundMaxBounds; // Bounds for movement
    private Vector2 halfSpriteSize; // Half the size of the player sprite
    private Animator animator; // Animator for the red fade upon eating

    // Reset size to original 5
    public void ResetSize()
    {
        size = 5;
    }

    // Update size of sprite, boundaries, and text
    public void UpdateSize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfSpriteSize = spriteRenderer.bounds.size / 2f;

        // Update boundaries taking into consideration new sprite size
        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;
        backgroundMinBounds = new Vector2(minBounds.x + halfSpriteSize.x, minBounds.y + halfSpriteSize.y);
        backgroundMaxBounds = new Vector2(maxBounds.x - halfSpriteSize.x, maxBounds.y - halfSpriteSize.y);  

        spriteRenderer.transform.localScale = new Vector3(size / 100f, size / 100f, 1f); // Resize sprite

        sizeText.text = "Size: " + size; // Update displayed text
    }

    // Describe behavior on collision with an entity
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            EntityComponent entityComponent = other.GetComponent<EntityComponent>();
            if (entityComponent.entity.size >= size) // If entity size is >= player, game over loss
            {
                gameManager.GameOver("Loss");
            }
            else if(entityComponent.entity.size + size >= 100) // If updated player size is >= 100, game over win
            {
                size += entityComponent.entity.size;
                UpdateSize();
                gameManager.GameOver("Win");
            }
            else // Otherwise, update size, destroy collided entity, and spawn in new one of the same type
            {
                animator.Play("FadeToRed", -1, 0f); // Play red fade animation upon "eating" an entity
                size += entityComponent.entity.size;
                UpdateSize();
                bool isWandering = entityComponent.entity.isWandering; // Ensure newly spawned entity is same type
                Destroy(other.gameObject);
                entityManager.SpawnEntity(isWandering);
            }
        }
    }
    
    // Intitialize animator and components and size
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSize(); // Initialize size and boundaries
    }
    
    // Update player movement
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    // Update player movement
    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector3 targetPosition = rb.position + (Vector2)(movementInput.normalized * speed * Time.fixedDeltaTime);

            // Ensures new position is within boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, backgroundMinBounds.x, backgroundMaxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, backgroundMinBounds.y, backgroundMaxBounds.y);

            rb.MovePosition(targetPosition);
        }
    }
}
