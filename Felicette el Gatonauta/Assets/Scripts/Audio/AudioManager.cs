using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    AudioSource[] _allSounds;

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

        _allSounds = GetComponentsInChildren<AudioSource>(); //construyo mi array con todos los audiosource

        for (int i = 0; i < _allSounds.Length; i++) //lleno el diccionario de pares string-audiosource
        {
            string s = _allSounds[i].ToString(); //convierto a string
            s = s.Substring(0, s.Length - 26); //formateo para que no me quede (UnityEngine.AudioSource) en cada nombre
            sound.Add(s, _allSounds[i]);
            //print("agregue el key " + s + " con value " + allSounds[i]+ " al diccionario");
        }

        //EventManager.Subscribe(Evento.GoToSceneButtonUp, ChangeBGM);

    }

    //private void ChangeBGM(params object[] parameters)
    //{
    //    switch ((string)(parameters[0]))
    //    {
    //        case "Tienda":
    //            StopByName("PetSocMenu");
    //            PlayByName("PetSocShop");
    //            break;
    //        //case "MainMenu":
    //        //    StopByName("SpringWaltzLoop");
    //        //    StopByName("PetSocShop");
    //        //    PlayByName("PetSocMenu");
    //        //    break;
    //        case "Tutorial":
    //            StopByName("PetSocMenu");
    //            break;
    //        default:
    //            break;
    //    }
    //}

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
        sound = this.sound[clipName]; //establezco que voy a estar laburando con el audio cuyo nombre es clipname

        float originalPitch = sound.pitch; //pido el pitch original y lo guardo

        sound.pitch = pitch; //cambio al pitch deseado
        sound.Play(); //doy play
        StartCoroutine(SetPitchToOriginal(sound, originalPitch)); //le vuelvo a poner el pitch que tenia antes
    }

    public IEnumerator SetPitchToOriginal(AudioSource sound, float originalPitch)
    {
        while (sound.isPlaying)
        {
            yield return null;
        }

        sound.pitch = originalPitch; 
    }

    public void PlayBGM()
    {
        //print("reproduje el sonido " + thisLevelBgm);
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
        for (int i = 0; i < _allSounds.Length; i++)
        {
            _allSounds[i].Stop();
        }
    }

    public void MuteAll()
    {
        for (int i = 0; i < _allSounds.Length; i++)
        {
            _allSounds[i].mute = true;
        }
    }
    public void UnmuteAll()
    {
        for (int i = 0; i < _allSounds.Length; i++)
        {
            _allSounds[i].mute = false;
        }
    }
}
