using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehavior : MonoBehaviour
{
    public Entity entityData;
    public GameObject textPrefab;
    public SpriteRenderer background;
    private Vector2 halfSpriteSize;

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
        collider.isTrigger = true;
        EntityComponent entityComponent = entityObject.AddComponent<EntityComponent>();
        entityComponent.entity = entity;

        entitySpriteRenderer.sprite = entity.sprite;
        entitySpriteRenderer.transform.position = GetSpawnPosition();
        entitySpriteRenderer.sortingOrder = 1;

        // entitySpriteRenderer.transform.localScale = new Vector3(entity.size, entity.size, 1);

        GameObject text = Instantiate(textPrefab, entitySpriteRenderer.transform.position, Quaternion.identity);
        text.GetComponent<TMPro.TextMeshPro>().text = $"Size: {entity.size}";
        text.transform.SetParent(entitySpriteRenderer.transform);
        text.GetComponent<TMPro.TextMeshPro>().sortingOrder = 2;

        return entityObject;
    }
}
