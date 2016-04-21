using UnityEngine;
using UnityEngine.EventSystems;

public class StartGameSound : MonoBehaviour, IPointerDownHandler
    {
    [SerializeField]
    private GameObject buttons;
    [SerializeField]
    private AudioClip _startGameAudio;
    [SerializeField]
    private AudioSource _audioSource;

    private void Start ( )
        {
        _audioSource = GameObject.FindWithTag ( "Persistent" ).GetComponent<AudioSource> ( );
        }

    public void OnPointerDown ( PointerEventData eventData )
        {
        _audioSource.clip = _startGameAudio;
        _audioSource.Play ( );
        }
    }