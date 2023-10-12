using System;
using UnityEngine;

[Serializable]
public struct ObjectSpawnerComponent 
{
    public float spawnTime;

    public ObjectHandler handler;

    [HideInInspector] public float timeAfterPreviosSpawn;
}