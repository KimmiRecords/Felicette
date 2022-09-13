using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //todo lo pertinente a sonidos y sus metodos
    //por diego katabian

    public static AudioManager instance;
    bool _soundOn = true;

    public bool SoundOn
    {
        get
        {
            return _soundOn;
        }
        set
        {
            _soundOn = value;
            if (_soundOn)
            {
                UnmuteAll();
            }
            else
            {
                MuteAll();
            }
        }
    }

    AudioSource[] allSounds;

    [HideInInspector]
    public Dictionary<string, AudioSource> sound = new Dictionary<string, AudioSource>();

    public string thisLevelBgm;


    void Awake()
    {
        if (instance) //esto es para que audiomanager sea unico. puse uno en cada escena, pero a traves de las escenas se mantiene vivo uno solo.
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);

        allSounds = GetComponentsInChildren<AudioSource>(); //construyo mi array con todos los audiosource

        for (int i = 0; i < allSounds.Length; i++) //lleno el diccionario de pares string-audiosource
        {
            string s = allSounds[i].ToString(); //convierto a string
            s = s.Substring(0, s.Length - 26); //formateo para que no me quede (UnityEngine.AudioSource) en cada nombre
            sound.Add(s, allSounds[i]);
            //print("agregue el key " + s + " con value " + allSounds[i]+ " al diccionario");
        }
    }

    public void PlayByName(string clipName) //el mas groso. le das el string y te da play a ese audio. muy global y sencillo.
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.Play();
    }
    public void StopByName(string clipName)
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.Stop();
    }
    public void PlayByNamePitch(string clipName, float pitch)
    {
        AudioSource sound;
        sound = this.sound[clipName];
        sound.pitch = pitch;
        sound.Play();
    }

    public void PlayBGM()
    {
        print("reproduje el sonido " + thisLevelBgm);
        sound[thisLevelBgm].Play();
    }
    public void StopBGM()
    {
        sound[thisLevelBgm].Stop();
    }
    public void FadeInBGM(float fadetime)
    {
        float timer = Time.time / fadetime;
        sound[thisLevelBgm].volume = Mathf.Lerp(0, 1, timer);
    }
    public void FadeOutBGM(float fadetime)
    {
        float timer = Time.time / fadetime;
        sound[thisLevelBgm].volume = Mathf.Lerp(1, 0, timer);
    }

    public void StopAll()
    {
        for (int i = 0; i < allSounds.Length; i++)
        {
            allSounds[i].Stop();
        }
    }

    public void MuteAll()
    {
        for (int i = 0; i < allSounds.Length; i++)
        {
            allSounds[i].mute = true;
        }
    }
    public void UnmuteAll()
    {
        for (int i = 0; i < allSounds.Length; i++)
        {
            allSounds[i].mute = false;
        }
    }
}
