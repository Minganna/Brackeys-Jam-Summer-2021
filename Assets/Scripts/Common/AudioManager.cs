using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public List<AudioClip> Music=new List<AudioClip>();
    public List<AudioClip> sfx = new List<AudioClip>();

    public int levelMusicToPlay;

    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SfxMixer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int musicToPlay)
    {
        AudioSource[] Jukebox = GetComponentsInChildren<AudioSource>();
        if (Jukebox[0])
        {
            if(musicToPlay<Music.Count)
            {
                Jukebox[0].clip = Music[musicToPlay];
                Jukebox[0].Play();
            }
        }    
    }

    public void Sfx(int SfxToPlay)
    {
        AudioSource[] Jukebox = GetComponentsInChildren<AudioSource>();
        if (Jukebox[1])
        {
            if (SfxToPlay < sfx.Count)
            {
                Jukebox[1].clip = sfx[SfxToPlay];
                Jukebox[1].Play();
            }
        }
    }

    public void SetMusicLevel()
    {
        MusicMixer.audioMixer.SetFloat("MusicVol", UiManager.instance.MusicVolSlider.value);
    }
    public void SetSfxLevel()
    {
        SfxMixer.audioMixer.SetFloat("SfxVol", UiManager.instance.SfxVolSlider.value);
    }
}

