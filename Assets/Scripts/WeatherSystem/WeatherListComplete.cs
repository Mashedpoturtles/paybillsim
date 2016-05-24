using UnityEngine;
using System;
using DigitalRuby.RainMaker;


public class WeatherListComplete : MonoBehaviour
    {
    [SerializeField]
    private Animator cloudanim;
    void Start ( )
        {
        cloudanim.GetComponent<Animator> ( );
        }

    public void WeatherEffects ( )
        {
        Application.runInBackground = true;
        RainScript baserainscript = GameObject.FindWithTag ( "BaseRain" ).GetComponent<RainScript> ( );


        weatherConditions weather = ( weatherConditions ) Enum.Parse ( typeof ( weatherConditions ), GetWeatherUrl.sWeatherCondition.Replace ( " ", "" ) );
        if ( weather == weatherConditions.Cloudy ||
             weather == weatherConditions.PartlyCloudy ||
             weather == weatherConditions.MostlyCloudy ||
             weather == weatherConditions.ScatteredClouds ||
             weather == weatherConditions.Rain ||
             weather == weatherConditions.RainShowers ||
             weather == weatherConditions.LightDrizzle )
            {
            cloudanim.SetBool ( "IsCloudy", true );
            }
        else
            {
            cloudanim.SetBool ( "IsCloudy", false );
            }

        switch ( weather )
            {
            case weatherConditions.LightDrizzle:
                baserainscript.RainIntensity = 0.1f;
                break;
            case weatherConditions.HeavyDrizzle:
                baserainscript.RainIntensity = 0.3f;
                break;
            case weatherConditions.LightRain:
                baserainscript.RainIntensity = 0.4f;
                break;
            case weatherConditions.HeavyRain:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.LightSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSnowGrains:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySnowGrains:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.SnowGrains:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightIceCrystals:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.IceCrystals:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyIceCrystals:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.IcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightIcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyIcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Hail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightMist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Mist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyMist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightFogPatches:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.FogPatches:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyFogPatches:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSmoke:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Smoke:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySmoke:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.VolcanicAsh:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightVolcanicAsh:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyVolcanicAsh:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.WidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Sand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightHaze:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyHaze:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSpray:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Spray:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySpray:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightDustWhirls:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.DustWhirls:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyDustWhirls:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSandstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Sandstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySandstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightLowDriftingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LowDriftingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyLowDriftingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightLowDriftingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LowDriftingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyLowDriftingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightLowDriftingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LowDriftingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyLowDriftingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightBlowingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.BlowingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyBlowingSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightBlowingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.BlowingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyBlowingWidespreadDust:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightBlowingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.BlowingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyBlowingSand:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightRainMist:
                baserainscript.RainIntensity = 0.1f;
                break;
            case weatherConditions.RainMist:
                baserainscript.RainIntensity = 0.3f;
                break;
            case weatherConditions.HeavyRainMist:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.LightRainShowers:
                baserainscript.RainIntensity = 4f;
                break;
            case weatherConditions.RainShowers:
                baserainscript.RainIntensity = 0.6f;
                break;
            case weatherConditions.HeavyRainShowers:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.SnowShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSnowShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySnowShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.SnowBlowingSnowMist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSnowBlowingSnowMist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySnowBlowingSnowMist:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightIcePelletShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.IcePelletShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyIcePelletShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightHailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyHailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.SmallHailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightSmallHailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavySmallHailShowers:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyThunderstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstormsandRain:
                baserainscript.RainIntensity = 0.4f;
                break;
            case weatherConditions.ThunderstormsandRain:
                baserainscript.RainIntensity = 0.6f;
                break;
            case weatherConditions.HeavyThunderstormsandRain:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.ThunderstormsandSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstormsandSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyThunderstormsandSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ThunderstormsandIcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstormsandIcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyThunderstormsandIcePellets:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstormswithHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ThunderstormswithHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyThunderstormswithHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightThunderstormswithSmallHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ThunderstormswithSmallHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyThunderstormswithSmallHail:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightFreezingDrizzle:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.FreezingDrizzle:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyFreezingDrizzle:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightFreezingRain:
                baserainscript.RainIntensity = 0.1f;
                break;
            case weatherConditions.FreezingRain:
                baserainscript.RainIntensity = 0.5f;
                break;
            case weatherConditions.HeavyFreezingRain:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.FreezingFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.LightFreezingFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.HeavyFreezingFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.PatchesofFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ShallowFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.PartialFog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Overcast:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Clear:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.PartlyCloudy:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.MostlyCloudy:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ScatteredClouds:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.SmallHail:
                baserainscript.RainIntensity = 1f;
                break;
            case weatherConditions.Squalls:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.FunnelCloud:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.chanceOfFlurries:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ChanceofRain:
                baserainscript.RainIntensity = 0.3f;
                break;
            case weatherConditions.ChanceRain:
                baserainscript.RainIntensity = 0.3f;
                break;
            case weatherConditions.ChanceOfFreezing:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ChanceOfSleet:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ChanceOfSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ChanceOfThunderstorms:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Cloudy:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.VeryLightSnow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Fog:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Haze:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.MostlySunny:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.PartlySunny:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Freezing:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Rain:
                baserainscript.RainIntensity = 0.5f;
                break;
            case weatherConditions.Sleet:
                baserainscript.RainIntensity = 0.1f;
                break;
            case weatherConditions.Snow:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Sunny:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Thunderstorms:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Thunderstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.UnknownPrecipitation:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Unknown:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.ChanceOfAThunderstorm:
                baserainscript.RainIntensity = 0f;
                break;
            case weatherConditions.Drizzle:
                baserainscript.RainIntensity = 0.2f;
                break;
            default:
                Debug.Log ( "Weather conditions do not match weathercondition list." );
                break;
            }
        }
    }

