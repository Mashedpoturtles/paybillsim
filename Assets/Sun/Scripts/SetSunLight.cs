using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour
    {
    public GameObject windowLight;
    public Renderer water;
    public Transform stars;
    public Transform worldProbe;
    public bool IsNight;

    void Update ( )
        {
        stars.transform.rotation = transform.rotation;
        if ( IsNight )
            {
            foreach ( Renderer childRenderer in windowLight.GetComponentsInChildren<Renderer> ( ) )
                {
                Color final = Color.white * Mathf.LinearToGammaSpace ( 5 );
                childRenderer.material.SetColor ( "_EmissionColor", final );
                DynamicGI.SetEmissive ( childRenderer, final );
                }
            foreach ( Light childLight in windowLight.GetComponentsInChildren<Light> ( ) )
                {
                childLight.enabled = true;
                }
            }
        else
            {
            foreach ( Renderer childRenderer in windowLight.GetComponentsInChildren<Renderer> ( ) )
                {
                Color final = Color.white * Mathf.LinearToGammaSpace ( 0.1f );
                childRenderer.material.SetColor ( "_EmissionColor", final );
                DynamicGI.SetEmissive ( childRenderer, final );
                }
            foreach ( Light childLight in windowLight.GetComponentsInChildren<Light> ( ) )
                {
                childLight.enabled = false;
                }
            }

        Vector3 tvec = Camera.main.transform.position;
        worldProbe.transform.position = tvec;

        water.material.mainTextureOffset = new Vector2 ( Time.time / 100, 0 );
        water.material.SetTextureOffset ( "_DetailAlbedoMap", new Vector2 ( 0, Time.time / 80 ) );

        }
    }
