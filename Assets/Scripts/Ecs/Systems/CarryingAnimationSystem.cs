using Leopotam.Ecs;

public class CarryingAnimationSystem : IEcsRunSystem
{
    private readonly EcsFilter<CarryingAnimationComponent, StackHandlerComponent> animationFilter = null;

    public void Run()
    {
        foreach (var i in animationFilter)
        {
            ref var stackHandler = ref animationFilter.Get2(i);
            var isCarrying = !stackHandler.IsStackEmpty();

            ref var animationComponent = ref animationFilter.Get1(i);
            ref var currentKey = ref animationComponent.currentKey;

            if (isCarrying == currentKey) return;

            var animator = animationComponent.animator;
            var key = animationComponent.animationKey;

            animator.SetBool(key, isCarrying);

            currentKey = isCarrying;
        }
    }
}