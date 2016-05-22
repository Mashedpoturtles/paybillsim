using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

#if !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2
using UnityEngine.SceneManagement;
#endif

namespace Universe
{
    /// <summary>
    /// This simple tool is there to guaranty that one Universe exist at all time.
    /// Should a new Manager be found, it saves it as an Asset.
    /// </summary>
    [InitializeOnLoad]
    public class UniverseTool
    {
        private const string NAME = "Universe";
        private const string SCENE = "00 - Universe";

        static UniverseTool()
        {
            EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
            PlaymodeStateChanged();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IManager).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        GameObject go = Resources.Load(Universe.PATH + type.Name) as GameObject;
                        if (go != null)
                            continue;

                        go = new GameObject(type.Name);
                        go.AddComponent(type);

                        PrefabUtility.CreatePrefab("Assets/Resources/Universe/" + type.Name + ".prefab", go.gameObject);
                        GameObject.DestroyImmediate(go);
                    }
                }
            }
        }

        private static void PlaymodeStateChanged()
        {
            if (Application.isPlaying)
            {
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
                if (Application.loadedLevelName != SCENE)
#else
                if (SceneManager.GetActiveScene().name != SCENE)
#endif
                {
                    GameObject go = GameObject.Find(NAME);
                    if (go == null)
                        go = new GameObject(NAME);

                    Universe universe = go.GetComponent<Universe>();
                    if (universe == null)
                        universe = go.AddComponent<Universe>();
                }
            }
        }
    }
}