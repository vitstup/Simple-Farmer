using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ObjectPool
{
    private GameObject prefab;

    private List<GameObject> pool = new List<GameObject>();

    public bool isAutoExpanded;

    public ObjectPool(GameObject prefab, int count, bool isAutoExpanded)
    {
        this.prefab = prefab;
        this.isAutoExpanded = isAutoExpanded;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        var obj = Object.Instantiate(prefab);
        obj.SetActive(isActiveByDefault);
        pool.Add(obj);
        return obj;
    }

    private bool HasFreeElement(out GameObject element)
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeSelf)
            {
                element = obj;
                return true;
            }
        }
        element = null;
        return false;
    }

    public GameObject GetElement()
    {
        GameObject result = null;
        if (HasFreeElement(out var element)) { result = element; result.SetActive(true); }
        else if (!isAutoExpanded) throw new System.Exception("There is no free elements");
        else result = CreateObject(true);
        return result;
    }
}