using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update

    [SerializeField]
    LevelsData levelsData;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {
            var isMusic = s.isMusic;
            if (levelsData.musicOn && isMusic)
                s.source.Play();
            if (levelsData.soundOn && !isMusic)
                s.source.Play();
        }
    }

    public void ModifySoundParams(string name, float volume, bool loop)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = volume;
        s.source.loop = loop;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s != null)
            s.source.Stop();
    }

    public void StopAll()
    {
        foreach (var sound in sounds)
        {
            if (!levelsData.musicOn && sound.isMusic)
            {
                sound.source.Stop();
            }
            if (!levelsData.soundOn && !sound.isMusic)
            {
                sound.source.Stop();
            }
        }
    }

    public void ManageSceneSounds(string sceneName)
    {
        Play("buttonClick");
        if (levelsData.musicOn)
            Play(sceneName + "Music");
        else
            Stop(sceneName + "Music");

        if(levelsData.soundOn)
            Play(sceneName + "Sound");
        else
            Stop(sceneName + "Sound");

    }
}