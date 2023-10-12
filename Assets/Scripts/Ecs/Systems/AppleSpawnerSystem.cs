using Leopotam.Ecs;
using UnityEngine;

public class AppleSpawnerSystem : IEcsRunSystem
{
    private readonly EcsFilter<AppleSpawnerComponent, ObjectSpawnerComponent> appleSpawnerFilter = null;

    private readonly Apple applePrefab = null;

    private MonoPool<Apple> applesPool;


    public void Run()
    {
        if (applesPool == null) InitPool();

        foreach(var i in appleSpawnerFilter)
        {
            ref var objectSpawnerComponent = ref appleSpawnerFilter.Get2(i);
            ref var objectHandler = ref objectSpawnerComponent.handler;

            ref var timeToSpawn = ref objectSpawnerComponent.spawnTime;
            ref var timePassed = ref objectSpawnerComponent.timeAfterPreviosSpawn;

            timePassed += Time.deltaTime;

            bool needToSpawnObjects = objectHandler.CanAddObjects();

            if (!needToSpawnObjects) continue;

            if (timePassed < timeToSpawn) continue;

            timePassed = 0;

            var obj = applesPool.GetElement();

            objectHandler.AddObject(obj);
        }
    }

    private void InitPool()
    {
        applesPool = new MonoPool<Apple>(applePrefab, 5, true);
    }
}