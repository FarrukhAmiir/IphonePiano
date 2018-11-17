using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSelectionManager : MonoBehaviour
{

    // Use this for initialization
    public GameObject SurvivalModeLocked;

    public void Start()
    {
        bool deviceIsIphoneX = UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhoneX;
        GetComponent<Canvas> ().worldCamera = Camera.main;
        if (UserPrefs.IsSurvivalModeUnlocked)
        {
            SurvivalModeLocked.SetActive(false);
        }
    }

	public void PoemSelect(string poemName)
	{
		FindObjectOfType<FadeInOut> ().FadeIn ();
	//	Variables.currentVideo = poemName;
		Invoke ("loadGame", 1f);
	}

	public void loadGame()
	{
		GameManager.Instance.LoadLoadingScreen(Constants.GamePlay);
		}

    public void OnCampaignModeClick()
    {

        GAManager.Instance.LogDesignEvent("ModeSelection:CareerMode");
        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        GameManager.Instance.currentGameMode = GameManager.GameMode.CampaignMode;
        LoadLevelSelection();

    }

    public void OnSurvivalModeClick()
    {
        GAManager.Instance.LogDesignEvent("ModeSelection:SurvivalMode");
        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        if (UserPrefs.IsSurvivalModeUnlocked)
        {
            GameManager.Instance.currentGameMode = GameManager.GameMode.SurvivalMode;
            Constants.selectedLevel = 24;
            GameManager.Instance.LoadLoadingScreen(Constants.GamePlay);
        }
        else
        {
            Instantiate(Resources.Load(Constants.LockedSurvivalMode));
        }
    }

    void LoadLevelSelection()
    {
        Instantiate(Resources.Load(Constants.LevelSelectionMenu));
        Destroy(gameObject);
    }

    public void OnButtonPress(string ID)
    {
       
        switch (ID)
        {
            case "Back":

                FindObjectOfType<FadeInOut>().FadeIn();
                Invoke("LoadHome", 1f);
                break;
        }		

    }

    public void BackButton()
    {

        if (GameObject.FindWithTag(Tags.ModeSelectionMenu) && !GameObject.FindWithTag(Tags.LockedLevelScriptCoinVideo) && !GameObject.FindWithTag(Tags.NoMoreAd))
        {
            OnButtonPress("Back");
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BackButton();
        }
    }

    void LoadHome()
    {
        SceneManager.LoadScene(0);
    }
}
