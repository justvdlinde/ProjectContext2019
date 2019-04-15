using UnityEngine;

public static class TransformExtensions 
{
    public static void SetFromData(this Transform transform, TransformData data)
    {
        if (data.parent != null)
        {
            transform.SetParent(data.parent, true);
        }

        transform.position = data.position;
        transform.rotation = data.rotation;
        transform.localScale = data.scale;
    }
}
