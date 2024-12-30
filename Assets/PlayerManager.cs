using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using Unity.Mathematics;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public int speed = 5;
    public TextMeshProUGUI sizeText;
    public SpriteRenderer background;
    public EntityManager entityManager;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int size = 5;
    private Vector2 movementInput;
    private Vector2 backgroundMinBounds;
    private Vector2 backgroundMaxBounds;
    private Vector2 halfSpriteSize;

    void IncreaseSize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfSpriteSize = spriteRenderer.bounds.size / 2f;

        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;
        backgroundMinBounds = new Vector2(minBounds.x + halfSpriteSize.x, minBounds.y + halfSpriteSize.y);
        backgroundMaxBounds = new Vector2(maxBounds.x - halfSpriteSize.x, maxBounds.y - halfSpriteSize.y);   

        sizeText.text = "Size: " + size;     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Entity"))
        {
            EntityComponent entityComponent = other.GetComponent<EntityComponent>();
            size += entityComponent.entity.size;
            Debug.Log("Collected Size: " + entityComponent.entity.size);
            IncreaseSize();
            bool isWandering = entityComponent.entity.isWandering;
            // Debug.Log("boolean iswandering: " + isWandering);

            Destroy(other.gameObject);
            entityManager.SpawnEntity(isWandering);
        }
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        IncreaseSize();
    }
    
    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            Vector3 targetPosition = rb.position + (Vector2)(movementInput.normalized * speed * Time.fixedDeltaTime);

            targetPosition.x = Mathf.Clamp(targetPosition.x, backgroundMinBounds.x, backgroundMaxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, backgroundMinBounds.y, backgroundMaxBounds.y);

            rb.MovePosition(targetPosition);
        }
    }
}
