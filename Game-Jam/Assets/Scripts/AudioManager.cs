using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioSource;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource[0].Play();
        }
    }
}
