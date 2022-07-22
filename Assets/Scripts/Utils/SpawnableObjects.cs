using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Objects", menuName = "Spawnable Objects/Objects", order = 1)]
public class SpawnableObjects : ScriptableObject
{
    public List<ObjectsList> objectList;

    public List<string> GetAllObjectListTypes()
    {
        List<string> types = new List<string>();

        foreach (var obj in objectList)
        {
            types.Add(obj.name);
        }

        return types;
    }

    public GameObject GetRandomGameObjectFromType(string type)
    {
        foreach (var obj in objectList)
        {
            //Debug.Log(obj.name + " " + type);
            
            if (String.CompareOrdinal(obj.name, type) == 0)
            {
                return obj.objects[Random.Range(0, obj.objects.Count)];
            }
        }

        return null;
    }
}

[Serializable]
public struct ObjectsList
{
    public string name;
    public List<GameObject> objects;
}
