using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;

public sealed class EcsGameStartup : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems systems;

    [SerializeField] private Apple applePrefab;
    [SerializeField] private Cookie cookiePrefab;

    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);

        systems.ConvertScene();

        AddInjections();
        AddOneFrames();
        AddSystems();

        systems.Init();
    }

    private void AddInjections()
    {
        systems.Inject(applePrefab);
        systems.Inject(cookiePrefab);
    }

    private void AddSystems()
    {
        systems.Add(new EntityInitializeSystem());
        systems.Add(new PlayerInputSystem());
        systems.Add(new RotationSystem());
        systems.Add(new MovementSystem());
        systems.Add(new MovementAnimationSystem());
        systems.Add(new StackDisplayerSystem());
        systems.Add(new CarryingAnimationSystem());

        systems.Add(new AppleSpawnerSystem());
        systems.Add(new CookieSpawnSystem());

        systems.Add(new StackUISystem());
    }

    private void AddOneFrames()
    {
        
    }

    private void Update()
    {
        systems.Run();
    }

    private void OnDestroy()
    {
        if (systems == null) return; 

        systems.Destroy();
        systems = null;
        world.Destroy();
        world = null;
    }
}
