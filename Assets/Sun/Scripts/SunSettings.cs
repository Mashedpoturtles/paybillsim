using UnityEngine;

public class SunSettings : MonoBehaviour
    {
    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.2f;

    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;

    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;

    public Vector3 dayRotateSpeed;
    public Vector3 nightRotateSpeed;

    float skySpeed = 1;

    [SerializeField]
    DayNightControl dayNightControl;


    Light mainLight;
    Skybox sky;
    Material skyMat;

    void Start ( )
        {
        mainLight = GetComponent<Light> ( );
        skyMat = RenderSettings.skybox;
        }

    void Update ( )
        {

        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01 ( ( Vector3.Dot ( mainLight.transform.forward, Vector3.down ) - minPoint ) / tRange );
        float i = ( ( maxIntensity - minIntensity ) * dot ) + minIntensity;

        mainLight.intensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01 ( ( Vector3.Dot ( mainLight.transform.forward, Vector3.down ) - minAmbientPoint ) / tRange );
        i = ( ( maxAmbient - minAmbient ) * dot ) + minAmbient;
        RenderSettings.ambientIntensity = i;
        RenderSettings.ambientLight = mainLight.color;

        i = ( ( dayAtmosphereThickness - nightAtmosphereThickness ) * dot ) + nightAtmosphereThickness;
        skyMat.SetFloat ( "_AtmosphereThickness", i );
        if ( dot > 0.4 )
            {
            dayNightControl.IsNight = false;
            }
        if ( dot > 0 )
            {
            transform.Rotate ( dayRotateSpeed * Time.deltaTime * skySpeed );
            }
        else
            {
            transform.Rotate ( nightRotateSpeed * Time.deltaTime * skySpeed );
            dayNightControl.IsNight = true;
            }
        }
    }
