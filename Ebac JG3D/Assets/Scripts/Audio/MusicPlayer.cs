using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;

    public AudioSource audioSource;
    private MusicSetup _currentMusicSetup;

    private void Start() {
        
    }
    
    private void Play()
    {
        _currentMusicSetup = SoundManager.Instance.GetMusicBType(musicType);

        audioSource.clip = _currentMusicSetup.audioClip;
        audioSource.Play();
    }
}
