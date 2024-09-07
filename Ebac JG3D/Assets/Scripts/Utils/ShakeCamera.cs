using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public List<CinemachineVirtualCamera> virtualCameras; // Lista de câmeras
    public float shakeTime;
    [Header("Shake Values")]
    public float frequency = 3f;
    public float amplitude = 3f;
    public float time = .3f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        Shake(amplitude, frequency, time);
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        foreach (var camera in virtualCameras)
        {
            CinemachineBasicMultiChannelPerlin c = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (c != null)
            {
                c.m_AmplitudeGain = amplitude;
                c.m_FrequencyGain = frequency;
            }
        }
        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {
            foreach (var camera in virtualCameras)
            {
                CinemachineBasicMultiChannelPerlin c = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                if (c != null)
                {
                    c.m_AmplitudeGain = 0f;
                    c.m_FrequencyGain = 0f;
                }
            }
        }
    }
}
