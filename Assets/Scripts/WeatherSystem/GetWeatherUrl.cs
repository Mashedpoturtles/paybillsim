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
    public Text Temperature;
    public Text FeelTemperature;
    public Text CurrentCityLocation;

    // Deserialize json format into a c# model format.

    public IEnumerator ServerCall ( )
        {

        while ( true )
            {
            WWW www = new WWW ( url );
            yield return www;
            if ( www.error == null )
                {
                print ( "Successfully retrieved weather conditions." );
                }
            else
                {
                print ( "Something is wrong with: " + www.text + "." );
                }
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
                    Debug.Log ( "Current temp : " + dTemp + "°C ." );
                    Debug.Log ( "Feels like  " + dFeelTempC + "°C ." );
                    Debug.Log ( "Weather condition: " + sWeatherCondition + "." );
                    Debug.Log ( "Hello good people from the country " + sCountry + "." );
                    Debug.Log ( "You are currently located in the city " + sCity + "." );
                    Temperature.text = "Het is momenteel : " + dTemp + " °C";
                    FeelTemperature.text = "Het voelt buiten aan als : " + dFeelTempC + " °C .";
                    CurrentCityLocation.text = "Welkom debiteur in " + sCity + "!";

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
        InvokeRepeating ( "Fetch", 30, 120 );
        }
    }


