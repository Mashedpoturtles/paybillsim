using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Entry point of all game-wide serialized data.
/// The Universe is a self-regulating script.
/// When awaken, it loads all the possible game Managers.
/// </summary>
[AddComponentMenu ( "" )]
public sealed class Universe : MonoBehaviour
    {
    private const string UniversePath = "Universe/";

    private static Universe instance;

    public static Universe Instance
        {
        get { return instance; }
        }

    private List<ManagerBase> managers = new List<ManagerBase> ( );

    /// <summary>
    /// Only if you want all existing managers.
    /// Otherwise, you should access one by the Instance property.
    /// </summary>
    public ManagerBase [ ] Managers
        {
        get { return managers.ToArray ( ); }
        }

    private bool initialized = false;

    #region Event
    public delegate void NewManagerEventHandler ( object sender, NewManagerEventArgs e );

    public class NewManagerEventArgs
        {
        private ManagerBase manager;

        public ManagerBase Manager
            {
            get { return manager; }
            }

        public NewManagerEventArgs ( ManagerBase manager )
            {
            this.manager = manager;
            }
        }

    /// <summary>
    /// Fired when a new Manager type is found. Editor Only.
    /// </summary>
    public static event NewManagerEventHandler OnManagerCreated;
    #endregion

    private void Start ( )
        {
        if ( instance == null )
            {
            instance = this;
            DontDestroyOnLoad ( gameObject );
            }
        else if ( instance != this ) // Frankly, this should never happen. Someone made an error otherwise.
            {
            Destroy ( gameObject );
            return;
            }

        initialized = managers.Count != 0;
        if ( initialized )
            return;

        Deserialize ( this );
        initialized = true;
        }

    /// <summary>
    /// Used on editor only, to load a manager.
    /// For example, if you make a Web manager and want to use it in Editor mode, you use this.
    /// </summary>
    public static void EditorLoad ( Type type )
        {
        if ( Application.isPlaying || !typeof ( ManagerBase ).IsAssignableFrom ( type ) )
            return;

        GameObject go = Resources.Load ( UniversePath + type.Name ) as GameObject;
        go = Instantiate ( go ) as GameObject;
        ManagerBase manager = go.GetComponent ( type ) as ManagerBase;
        manager.Deserialize ( );
        go.hideFlags = HideFlags.HideAndDontSave;
        }

    /// <summary>
    /// Retrieve all the Manager and their data.
    /// If data is inexistant, create a new one.
    /// </summary>
    private void Deserialize ( Universe universe )
        {
        Assembly [ ] assemblies = AppDomain.CurrentDomain.GetAssemblies ( );
        for ( int i = 0 ; i < assemblies.Length ; i++ )
            {
            Assembly assembly = assemblies [ i ];

            Type [ ] types = assembly.GetTypes ( );
            for ( int j = 0 ; j < types.Length ; j++ )
                {
                Type type = types [ j ];

                if ( typeof ( ManagerBase ).IsAssignableFrom ( type ) && !type.IsAbstract )
                    {
                    GameObject go = GameObject.Find ( type.Name );

                    ManagerBase manager = null;
                    if ( go != null )
                        manager = go.GetComponent ( type ) as ManagerBase;

                    // None in memory
                    if ( manager == null )
                        {
                        go = Resources.Load ( UniversePath + type.Name ) as GameObject;

                        if ( go != null )
                            {
                            GameObject clone = Instantiate ( go ) as GameObject;
                            clone.name = type.Name;
                            clone.transform.parent = Universe.Instance.transform;
                            DontDestroyOnLoad ( clone.transform.root );
                            manager = clone.GetComponent ( type ) as ManagerBase;
                            }
                        }

                    // If a manager is still not loaded, it's because it is a new one that was never serialized before.
                    // In all aspect, that should only happens within the scope of the Editor as a coder add a new Manager type.
                    if ( Application.isEditor )
                        {
                        if ( manager == null )
                            {
                            Debug.Log ( "New Manager type found: " + type.Name + ". A new prefab have been created to host it." );

                            go = new GameObject ( type.Name );
                            manager = go.AddComponent ( type ) as ManagerBase;
                            manager.transform.parent = Universe.Instance.transform;

                            if ( manager != null && OnManagerCreated != null )
                                OnManagerCreated ( Universe.Instance, new NewManagerEventArgs ( manager ) );
                            }
                        else
                            {
                            RemoveExisting ( type );
                            manager.Deserialize ( );
                            managers.Add ( manager );
                            }
                        }
                    }
                }
            }
        }

    /// <summary>
    /// This was added because of Managers that are loaded by an editor tool.
    /// They could carry on as being loaded and ends up having duplicates.
    /// </summary>
    private void RemoveExisting ( Type type )
        {
        for ( int i = 0 ; i < managers.Count ; i++ )
            if ( managers [ i ].GetType ( ) == type )
                managers.RemoveAt ( i );
        }
    }