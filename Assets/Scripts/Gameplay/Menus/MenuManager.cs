using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameAnalyticsSDK.Setup;
using System.Security.Cryptography.X509Certificates;

public class MenuManager : MonoBehaviour
{

    //	public GameObject EnemyHealthBar;
    public GameObject PlayerSelectionMenuGameObject;

    void Awake()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.MAINMENU);

        if (GameManager.Instance.GetCurrentGameState() == GameManager.GameState.MAINMENU)
        {
            GAManager.Instance.LogDesignEvent("MainMenu:GameStart");
        }
    }

    public void Start()
    {
		SoundManager.Instance.MuteUnMuteChecker();
    }

    public void OnInMenuButtonPress(string ID)
    {
        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        switch (ID)
        {
            case "Settings":
                if (!GameObject.FindWithTag(Tags.LevelSettings))
                {
                    Instantiate(Resources.Load(Constants.LevelSettings));
                }
                GAManager.Instance.LogDesignEvent("MainMenu:Settings");
                break;
            case "Facebook":
                Application.OpenURL(Constants.FACEBOOK_LINK); 
                GAManager.Instance.LogDesignEvent("MainMenu:facebook");
                break;
            case "Twitter":
                Application.OpenURL(Constants.TWITTER_LINK);
                GAManager.Instance.LogDesignEvent("MainMenu:Twitter");
                break;
            case "MoreGames":
                Application.OpenURL(Constants.MoreGames_LINK);
                GAManager.Instance.LogDesignEvent("MainMenu:MoreGames");
                break;

        }	
    }


    public void OnButtonPress(string ID)
    {
        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        switch (ID)
        {
            
            case "PlayPlayerMenu": 
			//PlayerSelectionMenu ();
                ModeSelectionMenu();
                Destroy(gameObject);
                GetComponent <Canvas>().enabled = false;
                GAManager.Instance.LogDesignEvent("MainMenu:Play");
            break;
          
			case "Pause": 
                Time.timeScale = 0f;
           	break;
        }
		
    }

//    void PlayerSelectionMenu()
//    {
//        PlayerSelectionMenuGameObject.GetComponent <Canvas>().enabled = true;
////		Instantiate (Resources.Load (Constants.PlayerSelectionMenu));
//    }

    void ModeSelectionMenu()
    {
        if (GameObject.FindWithTag(Tags.MainMenu))
            Destroy(GameObject.FindWithTag(Tags.MainMenu));
        
        Instantiate(Resources.Load(Constants.ModeSelectionMenu));
    }


    public void BackButton()
    {
        if (GameObject.FindWithTag(Tags.MainMenu) && !GameObject.FindWithTag(Tags.LevelSettings) && !GameObject.FindWithTag(Tags.ModeSelectionMenu) && !GameObject.FindWithTag(Tags.PlayerSelectionMenu).GetComponent<Canvas>().enabled)
        {
            Application.Quit();
        }

        if (GameObject.FindWithTag(Tags.MenuBar) && GameObject.FindWithTag(Tags.MenuBar).GetComponent<CanvasGroup>().alpha == 1)
        {
            OnButtonPress("Pause");
            GameObject.FindWithTag(Tags.MenuBar).GetComponent<CanvasGroup>().alpha = 0;
            GameObject.FindWithTag(Tags.MenuBar).GetComponent<CanvasGroup>().interactable = true;
            GameObject.FindGameObjectWithTag(Tags.Joystick).transform.GetChild(0).gameObject.GetComponent<CanvasGroup>().alpha = 0;
			
            if (GameObject.Find("ControllerUi"))
            {
                GameObject.Find("ControllerUi").GetComponent<CanvasGroup>().alpha = 0;
                GameObject.Find("ControllerUi").GetComponent<CanvasGroup>().interactable = false;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameObject.FindWithTag(Tags.LevelExit))
            {
                return;
            }

            if (GameObject.FindWithTag(Tags.Enjoying))
            {
                return;
            }

            if (GameObject.FindWithTag(Tags.LevelSettings))
            {
                return;
            }

            if (GameObject.FindWithTag(Tags.LockedLevelScriptCoinVideo))
            {
                return;
            }

            if (GameObject.FindWithTag(Tags.NoMoreAd))
            {
                return;
            }

            if (GameObject.FindWithTag(Tags.levelSelectionMenu))
            {
                return;
            }



            BackButton();
        }

    }

}
