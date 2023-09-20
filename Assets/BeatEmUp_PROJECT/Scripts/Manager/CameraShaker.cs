using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin p;
    float amp, t;
    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        p = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amplitude, float time)
    {
        amp = amplitude;
        t = time;
    }

    private void Update()
    {
        if (t > 0)
        {
            p.m_AmplitudeGain = amp;
            t -= 1 * Time.deltaTime;
        }
        else
        {
            t = 0;
            amp = 0;
            p.m_AmplitudeGain = 0;
        }
    }
}
