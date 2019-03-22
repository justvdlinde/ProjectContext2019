using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LevitationObject : MonoBehaviour, ILevitatable
{
    public Action DestroyEvent { get; set; }
    public Rigidbody Rigidbody { get; protected set; }
    public LevitationManager LevitationManager { get; private set; }
    public bool IsLevitated { get; private set; }

    protected virtual void OnValidate()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void OnLevitateStart(LevitationManager levitationManager)
    {
        IsLevitated = true;
        LevitationManager = levitationManager;
    }

    public void OnLevitateStop(LevitationManager levitationManager)
    {
        IsLevitated = false;
        LevitationManager = null;
    }

    private void OnDestroy()
    {
        DestroyEvent?.Invoke();
    }
}
