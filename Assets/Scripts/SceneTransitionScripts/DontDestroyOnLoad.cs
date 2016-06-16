using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
    {
    private void Awake ( )
        {
        DontDestroyOnLoad ( transform.gameObject );
        }
    }
