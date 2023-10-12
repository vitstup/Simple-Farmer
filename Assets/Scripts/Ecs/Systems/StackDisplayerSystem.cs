using Leopotam.Ecs;
using UnityEngine;

public class StackDisplayerSystem : IEcsRunSystem
{
    private EcsFilter<StackDisplayerComponent, StackHandlerComponent> stackFilter = null;

    public void Run()
    {
        foreach (var i in stackFilter)
        {
            ref var stackHandlerComponent = ref stackFilter.Get2(i);
            var objects = stackHandlerComponent.objects;
            var offset = new Vector3(0, stackHandlerComponent.yOffset, 0);
            if (stackHandlerComponent.IsStackEmpty()) return;

            ref var displayerComponent = ref stackFilter.Get1(i);
            var transform = displayerComponent.objectsParentTransform;

            for (int o = 0; o < objects.Count; o++)
            {
                if (objects[o].transform.parent != transform) objects[o].transform.SetParent(transform);

                objects[o].transform.localPosition = offset * o;
            }
        }
    }
}