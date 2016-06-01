using UnityEngine;
using System.Collections;

public class LightWall : MonoBehaviour
    {
    [SerializeField]
    Renderer [ ] parentRenderer;
    Material mat;

    void Start ( )
        {
        parentRenderer = GetComponentsInChildren<Renderer> ( );
        }

    void Update ( )
        {
        foreach ( Renderer childRenderer in parentRenderer )
            {
            if ( Input.GetKeyDown ( KeyCode.L ) )
                {
                Color final = Color.white * Mathf.LinearToGammaSpace ( 4 );
                childRenderer.material.SetColor ( "_EmissionColor", final );
                DynamicGI.SetEmissive ( childRenderer, final );
                }
            }
        }
    }
