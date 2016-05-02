using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

/// <summary>
/// This simple tool is there to guaranty that one Universe exist at all time.
/// Should a new Manager be found, it saves it as an Asset.
/// If the current scene has no Universe, we make a fake one.
/// </summary>
[InitializeOnLoad]
public class UniverseTool
{
    private const string DataPath = "/Resources/Universe/";
    private const string AssetDataPath = "Assets" + DataPath;
    private const string PrefabExtension = ".prefab";

    static UniverseTool()
    {
        EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
        Universe.OnManagerCreated += ManagerCreated;

        PlaymodeStateChanged();
    }

    private static void ManagerCreated(object sender, Universe.NewManagerEventArgs e)
    {
        CreateAsset<ManagerBase>(e.Manager);
    }

    private static void PlaymodeStateChanged()
    {
        if (!Application.isPlaying)
            return;

        GameObject go = GameObject.Find("Universe");
        if (go == null)
            go = new GameObject("Universe");

        Universe universe = go.GetComponent<Universe>();
        if (universe == null)
            universe = go.AddComponent<Universe>();
    }

    /// <summary>
    /// This saves the Manager in the Resources Folder for in-game retreival.
    /// </summary>
    private static void CreateAsset<T>(T asset) where T : ManagerBase
    {
        DirectoryInfo directory = new DirectoryInfo(Application.dataPath + DataPath);
        if (!directory.Exists)
            directory.Create();

        string assetPathAndName = AssetDataPath + asset.GetType().Name + PrefabExtension;
        PrefabUtility.CreatePrefab(assetPathAndName, asset.gameObject);
    }
}