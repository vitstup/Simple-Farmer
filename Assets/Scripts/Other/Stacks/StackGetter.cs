using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StackGetter : MonoTriggerStack
{
    private Type stackType;
    private Vector3 baseOffset;

    [SerializeField] private int maxObjects;
    [SerializeField] private Transform objectsParent;
    [SerializeField] private Transform[] columns;

    private List<Item> items = new List<Item>();

    protected override void Trigerred(ref EcsEntity entity)
    {
        if (entity.Has<StackHandlerComponent>())
        {
            ref var stack = ref entity.Get<StackHandlerComponent>();

            if (stack.IsStackEmpty()) return;

            if (stackType != null && stack.itemType != stackType) return;

            if (stackType == null) { stackType = stack.itemType; baseOffset = new Vector3(0, stack.yOffset, 0); }

            int freeSpace = maxObjects - items.Count;

            if (freeSpace <= 0) return;

            int addAmount = stack.objects.Count <= freeSpace ? stack.objects.Count : freeSpace;

            for (int i = 0; i < addAmount; i++)
            {
                var item = stack.RemoveItem();

                item.transform.SetParent(objectsParent);

                items.Add(item);
            }

            PlaceObjects();
        }
    }

    private void PlaceObjects()
    {
        int currentColumn = 0;
        int currentOfset = 0;

        for (int i = 0; i < items.Count; i++)
        {
            items[i].transform.position = columns[currentColumn].position;
            items[i].transform.position += baseOffset * currentOfset;

            currentColumn++;
            if (currentColumn >= columns.Length) { currentColumn = 0; currentOfset++; }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        if (columns == null) return;
        for (int i = 0; i < columns.Length; i++)
        {
            Gizmos.DrawWireSphere(columns[i].position, 0.1f);
        }
    }
}