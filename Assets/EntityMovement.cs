using System.Collections;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionInterval = 3f;
    public SpriteRenderer background;
    private Vector2 movementDirection;
    private Vector2 backgroundMinBounds;
    private Vector2 backgroundMaxBounds;
    public Vector2 halfSpriteSize;

    void Start()
    {
        Vector3 minBounds = background.bounds.min;
        Vector3 maxBounds = background.bounds.max;
        backgroundMinBounds = new Vector2(minBounds.x + halfSpriteSize.x, minBounds.y + halfSpriteSize.y);
        backgroundMaxBounds = new Vector2(maxBounds.x - halfSpriteSize.x, maxBounds.y - halfSpriteSize.y);  

        StartCoroutine(ChangeDirectionRoutine());
    }

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

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            yield return new WaitForSeconds(changeDirectionInterval);
        }
    }

    void MoveEnemy()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
    }
}
