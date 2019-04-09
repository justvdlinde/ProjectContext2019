using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    public static T GetInterface<T>(this GameObject obj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an interface!");
            return null;
        }
        return obj.GetComponent<T>() as T;
    }

    public static T[] GetInterfaces<T>(this GameObject obj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an interface!");
            return null; ;
        }

        return obj.GetComponents<T>() as T[];
    }

    public static T GetRandom<T>(this T[] arr)
    {
        return arr[UnityEngine.Random.Range(0, arr.Length)];
    }
}
