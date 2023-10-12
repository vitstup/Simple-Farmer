using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectHandler : MonoBehaviour
{
    [field: SerializeField] public int maxObjects { get; private set; }

    protected List<Item> objects = new List<Item>();

    [SerializeField] private Transform objectsParent;

    [SerializeField] private Transform[] columns;

    [SerializeField] private Vector3 baseOfset;

    public float baseYofset { get {  return baseOfset.y; }  }

    public int currentObjects { get { return objects.Count; } }

    public abstract Type itemType { get; }
    
    public bool CanAddObjects()
    {
        return objects.Count < maxObjects;
    }

    public void AddObject(Item obj)
    {
        obj.transform.parent = objectsParent;

        objects.Add(obj);

        PlaceObjects();
    }

    public Item RemoveItem()
    {
        var item = objects.Last();

        objects.Remove(item);

        PlaceObjects();

        return item;
    }

    private void PlaceObjects()
    {
        int currentColumn = 0;
        int currentOfset = 0;

        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = columns[currentColumn].position;
            objects[i].transform.position += baseOfset * currentOfset;

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