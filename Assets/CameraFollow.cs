// Follows the player so the camera stays centered on the player.

using UnityEngine;

// Handles camera movement
public class CameraFollow : MonoBehaviour
{
    public Transform player; // The Transform component of the player
    private Vector3 offset; // Distance between camera and player

    // Initialize the offset
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update the camera position based on player movement to keep it centered
    void LateUpdate()
    {
        Vector3 newCameraPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
        transform.position = newCameraPosition;
    }
}
