using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public EntityBehavior entityBehavior;
    private List<GameObject> entities = new List<GameObject>();
    public List<GameObject> GetEntities()
    {
        return entities;
    }

    public void SpawnEntity(bool isWandering)
    {
        GameObject entityObject = entityBehavior.SpawnEntity(isWandering);
        entityObject.SetActive(true);
        entities.Add(entityObject);
    }
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
    public void Start()
    {
        SpawnInitialEntities();
    }

}
