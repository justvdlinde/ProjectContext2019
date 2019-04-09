using UnityEngine;

public struct TransformData 
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Transform parent;

    public TransformData(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;

        parent = transform.parent;
    }

    public TransformData(Vector3 position, Quaternion rotation, Vector3 scale, Transform parent = null)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;

        this.parent = parent;
    }
}
