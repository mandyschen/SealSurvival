using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewEntity", menuName = "Entity")]
public class Entity : ScriptableObject
{
    public int size = 1;
    public bool isWandering = false;
    public Sprite sprite;
}

public class EntityComponent : MonoBehaviour
{
    public Entity entity;
}