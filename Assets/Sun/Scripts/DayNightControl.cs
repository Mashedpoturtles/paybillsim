using UnityEngine;
using System.Collections;
using UnityStandardAssets.CinematicEffects;

public class DayNightControl : MonoBehaviour
    {
    public GameObject windowLight;
    public Renderer water;
    public Transform stars;
    public Transform worldProbe;
    public GameObject Clouds;

    [SerializeField]
    private float bloomMaximumIntensity = 1.5f;
    [SerializeField]
    private float bloomMinimumIntensity = 0.7f;
    [SerializeField]
    private float timeParam = 0;
    [SerializeField]
    private float bloomLerpSpeed = 0.005f;
    [SerializeField]
    Bloom bloomScript;

    private bool m_isNight = false;
    public bool IsNight
        {
        get { return m_isNight; }
        set
            {
            m_isNight = value;
            }
        }

    void Update ( )
        {
        stars.transform.rotation = transform.rotation;
        if ( IsNight )
            {
            Clouds.SetActive ( false );
            foreach ( Renderer childRenderer in windowLight.GetComponentsInChildren<Renderer> ( ) )
                {
                Color final = childRenderer.material.color * Mathf.LinearToGammaSpace ( 5 );
                childRenderer.material.SetColor ( "_EmissionColor", final );
                DynamicGI.SetEmissive ( childRenderer, final );
                bloomScript.settings.radius = 1.5f;
                timeParam = 0;
                if ( timeParam < 1 )
                    {
                    timeParam += Time.deltaTime * bloomLerpSpeed;
                    bloomScript.settings.intensity = Mathf.Lerp ( bloomScript.settings.intensity, bloomMaximumIntensity, timeParam );
                    }
                }
            foreach ( Light childLight in windowLight.GetComponentsInChildren<Light> ( ) )
                {
                childLight.enabled = true;
                }
            }
        else if ( !IsNight )
            {
            Clouds.SetActive ( true );
            foreach ( Renderer childRenderer in windowLight.GetComponentsInChildren<Renderer> ( ) )
                {
                Color final = childRenderer.material.color * Mathf.LinearToGammaSpace ( 0.1f );
                childRenderer.material.SetColor ( "_EmissionColor", final );
                DynamicGI.SetEmissive ( childRenderer, final );
                bloomScript.settings.radius = 1;
                timeParam = 0;
                if ( timeParam < 1 )
                    {
                    timeParam += Time.deltaTime * bloomLerpSpeed;
                    bloomScript.settings.intensity = Mathf.Lerp ( bloomScript.settings.intensity, bloomMinimumIntensity, timeParam );
                    }
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
