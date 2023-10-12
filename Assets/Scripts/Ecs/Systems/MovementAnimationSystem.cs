using Leopotam.Ecs;

public class MovementAnimationSystem : IEcsRunSystem
{
    private readonly EcsFilter<MovementAnimationComponent, MovableComponent> animationFilter = null;

    public void Run()
    {
        foreach (var i in animationFilter)
        {
            ref var movableComponent = ref animationFilter.Get2(i);
            ref var isMoving = ref movableComponent.isMoving;

            ref var animationComponent = ref animationFilter.Get1(i);
            ref var currentKey = ref animationComponent.currentKey;

            if (isMoving == currentKey) continue;

            ref var animator = ref animationComponent.animator;
            ref var key = ref animationComponent.animationKey;

            animator.SetBool(key, isMoving);

            currentKey = isMoving;
        }
    }
}