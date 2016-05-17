using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

/// <summary>
/// Base manager class that inforces a self-referencing singleton pattern.
/// Your class should derive from this, not directly from the non-generic Manager.
/// </summary>
/// <typeparam name="T">Self</typeparam>
public abstract class Manager<T> : ManagerBase where T : Manager<T>
{
    private static T instance = null;

    public static T Instance
    {
        get { return instance; }
    }

    protected Manager() { }

    /// <summary>
    /// Called when a deserialized version is loaded.
    /// </summary>
    public override void Deserialize()
    {
        instance = (T)this;
    }
}