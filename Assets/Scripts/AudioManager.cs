using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public static AudioManager _instance;

    private void Awake() {
        // Singelton handling
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        // Create audio sources for each sound
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound not found");
            return;
        }
        sound.source.Play();
    }
}
