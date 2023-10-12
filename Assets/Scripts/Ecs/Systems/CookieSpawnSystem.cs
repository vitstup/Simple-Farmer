using Leopotam.Ecs;
using UnityEngine;

public class CookieSpawnSystem : IEcsRunSystem
{
    private readonly EcsFilter<CookieSpawnerComponent, ObjectSpawnerComponent> cookieSpawnerFilter = null;

    private readonly Cookie cookiePrefab = null;

    private MonoPool<Cookie> cookiesPool;

    public void Run()
    {
        if (cookiesPool == null) InitPool();

        foreach (var i in cookieSpawnerFilter)
        {
            ref var objectSpawnerComponent = ref cookieSpawnerFilter.Get2(i);
            ref var objectHandler = ref objectSpawnerComponent.handler;

            ref var timeToSpawn = ref objectSpawnerComponent.spawnTime;
            ref var timePassed = ref objectSpawnerComponent.timeAfterPreviosSpawn;

            timePassed += Time.deltaTime;

            bool needToSpawnObjects = objectHandler.CanAddObjects();

            if (!needToSpawnObjects) continue;

            if (timePassed < timeToSpawn) continue;

            timePassed = 0;

            var obj = cookiesPool.GetElement();

            objectHandler.AddObject(obj);
        }
    }

    private void InitPool()
    {
        cookiesPool = new MonoPool<Cookie>(cookiePrefab, 5, true);
    }
}