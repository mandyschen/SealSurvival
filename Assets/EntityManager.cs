// Spawn in entities.

using System.Collections.Generic;
using UnityEngine;

// Manage the amount of entities spawned in and some game behavior
public class EntityManager : MonoBehaviour
{
    public EntityBehavior entityBehavior; // How the entity spawned in behaves
    private List<GameObject> entities = new List<GameObject>(); // List of active entities
    
    // Return list of active entities
    public List<GameObject> GetEntities() 
    {
        return entities;
    }

    // Spawn an entity with parameter of either wandering or stagnant
    public void SpawnEntity(bool isWandering)
    {
        GameObject entityObject = entityBehavior.SpawnEntity(isWandering);
        entityObject.SetActive(true);
        entities.Add(entityObject);
    }

    // Spawn 25 stagnant entities and 15 wandering entities
    void SpawnInitialEntities()
    {
        for (int i = 0; i < 25; i++)
        {
            SpawnEntity(false);
        }
        for (int i = 0; i < 15; i++)
        {
            SpawnEntity(true);
        }
    }

    // Spawn intitial entities
    public void Start()
    {
        SpawnInitialEntities();
    }

}
