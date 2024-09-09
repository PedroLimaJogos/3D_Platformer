using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sFXSetups;

    public AudioSource musicSource;
    public AudioSource sfxSource; // Fonte de áudio para os efeitos sonoros
    private bool isMuted = false; // Estado de mutado

    private void Update()
    {
        // Verifica se a tecla 'M' foi pressionada
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute(); // Alterna o estado de mute quando "M" for pressionado
        }
    }

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicBType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicBType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }
    
    public SFXSetup GetSFXBType(SFXType sFXType)
    {
        return sFXSetups.Find(i => i.sfxType == sFXType);
    }

    // Alterna entre mutar e desmutar o som
    public void ToggleMute()
    {
        isMuted = !isMuted; // Alterna o estado de mutado

        // Define o volume para 0 quando mutado, ou para o volume original quando desmutado
        musicSource.mute = isMuted;
        sfxSource.mute = isMuted;

        Debug.Log(isMuted ? "Som Mutado" : "Som Desmutado");
    }
}


public enum MusicType
{
    TYPE_01,
    TYPE_03,
    TYPE_02
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}
public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_03,
    TYPE_02
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}

