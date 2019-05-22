using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ScenePathAttribute : PropertyAttribute
{
    public string scene;
}
