// Holds information for generated entity objects.

using UnityEngine;

// Creates ScriptableObject to hold information
[CreateAssetMenu(fileName = "NewEntity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public int size = 1; // The size (1 for stagnant, 1-100 for wandering)
    public bool isWandering = false; // Describes type of entity
    public Sprite sprite; // What sprite will be shown
}

// Component to attach ScriptableObjects
public class EntityComponent : MonoBehaviour
{
    public Entity entity; // Entity object that holds information
}