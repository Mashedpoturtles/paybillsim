using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private AudioClip __hoverAudio;
    [SerializeField]
    private AudioSource _audioSource;
    private bool _soundPlayed = false;


    private void Start ( )
        {
        _audioSource = GameObject.FindWithTag ( "Persistent" ).GetComponent<AudioSource> ( );
        }

    void IPointerEnterHandler.OnPointerEnter ( PointerEventData eventData )
        {
        if ( !_soundPlayed )
            _audioSource.clip = __hoverAudio;
        _audioSource.Play ( );
        _soundPlayed = true;
        }

    public void OnPointerExit ( PointerEventData eventData )
        {
        _soundPlayed = false;
        }
    }
