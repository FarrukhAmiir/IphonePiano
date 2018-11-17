using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    private AudioSource source;

    public string Clipname;
    public AudioClip clip;

    public float volume;
    public float pitch;

    public bool mute;
    public bool loop;
    public bool playOnAwake;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.mute = mute;
        source.loop = loop;
        source.playOnAwake = playOnAwake;
    }
    public void Play()
    {
        source.Play();
    }

}

public class TestSound : MonoBehaviour {

    public static TestSound instance;
    float volumeSounds;
    public Sounds[] sounds;

    void Awake()
    {

        //PlayerPrefs.DeleteAll ();
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {
        Sounds obj = new Sounds();
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject Soundsclips = new GameObject("Sounds" + i + "" + sounds[i].Clipname);
            Soundsclips.transform.SetParent(this.transform);
            sounds[i].SetSource(Soundsclips.AddComponent<AudioSource>());
        }
        //	Debug.Log ("length" + sounds.Length);
        //if (PlayerPrefs.GetInt ("sounds", 1) == 1) {
        //	UnMuteMusic ("sounds");
        //} else 
        //{
        //	MuteMusic ("sounds");
        //}

        //if (PlayerPrefs.GetInt ("music", 1) == 1) {
        //	UnMuteMusic ("music_background");
        //} else 
        //{
        //	MuteMusic ("music_background");
        //}

        PlaySounds("Intro Theme");
    }

    public void PlaySounds(string name)
    {
        //		Debug.Log (name);
        for (int i = 0; i < sounds.Length; i++)
        {
            if (i > 0)
            {
                if (sounds[i].Clipname == name)
                {
                    Debug.Log("Souds pop");
                    sounds[i].Play();

                    return;
                }
            }
            else
            {
                if (sounds[i].Clipname == name)
                {
                    Debug.Log("Souds True");
                    sounds[i].Play();
                    return;
                }
            }
        }
    }

    public void ClickSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {

            if (sounds[i].Clipname == name)
            {
                Debug.Log("Souds click");
                sounds[i].Play();
                return;
            }

        }
    }


    public void MuteMusic(string _name)
    {
        if (_name == "Intro Theme")
        {
            gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            Debug.Log("mute");
            PlayerPrefs.SetInt("music", 0);
        }
        else
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<AudioSource>().mute = true;
            }
            PlayerPrefs.SetInt("sounds", 0);
        }
    }
    public void UnMuteMusic(string _name)
    {
        if (_name == "Intro Theme")
        {
            gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
            Debug.Log("unmute");
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            for (int i = 1; i < sounds.Length; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<AudioSource>().mute = false;
            }
            PlayerPrefs.SetInt("sounds", 1);
        }
    }
}
