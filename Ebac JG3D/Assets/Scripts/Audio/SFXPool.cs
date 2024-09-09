using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private bool isMuted = false; // Estado de mutado


    private int _index = 0;

    private void Start() {
        CreatePool();
    }
    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        for(int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        } 
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sFXType)
    {
        if(sFXType == SFXType.NONE) return;
        var sfx = SoundManager.Instance.GetSFXBType(sFXType);
        _audioSourceList[_index].clip = sfx.audioClip;
        _audioSourceList[_index].Play();

        _index++;
        if(_index >= _audioSourceList.Count) _index = 0;
    }

    private void Update()
    {
        // Verifica se a tecla 'M' foi pressionada
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute(); // Alterna o estado de mute quando "M" for pressionado
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted; // Alterna o estado de mutado

        // Define o volume para 0 quando mutado, ou para o volume original quando desmutado
        for(int i = 0; i < poolSize; i++)
        {
            _audioSourceList[i].mute = isMuted;
        }

        Debug.Log(isMuted ? "Som Mutado" : "Som Desmutado");
    }
}
