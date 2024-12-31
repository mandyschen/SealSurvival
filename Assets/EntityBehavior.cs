using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    public Entity entityData;
    public GameObject textPrefab;
    public SpriteRenderer background;
    private Vector2 halfSpriteSize;

    public Vector2 GetHalfSpriteSize()
    {
        return halfSpriteSize;
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;

        float spawnX = Random.Range(minBounds.x + halfSpriteSize.x, maxBounds.x - halfSpriteSize.x);
        float spawnY = Random.Range(minBounds.y + halfSpriteSize.y, maxBounds.y - halfSpriteSize.y);

        return new Vector3(spawnX, spawnY, 0f);
    }

    public GameObject SpawnEntity(bool isWandering)
    {
        Entity entity = Instantiate(entityData);

        entity.isWandering = isWandering;
        if (isWandering)
        {
            entity.size = Random.Range(1, 101);
        }
        else{
            entity.size = 1;
        }

        GameObject entityObject = new GameObject("Entity");
        entityObject.tag = "Entity";

        SpriteRenderer entitySpriteRenderer = entityObject.AddComponent<SpriteRenderer>();
        CircleCollider2D collider = entityObject.AddComponent<CircleCollider2D>();
        
        EntityComponent entityComponent = entityObject.AddComponent<EntityComponent>();
        entityComponent.entity = entity;
        Rigidbody2D rb = entityObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        
        
        entitySpriteRenderer.sprite = entity.sprite;
        entitySpriteRenderer.transform.position = GetSpawnPosition();
        entitySpriteRenderer.sortingOrder = 1;

        collider.radius = (Mathf.Max(entitySpriteRenderer.bounds.size.x, entitySpriteRenderer.bounds.size.y) / 2f) - 0.5f;
        collider.offset = Vector2.zero;

        entityObject.transform.localScale = new Vector3(entity.size / 100f, entity.size / 100f, 1f);

        

        if (isWandering)
        {
            EntityMovement entityMovement = entityObject.AddComponent<EntityMovement>();
            entityMovement.background = background;
            Vector2 spriteWorldSize = entitySpriteRenderer.bounds.size;
            entityMovement.halfSpriteSize = spriteWorldSize / 2;
        }

        GameObject text = Instantiate(textPrefab, entitySpriteRenderer.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        text.GetComponent<TMPro.TextMeshPro>().text = entity.size.ToString();
        text.transform.SetParent(entitySpriteRenderer.transform);
        text.GetComponent<TMPro.TextMeshPro>().sortingOrder = 2;

        return entityObject;
    }
}
