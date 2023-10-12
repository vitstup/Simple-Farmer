using Leopotam.Ecs;
using System;
using UnityEngine;

public class StackGiver : MonoTriggerStack
{
    [SerializeField] private ObjectHandler objectHandler;

    protected override void Trigerred(ref EcsEntity entity)
    {
        GiveItems(ref entity);
    }

    private void GiveItems(ref EcsEntity entity)
    {
        if (entity.Has<StackHandlerComponent>())
        {
            ref var stack = ref entity.Get<StackHandlerComponent>();

            Type giverType = objectHandler.itemType;

            int freeObjects = objectHandler.currentObjects;

            if (freeObjects == 0) return;

            if (!stack.IsStackEmpty() && giverType != stack.itemType) return;

            if (stack.IsStackEmpty()) stack.CreateStack(giverType, objectHandler.baseYofset);

            int possibleToAdd = stack.maxInStack - stack.objects.Count;

            if (possibleToAdd <= 0) return;

            int transitObjects = freeObjects >= possibleToAdd ? possibleToAdd : freeObjects;

            Item[] transitItems = new Item[transitObjects];

            for (int i = 0; i < transitItems.Length; i++)
            {
                transitItems[i] = objectHandler.RemoveItem();
            }

            stack.AddToStack(transitItems);
        }
    }
}