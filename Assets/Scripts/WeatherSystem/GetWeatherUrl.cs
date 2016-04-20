using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.UI;

public class GetWeatherUrl : MonoBehaviour
    {

    private static string json;
    public static double dTemp;
    public static double dFeelTempC;
    public static string sWeatherCondition;
    public string sCountry;
    public string sCity;
    protected string newsfeed;
    [SerializeField]
    private GameObject gWeatherObject;
    public string url = "http://api.wunderground.com/api/d33ba8071e6bde8b/conditions/bestfct/q/autoip.json";
    public Text Weather;

    // Deserialize json format into a c# model format.

    public IEnumerator ServerCall ( )
        {
        while ( true )
            {
            WWW www = new WWW ( url );
            yield return www;

            json = www.text;

            var MainObservation = JsonConvert.DeserializeObject<WeatherClasses.MainObservation> ( json );
            if ( MainObservation != null && MainObservation.current_observation != null )
                {
                for ( int i = 0 ; i < MainObservation.current_observation.weather.Length ; i++ )
                    dTemp = MainObservation.current_observation.temp_c;
                dFeelTempC = MainObservation.current_observation.feelslike_c;
                sWeatherCondition = MainObservation.current_observation.weather;
                sCountry = MainObservation.current_observation.display_location.state_name;
                sCity = MainObservation.current_observation.display_location.city;

                WeatherListComplete WeatherCompareScript = gWeatherObject.GetComponent<WeatherListComplete> ( );
                if ( WeatherCompareScript == null )
                    WeatherCompareScript = gWeatherObject.AddComponent<WeatherListComplete> ( );

                WeatherCompareScript.WeatherEffects ( );

                //  Display of weather goes here.      
                    {
                    Weather.text = string.Format ( "Temperatuur {0}  Gevoels temperatuur {1} Uw huidige locatie {2}"
                        , dTemp, dFeelTempC, sCity );
                    }
                }
            yield break;
            }
        }
    public void Fetch ( )
        {
        StartCoroutine ( ServerCall ( ) );
        }
    public void Start ( )
        {
        InvokeRepeating ( "Fetch", 1, 120 );
        }
    }


