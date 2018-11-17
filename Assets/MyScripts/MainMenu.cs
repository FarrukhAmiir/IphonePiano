using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    TestSound sounds;
    public int count = 0;
    public string mode;
    public GameObject starEffect,effects, completion, musicOn, MusicOff, SoundOn, SoundOff,MusicScreen,Next,Prev,AboutUs;
    // Use this for initialization
    void Start()
    {
       // sounds = GameObject.Find("SoundManager").GetComponent<TestSound>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick(string name)
    {
       
        if (name == "MusicOn")
        {
            sounds.MuteMusic("Intro Theme");
            MusicOff.SetActive(true);
            musicOn.SetActive(false);
        }

        else if (name == "MusicOff")
        {
            sounds.UnMuteMusic("Intro Theme");
            musicOn.SetActive(true);
            MusicOff.SetActive(false);
        }

        else if (name == "SoundOn")
        {
            sounds.MuteMusic("258269__jcallison__mouth-pop");
            sounds.MuteMusic("ClickSound");
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
        }

        else if (name == "SoundOff")
        {
            sounds.UnMuteMusic("258269__jcallison__mouth-pop");
            sounds.UnMuteMusic("ClickSound");
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
        else if (name == "Play")
        {

            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadPiano", 1f);

        }

        else if(name == "Sounds")
        {
            MusicScreen.SetActive(true);
            Next.SetActive(false);
            Prev.SetActive(false);
        }

        else if(name == "SoundsOff")
        {
            MusicScreen.SetActive(false);
            Next.SetActive(true);
            Prev.SetActive(true);
        }
        else if (name == "AboutOn")
        {
            AboutUs.SetActive(true);
            Next.SetActive(false);
            Prev.SetActive(false);
        }
        else if (name == "AboutOff")
        {
            AboutUs.SetActive(false);
            Next.SetActive(true);
            Prev.SetActive(true);
        }

        else if(name == "HappyBirthday")
        {
            mode = "HappyBirthday";
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadHappy", 1f);
        }
        else if (name == "JingleBells")
        {
            mode = "JingleBells";
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadJingles", 1f);    
        }

        else if (name == "OldMan")
        {
            mode = "OldMan";
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadOld", 1f);
        }
    }

    void LoadOld()
    {
        SceneManager.LoadScene(4);
    }

    void LoadJingles()
    {
        SceneManager.LoadScene(3);
    }

    void LoadHappy()
    {
        SceneManager.LoadScene(2);
    }

    void LoadPiano()
    {
        SceneManager.LoadScene(1);
    }
}
