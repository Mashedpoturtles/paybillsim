using UnityEngine;
using System.Collections;

public class StateConditions : MonoBehaviour
    {
    public int random_waitTimeBored;
    public int random_waitTimeSocial;
    public bool _Work;
    public bool _Social;
    public float workTimer;
    public static float maxDesireToWork = 15;
    public float triggerWorking = 5;
    public float socialTimer;
    public float triggerSocializing = 10;
    public static float maxDesireToSocialize = 20;
    [SerializeField]
    private Pathing path;
    [SerializeField]
    private DayNightControl dayNightControl;
    [SerializeField]
    private ParticleSystem ps;

    void Start ( )
        {
        InvokeRepeating ( "WorkDesireBuildup", 0, 1 );
        InvokeRepeating ( "SocialDesireBuildup", 0, 1 );
        var em = ps.emission;
        em.enabled = false;
        }

    public void WorkDesireBuildup ( )
        {
        if ( workTimer < maxDesireToWork )
            {
            workTimer++;
            }

        if ( workTimer == triggerWorking )
            {
            _Work = true;
            if ( !dayNightControl.IsNight )
                {
                path.MoveToRandom ( path.dayTimeTarget );
                }
            else
                {
                path.MoveToRandom ( path.nightTimeTarget );
                }
            }
        if ( workTimer == maxDesireToWork )
            {
            StartCoroutine ( ResetWorkDesire ( ) );
            }
        }

    public IEnumerator ResetWorkDesire ( )
        {
            {
            if ( workTimer == maxDesireToWork )
                {
                random_waitTimeBored = Random.Range ( 5, 10 );
                yield return new WaitForSeconds ( random_waitTimeBored );
                workTimer = 0;
                }
            }
        }

    public IEnumerator ResetSocialDesire ( )
        {
            {
            if ( socialTimer == maxDesireToSocialize )
                {
                random_waitTimeSocial = Random.Range ( 5, 10 );
                yield return new WaitForSeconds ( random_waitTimeSocial );
                socialTimer = 0;
                }
            }
        }

    public void SocialDesireBuildup ( )
        {
        if ( socialTimer < maxDesireToSocialize )
            {
            socialTimer++;
            }

        if ( socialTimer == triggerSocializing )
            {
            _Social = true;
            if ( !dayNightControl.IsNight )
                {
                path.MoveToRandom ( path.dayTimeTarget );
                }
            else
                {
                path.MoveToRandom ( path.nightTimeTarget );
                }
            }
        if ( socialTimer == maxDesireToSocialize )
            {
            StartCoroutine ( ResetSocialDesire ( ) );
            }
        }

    void OnTriggerEnter ( Collider other )
        {
        var em = ps.emission;
        if ( !dayNightControl.IsNight && this._Social == true && other.gameObject.GetComponent<StateConditions> ( )._Social == true )
            {
            transform.LookAt ( other.gameObject.transform );
            em.enabled = true;
            }
        else
            {
            em.enabled = false;
            return;
            }
        }
    void OnTriggerExit ( Collider other )
        {
        var em = ps.emission;
        em.enabled = false;
        }
    }
// 1400 / 40 = 35 weken  - looptijd = 38 weken (  28/09/2015 - 24/06-2016  )