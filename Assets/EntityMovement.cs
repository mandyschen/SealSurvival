// Describe how wandering entities should move.

using System.Collections;
using UnityEngine;

// Component that describes how wandering entities move
public class EntityMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of entities
    public float changeDirectionInterval = 3f; // How long to wait before entities change direction
    public SpriteRenderer background; // Background to stay in bounds and know when to turn
    private Vector2 movementDirection; // The angles of movements
    private Vector2 backgroundMinBounds; // Bounds for movement
    private Vector2 backgroundMaxBounds; // Bounds for movement
    public Vector2 halfSpriteSize; // Half the size of the entity sprite

    // Initialize bounds and couroutine for direction
    void Start()
    {
        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;
        backgroundMinBounds = new Vector2(minBounds.x + halfSpriteSize.x, minBounds.y + halfSpriteSize.y);
        backgroundMaxBounds = new Vector2(maxBounds.x - halfSpriteSize.x, maxBounds.y - halfSpriteSize.y);  

        StartCoroutine(ChangeDirectionRoutine());
    }

    // Update entity movement
    void Update()
    {
        MoveEnemy();

        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, backgroundMinBounds.x, backgroundMaxBounds.x),
            Mathf.Clamp(transform.position.y, backgroundMinBounds.y, backgroundMaxBounds.y),
            transform.position.z
        );

        transform.position = clampedPosition;
    }

    // Change entity movement direction every interval
    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    // Move the entity
    void MoveEnemy()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
    }
}
