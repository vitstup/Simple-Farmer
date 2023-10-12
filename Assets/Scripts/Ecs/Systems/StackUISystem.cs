using Leopotam.Ecs;

public class StackUISystem : IEcsRunSystem
{
    private readonly EcsFilter<StackUiPresenterComponent, StackHandlerComponent> presenterFilter = null;

    public void Run()
    {
        foreach (var i in presenterFilter)
        {
            ref var presenterComponent = ref presenterFilter.Get1(i);
            var uiPresenter = presenterComponent.UI_Presenter;

            ref var stackHandler = ref presenterFilter.Get2(i);
            var objectsInStack = stackHandler.IsStackEmpty() ? 0 : stackHandler.objects.Count;
            var maxObjects = stackHandler.maxInStack;

            uiPresenter.Present(objectsInStack, maxObjects);
        }
    }
}