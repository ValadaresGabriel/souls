using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct GameAudio
{
    public string name;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string sceneName;

    [Header("UI Sound")]
    [SerializeField] private AudioClip buttonsClip;

    [SerializeField] private AudioClip playAndDialogClip;

    [SerializeField] private AudioSource menuAudioSource;

    [Header("Game Sound")]
    [SerializeField] private GameAudio[] gameAudios;

    [SerializeField] private AudioSource gameAudioSource;

    private Dictionary<string, AudioClip> dictGameAudios = new Dictionary<string, AudioClip>();

    public AudioSource[] audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (gameAudios != null)
        {
            foreach (var audio in gameAudios)
            {
                dictGameAudios.Add(audio.name, audio.audioClip);
            }
        }
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        SetSceneName(sceneName);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource[0].Play();
        }

        if (menuAudioSource.clip != null && menuAudioSource.isPlaying == false)
        {
            menuAudioSource.clip = null;
        }
    }

    private void SetSceneName(string sceneName)
    {
        if (this.sceneName != sceneName)
        {
            this.sceneName = sceneName;
            gameAudioSource.clip = dictGameAudios[sceneName];
            gameAudioSource.Play();
        }
    }

    public void PlayButtonSoundButtonSound()
    {
        menuAudioSource.clip = buttonsClip;
        menuAudioSource.Play();
    }

    public void PlayPlayAndDialogButtonSound()
    {
        menuAudioSource.clip = playAndDialogClip;
        menuAudioSource.Play();
    }
}
