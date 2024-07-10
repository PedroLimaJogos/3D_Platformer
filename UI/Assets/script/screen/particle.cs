using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    public void Onclick()
    {
        particleSystem.Play();
    }
}
