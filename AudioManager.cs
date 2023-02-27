using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup defaultMixerGroup;
    [SerializeField] private AudioMixer mixer;

    public Sound[] sounds;

    private Sound engineSound;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.outputAudioMixerGroup = s.mixerGroup;

            s.source.outputAudioMixerGroup ??= defaultMixerGroup;
        }

        engineSound = FindSound("Engine Run");
    }

    public void Play(string sound)
    {
        Sound s = FindSound(sound);

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void StopSound(string sound) {
        Sound s = FindSound(sound);
        s.source.Stop();
    }

    public bool IsPlaying(string sound) {
        Sound s = FindSound(sound);
        return s.source.isPlaying;
    }

    private Sound FindSound(string name) {
        Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null) {
            Debug.LogWarning($"Sound: {name} not found!");
        }
        return s;
    }

    public void Mute() {
        mixer.SetFloat("volume", -80f);
    }

    public void Unmute() {
        mixer.SetFloat("volume", GameManager.instance.prefs.gameVolume);
    }

    public void AdjustEnginePitch(float pitch) {
        //pitch*=0.5f;
        engineSound.source.pitch = pitch;
    }

}
