using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

/// <summary>
/// Base non-generic.
/// DO NOT DERIVE FROM THIS. Use the generic version instead.
/// </summary>
public abstract class ManagerBase : MonoBehaviour
{
    /// <summary>
    /// Called when a manager is loaded.
    /// </summary>
    public virtual void Deserialize() { }
}