using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


    AudioSource audio;
    MainMenu menu;
   TestSound sound;
    bool flag = true;
    // Use this for initialization
    private void Awake()
    {
        
        menu = GameObject.Find("ModeSelectionMenu").GetComponent<MainMenu>();
    }
    void Start () {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menu.mode == "OldMan")
        {
            if (transform.localPosition.y <= -400f)
            {
                Destroy(this.gameObject);
                menu.count++;
                
                if (menu.count >= 59)
                {
                    menu.starEffect.SetActive(false);
                    StartCoroutine(OnComplete());
                    Destroy(this.gameObject);
                }
            }
        }

        else if (menu.mode == "HappyBirthday")
        {
            if (transform.localPosition.y <= -400f)
            {
                Destroy(this.gameObject);
                menu.count++;
                
                if (menu.count >= 50)
                {
                    menu.starEffect.SetActive(false);
                    StartCoroutine(OnComplete());
                    Destroy(this.gameObject);
                }
            }
        }

        else if (menu.mode == "JingleBells")
        {
            if (transform.localPosition.y <= -400f)
            {
                Destroy(this.gameObject);
                menu.count++;
                
                if (menu.count >= 97)
                {
                    menu.starEffect.SetActive(false);
                    StartCoroutine(OnComplete());
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void OnNextClick()
    {
        if (menu.mode == "JingleBells")
        {
            if (transform.localPosition.y <= -200f && transform.localPosition.y >= -280f && flag)
            {
                flag = false;
                StartCoroutine(starEffects());
            }
        }
        else if (menu.mode == "HappyBirthday")
        {
            if (transform.localPosition.y <= -200f && transform.localPosition.y >= -280f && flag)
            {
                flag = false;
                StartCoroutine(starEffects());
            }
        }

        else if (menu.mode == "OldMan")
        {
            if (transform.localPosition.y <= -200f && transform.localPosition.y >= -280f && flag)
            {
                flag = false;
                StartCoroutine(starEffects());
            }
        }

    }
    IEnumerator starEffects()
    {
        
        this.GetComponent<Image>().enabled = false;
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        if (menu.mode == "JingleBells")
        {
            if (menu.count >= 96)
            {
                Destroy(this.gameObject);
                menu.starEffect.SetActive(false);
                StartCoroutine(OnComplete());
            }
            else
            {
                Destroy(this.gameObject);
                menu.count++;
                
            }

        }

        else if(menu.mode == "HappyBirthday")
        {
            if (menu.count >= 49)
            {
                menu.starEffect.SetActive(false);
                StartCoroutine(OnComplete());
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                menu.count++;
                
            }
        }

        else if (menu.mode == "OldMan")
        {
            if (menu.count >= 58)
            {
                menu.starEffect.SetActive(false);
                StartCoroutine(OnComplete());
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                menu.count++;
                
            }
        }



    }

    IEnumerator OnComplete()
    {
        
        menu.completion.SetActive(true);
        menu.effects.SetActive(true);
        yield return new WaitForSeconds(4f);
        menu.effects.SetActive(false);
    }
    
    public void OnClick(string name)
    {
        if (name == "Home")
        {
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadHome", 1f);
        }

        else if (name == "Replay")
        {
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadHappy", 1f);
        }

        else if (name == "Replayjingle")
        {
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadJingles", 1f);
        }

        else if (name == "OldMcdonalds")
        {
            FindObjectOfType<FadeInOut>().FadeIn();
            Invoke("LoadOld", 1f);
        }


    }

    void LoadJingles()
    {
        SceneManager.LoadScene(3);
    }

    void LoadHappy()
    {
        SceneManager.LoadScene(2);
    }

    void LoadHome()
    {
        SceneManager.LoadScene(0);
    }
    void LoadOld()
    {
        SceneManager.LoadScene(4);
    }



}
