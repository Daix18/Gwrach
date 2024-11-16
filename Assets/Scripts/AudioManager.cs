using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager THIS { get; private set; }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }
    [System.Serializable]
    public class Music
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
        [Range(0.1f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }
    AudioSource _audioSource;
    public Sound[] sonidos;
    public Music[] musica;

    private void Awake()
    {
        if (THIS != null && THIS != this)
        {
            Destroy(gameObject);
            return;
        }

        THIS = this;
    }

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();

        // Initialize sound sources
        foreach (var sound in sonidos)
        {
            _audioSource.name = sound.name;
            _audioSource.clip = sound.clip;
            _audioSource.volume = sound.volume;
            _audioSource.pitch = sound.pitch;
            _audioSource.loop = sound.loop;
        }

        // Initialize music sources
        foreach (var music in musica)
        {
            _audioSource.clip = music.clip;
            _audioSource.volume = music.volume;
            _audioSource.pitch = music.pitch;
            _audioSource.loop = music.loop;
        }
    }

    public void PlaySound(string soundName)
    {
        if (_audioSource.name == soundName)
        {
            Debug.Log(_audioSource.name);
            _audioSource.Play();
        }
    }
}
