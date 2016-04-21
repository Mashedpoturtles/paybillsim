using UnityEngine;
using System.Collections;

public class GlobalAudio : MonoBehaviour {

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _audioSourceMusic;
    [SerializeField]
    private AudioClip __soundStart;
    [SerializeField]
    private AudioClip __soundPause;
    [SerializeField]
    private AudioClip __soundAttention;
    [SerializeField]
    private AudioClip _soundWarning;
    [SerializeField]
    private AudioClip __soundMusic;
    [SerializeField]
    private AudioClip __soundGameOver;
    [SerializeField]
    private AudioClip _paperTear;

    public static GlobalAudio instance;
    private void Start()
        {
        _audioSource = GameObject.FindWithTag ( "Persistent" ).GetComponent<AudioSource> ( );
        _audioSourceMusic = GameObject.FindWithTag ( "Music" ).GetComponent<AudioSource> ( );
        instance = this;
        }
    public void SoundStart()
        {
        _audioSource.clip = __soundStart;
        _audioSource.Play ( );
        }
    public void SoundPaidBill()
        {
        _audioSource.clip = _paperTear;
        _audioSource.Play ( );
        }
	 public void SoundAttention()
        {
        _audioSource.clip = __soundAttention;
        _audioSource.Play ( );
        }
    public void SoundWarning()
        {
        _audioSource.clip = _soundWarning;
        _audioSource.Play ( );
        }
    public void SoundPause()
        {
        _audioSource.clip = __soundPause;
      _audioSource.Play ( );
        }
    public void SoundGameOver()
        {
        _audioSource.clip = __soundGameOver;
      _audioSource.Play ( );
        }
    public void SoundMusic()
        {
        _audioSourceMusic.clip = __soundMusic;
        _audioSourceMusic.Play ( );
        }
}