[Serializable]
public enum weatherConditions
    {
    LightDrizzle,
    Drizzle,
    HeavyDrizzle,
    LightRain,
    HeavyRain,
    LightSnow,
    HeavySnow,
    LightSnowGrains,
    SnowGrains,
    HeavySnowGrains,
    LightIceCrystals,
    HeavyIceCrystals,
    IceCrystals,
    LightIcePellets,
    IcePellets,
    HeavyIcePellets,
    LightHail,
    Hail,
    HeavyHail,
    LightMist,
    Mist,
    HeavyMist,
    LightFog,
    HeavyFog,
    LightFogPatches,
    FogPatches,
    HeavyFogPatches,
    LightSmoke,
    Smoke,
    HeavySmoke,
    LightVolcanicAsh,
    VolcanicAsh,
    HeavyVolcanicAsh,
    LightWidespreadDust,
    WidespreadDust,
    HeavyWidespreadDust,
    LightSand,
    Sand,
    HeavySand,
    LightHaze,
    HeavyHaze,
    LightSpray,
    Spray,
    HeavySpray,
    LightDustWhirls,
    DustWhirls,
    HeavyDustWhirls,
    LightSandstorm,
    Sandstorm,
    HeavySandstorm,
    LightLowDriftingSnow,
    LowDriftingSnow,
    HeavyLowDriftingSnow,
    LightLowDriftingWidespreadDust,
    LowDriftingWidespreadDust,
    HeavyLowDriftingWidespreadDust,
    LightLowDriftingSand,
    LowDriftingSand,
    HeavyLowDriftingSand,
    LightBlowingSnow,
    BlowingSnow,
    HeavyBlowingSnow,
    LightBlowingWidespreadDust,
    BlowingWidespreadDust,
    HeavyBlowingWidespreadDust,
    LightBlowingSand,
    BlowingSand,
    HeavyBlowingSand,
    LightRainMist,
    RainMist,
    HeavyRainMist,
    LightRainShowers,
    RainShowers,
    HeavyRainShowers,
    LightSnowShowers,
    SnowShowers,
    HeavySnowShowers,
    LightSnowBlowingSnowMist,
    SnowBlowingSnowMist,
    HeavySnowBlowingSnowMist,
    LightIcePelletShowers,
    IcePelletShowers,
    HeavyIcePelletShowers,
    LightHailShowers,
    HailShowers,
    HeavyHailShowers,
    LightSmallHailShowers,
    SmallHailShowers,
    HeavySmallHailShowers,
    LightThunderstorm,
    HeavyThunderstorm,
    LightThunderstormsandRain,
    ThunderstormsandRain,
    HeavyThunderstormsandRain,
    LightThunderstormsandSnow,
    ThunderstormsandSnow,
    HeavyThunderstormsandSnow,
    LightThunderstormsandIcePellets,
    ThunderstormsandIcePellets,
    HeavyThunderstormsandIcePellets,
    LightThunderstormswithHail,
    ThunderstormswithHail,
    HeavyThunderstormswithHail,
    LightThunderstormswithSmallHail,
    ThunderstormswithSmallHail,
    HeavyThunderstormswithSmallHail,
    LightFreezingDrizzle,
    FreezingDrizzle,
    HeavyFreezingDrizzle,
    LightFreezingRain,
    FreezingRain,
    HeavyFreezingRain,
    LightFreezingFog,
    FreezingFog,
    HeavyFreezingFog,
    PatchesofFog,
    ShallowFog,
    PartialFog,
    Overcast,
    Clear,
    PartlyCloudy,
    MostlyCloudy,
    ScatteredClouds,
    SmallHail,
    Squalls,
    FunnelCloud,
    chanceOfFlurries,
    ChanceofRain,
    ChanceRain,
    ChanceOfFreezing,
    ChanceOfSleet,
    ChanceOfSnow,
    ChanceOfThunderstorms,
    ChanceOfAThunderstorm,
    Cloudy,
    VeryLightSnow,
    Fog,
    Haze,
    MostlySunny,
    PartlySunny,
    Freezing,
    Rain,
    Sleet,
    Snow,
    Sunny,
    Thunderstorms,
    Thunderstorm,
    UnknownPrecipitation,
    Unknown
    }

