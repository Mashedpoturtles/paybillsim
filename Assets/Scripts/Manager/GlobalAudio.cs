using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Audio manager to call all soundclips in this project from.
/// </summary>
public class GlobalAudio : MonoBehaviour
    {
    [SerializeField]
    private AudioSource _audioSource_ButtonPressed;
    [SerializeField]
    private AudioSource _audioSource_ButtonHover;
    [SerializeField]
    private AudioSource _audioSource_Music;
    [SerializeField]
    private AudioSource _audioSource_Voice;
    [SerializeField]
    private AudioSource _audioSource_SFX;
    [SerializeField]
    private AudioClip __soundStart;
    [SerializeField]
    private AudioClip __soundPause;
    [SerializeField]
    private AudioClip __soundAttention;
    [SerializeField]
    private AudioClip _soundWarning;
    [SerializeField]
    private AudioClip [ ] _soundMusic;
    [SerializeField]
    private AudioClip [ ] sound_SFXThunder;
    [SerializeField]
    private AudioClip __soundGameOver;
    [SerializeField]
    private AudioClip _soundPaperTear;
    [SerializeField]
    private AudioClip __soundHoverAudio;
    [SerializeField]
    private AudioClip _soundConfirmation;
    [SerializeField]
    private AudioClip _soundbillPending;
    [SerializeField]
    private Text currentTrack;

    public static GlobalAudio instance;
    void Start ( )
        {
        _audioSource_ButtonPressed = GameObject.FindWithTag ( "Audio_Button_Pressed" ).GetComponent<AudioSource> ( );
        _audioSource_ButtonHover = GameObject.FindWithTag ( "Audio_Button_Hover" ).GetComponent<AudioSource> ( );
        _audioSource_Music = GameObject.FindWithTag ( "Audio_Music" ).GetComponent<AudioSource> ( );
        _audioSource_Voice = GameObject.FindWithTag ( "Audio_Voice" ).GetComponent<AudioSource> ( );

        instance = this;
        }
    void Awake ( )
        {
        _audioSource_SFX = GameObject.FindWithTag ( "Audio_Sfx" ).GetComponent<AudioSource> ( );
        }

    void Update ( )
        {
        if ( !_audioSource_Music.isPlaying )
            SoundMusic ( );
        if ( _audioSource_Music.clip != null )
            currentTrack.text = ( "Music track currently Playing: " + _audioSource_Music.clip.name );
        }

    public void SoundConfirm ( )
        {
        _audioSource_ButtonPressed.clip = _soundConfirmation;
        _audioSource_ButtonPressed.Play ( );
        }

    public void SoundStart ( )
        {
        _audioSource_Voice.clip = __soundStart;
        _audioSource_Voice.Play ( );
        }

    public void SoundPaidBill ( )
        {
        _audioSource_SFX.clip = _soundPaperTear;
        _audioSource_SFX.Play ( );
        }

    public void SoundBillPending ( )
        {
        _audioSource_SFX.clip = _soundbillPending;
        _audioSource_SFX.Play ( );
        }

    public void SoundAttention ( )
        {
        _audioSource_SFX.clip = __soundAttention;
        _audioSource_SFX.Play ( );
        }

    public void SoundWarning ( )
        {
        _audioSource_SFX.clip = _soundWarning;
        _audioSource_SFX.Play ( );
        }

    public void SoundPause ( )
        {
        _audioSource_Voice.clip = __soundPause;
        _audioSource_Voice.Play ( );
        }

    public void SoundGameOver ( )
        {
        _audioSource_Voice.clip = __soundGameOver;
        _audioSource_Voice.Play ( );
        }

    public void SoundHover ( )
        {
        _audioSource_ButtonHover.clip = __soundHoverAudio;
        _audioSource_ButtonHover.Play ( );
        }

    public void PlaySFX ( SFXType sfxType )
        {
        switch ( sfxType )
            {
            case SFXType.sfxThunder:
                _audioSource_SFX.clip = sound_SFXThunder [ Random.Range ( 0, sound_SFXThunder.Length ) ];
                break;
            }
        _audioSource_SFX.Play ( );
        }

    public void SoundMusic ( )
        {
        _audioSource_Music.clip = _soundMusic [ Random.Range ( 0, _soundMusic.Length ) ];
        _audioSource_Music.Play ( );
        }
    }
public enum SFXType
    {
    sfxThunder
    }

