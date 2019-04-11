using UnityEngine;

public static class TransformExtensions 
{
    public static void SetFromData(this Transform transform, TransformData data, bool alsoSetParent = true)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
        transform.localScale = data.scale;

        if (alsoSetParent == true && data.parent != null)
        {
            transform.parent = data.parent;
        }
    }
}
