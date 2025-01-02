// Describe how entities should be spawned in.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Describes how and where entities should spawn
public class EntityBehavior : MonoBehaviour
{
    public Entity entityData; // Information about entities (described in Entity.cs)
    public GameObject textPrefab; // Text that displays size
    public SpriteRenderer background; // Background sprite to keep spawn within bounds
    public PlayerManager player; // Gives us player information (needed for location)
    private Vector2 halfSpriteSize; // Keep track of size of sprites

    // Returns half the size of the entity sprite
    public Vector2 GetHalfSpriteSize()
    {
        return halfSpriteSize;
    }

    // Returns a valid spawn position
    Vector3 GetSpawnPosition()
    {
        // Prevents spawning too close to the player
        Vector3 playerPosition = player.transform.position;
        float minSafeDistance = 5f;

        // Prevents spawning outside of the background bounds
        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;

        Vector3 spawnPosition = Vector3.zero;

        // Loops until a valid spawn location is found
        bool isSpawnSafe = false;
        while (!isSpawnSafe)
        {
            // Finds a random location within the bounds of the background
            float spawnX = Random.Range(minBounds.x + halfSpriteSize.x, maxBounds.x - halfSpriteSize.x);
            float spawnY = Random.Range(minBounds.y + halfSpriteSize.y, maxBounds.y - halfSpriteSize.y);
            
            spawnPosition = new Vector3(spawnX, spawnY, 0f);
            
            float distanceToPlayer = Vector3.Distance(spawnPosition, playerPosition);

            // Spawn location is valid when it is far enough from the player location
            if (distanceToPlayer >= minSafeDistance)
            {
                isSpawnSafe = true;
            }
        }

        return spawnPosition;
    }

    // Spawns and returns a valid entity
    public GameObject SpawnEntity(bool isWandering)
    {
        Entity entity = Instantiate(entityData);

        // If entity is wandering type, it generates a random size 1-100, else it is size 1
        entity.isWandering = isWandering;
        if (isWandering)
        {
            entity.size = Random.Range(1, 101);
        }
        else{
            entity.size = 1;
        }

        GameObject entityObject = new GameObject("Entity");
        entityObject.tag = "Entity"; // For collision detection

        // Adds SpriteRenderer, CircleCollider2D, EntityComponent, and Rigidbody2D components
        SpriteRenderer entitySpriteRenderer = entityObject.AddComponent<SpriteRenderer>();

        CircleCollider2D collider = entityObject.AddComponent<CircleCollider2D>();

        EntityComponent entityComponent = entityObject.AddComponent<EntityComponent>();
        entityComponent.entity = entity;

        Rigidbody2D rb = entityObject.AddComponent<Rigidbody2D>(); // For collision detection in moving entities
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        // Update sprite, position, and layer order
        entitySpriteRenderer.sprite = entity.sprite;
        entitySpriteRenderer.transform.position = GetSpawnPosition();
        entitySpriteRenderer.sortingOrder = 1;

        // Update collider radius and offset to accurately map onto sprite with slight allowance
        collider.radius = (Mathf.Max(entitySpriteRenderer.bounds.size.x, entitySpriteRenderer.bounds.size.y) / 2f) - 0.5f;
        collider.offset = Vector2.zero;

        // Resize entity based on size aspect (1 for stagnant, 1-100 for wandering entities)
        entityObject.transform.localScale = new Vector3(entity.size / 100f, entity.size / 100f, 1f);

        // Add movement to wandering entities
        if (isWandering)
        {
            EntityMovement entityMovement = entityObject.AddComponent<EntityMovement>();
            entityMovement.background = background;
            Vector2 spriteWorldSize = entitySpriteRenderer.bounds.size;
            entityMovement.halfSpriteSize = spriteWorldSize / 2;
        }

        // Add text stating size by entities (for both debugging and ease of use for players)
        GameObject text = Instantiate(textPrefab, entitySpriteRenderer.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        text.GetComponent<TMPro.TextMeshPro>().text = entity.size.ToString();
        text.transform.SetParent(entitySpriteRenderer.transform); // Placed under entity's SpriteRenderer
        text.GetComponent<TMPro.TextMeshPro>().sortingOrder = 2; // On top of all entities

        return entityObject;
    }
}
