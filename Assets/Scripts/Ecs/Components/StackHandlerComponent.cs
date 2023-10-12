using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct StackHandlerComponent
{
    public int maxInStack;

    [HideInInspector] public List<Item> objects;

    [HideInInspector] public Type itemType;

    [HideInInspector] public float yOffset;

    public bool IsStackEmpty()
    {
        return objects == null || objects.Count == 0;
    }

    public void CreateStack(Type itemType, float yOffset)
    {
        this.itemType = itemType;
        this.yOffset = yOffset;
        objects = new List<Item>();
    }

    public void AddToStack(params Item[] items)
    {
        foreach(Item item in items)
        {
            objects.Add(item);
        }
    }

    public Item RemoveItem()
    {
        var item = objects.Last();

        objects.Remove(item);

        if (objects.Count <= 0) ClearStack();

        return item;
    }

    private void ClearStack()
    {
        itemType = null;
        objects = null;

        yOffset = 0;
    }
}