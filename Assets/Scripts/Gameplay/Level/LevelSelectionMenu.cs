using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    public ScrollRect SRect;
    public RectTransform LevelPanel;
    public GameObject[] LevelsArray;
    public Sprite[] SelectedCurrent;
    public Sprite PreviousSprite;

    RectTransform temp;

    public Image SelectedSprite;
    public Image PressedSprite;

    public Text coins;

    float movingFactor = 0.2f;
    Vector2 targetVector;
    bool notScrolling = false;

    public void Start()
    {
        Time.timeScale = 1;
        UpdateLevelArray();
        GAManager.Instance.LogDesignEvent("LevelSelection");

        PressedSprite.transform.position = Vector3.one * 5000;

        if (UserPrefs.currentLevel >= 6)
        {
            LevelPanel.anchoredPosition = new Vector2(-150 * (UserPrefs.currentLevel + 1), LevelPanel.anchoredPosition.y);
        } 

    }

    void LerpPanel(RectTransform target, Vector2 position)
    {
        while (position != target.anchoredPosition)
        {
            LevelPanel.anchoredPosition = Vector2.Lerp(LevelPanel.anchoredPosition, targetVector.x > -3300 ? targetVector : new Vector2(-3300, 0), Time.deltaTime * 5);

        }
    }

    public void OnLevelButtonPress(int LevelID)
    {
		

        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        GAManager.Instance.LogDesignEvent("LevelSelection:LevelNumber:" + (LevelID + 1));

        if (LevelID <= UserPrefs.currentLevel)
        {
            Constants.selectedLevel = LevelID;

//			if (Constants.selectedLevel == 0)
//				GameManager.Instance.LoadLoadingScreen (Constants.CutScene);
//            if ((LevelID >= 0 && LevelID < 4) || (LevelID >= 8 && LevelID < 12) || (LevelID >= 16 && LevelID < 20))
            GameManager.Instance.LoadLoadingScreen(Constants.GamePlay);
//            else
//                GameManager.Instance.LoadLoadingScreen(Constants.secondEnvironment);


        }
        if (LevelID == UserPrefs.currentLevel + 1)
        {
            if (!GameObject.FindWithTag(Tags.LockedLevelScriptCoinVideo))
                Instantiate(Resources.Load(Constants.LockedLevelSkipCoinVideo));
        }

    }

    void UpdateLevelArray()
    {
        foreach (GameObject g in LevelsArray)
        {
            g.GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i <= UserPrefs.currentLevel; i++)
        {
            if (i != 24)
            {
                LevelsArray[i].GetComponent<Button>().interactable = true;
                LevelsArray[i].GetComponent<Button>().transition = Selectable.Transition.ColorTint;
                LevelsArray[i].transform.Find("LockedSprite").gameObject.SetActive(false);
                LevelsArray[i].GetComponent<Animator>().enabled = false;

                if (i == UserPrefs.currentLevel)
                {
                    SelectedSprite.GetComponent<RectTransform>().anchoredPosition = LevelsArray[i].GetComponent<RectTransform>().anchoredPosition;
                }
            }
        }
        if (UserPrefs.currentLevel != 23)
        {
            if (UserPrefs.currentLevel != 24)
                LevelsArray[UserPrefs.currentLevel + 1].GetComponent<Button>().interactable = true;
        }
    }

    public void OnButtonPress(string ID)
    {
        SoundManager.Instance.PlaySound(GameManager.SoundState.BUTTONCLICKSOUND);
        switch (ID)
        {
            case "Back": 
                Instantiate(Resources.Load(Constants.ModeSelectionMenu));
                Time.timeScale = 1;
                Destroy(gameObject);
                break;
        }		

    }

    public void BackButton()
    {

        if (GameObject.FindWithTag(Tags.levelSelectionMenu) && !GameObject.FindWithTag(Tags.LockedLevelScriptCoinVideo) && !GameObject.FindWithTag(Tags.NoMoreAd))
        {
            OnButtonPress("Back");
        }
    }

    public void OnNextBtn()
    {
		
        notScrolling = true;
        targetVector = new Vector2(Mathf.Clamp(SRect.horizontalNormalizedPosition + movingFactor, 0f, 1), SRect.verticalNormalizedPosition);
    }

    public void OnPreBtn()
    {
        notScrolling = true;

        targetVector = new Vector2(Mathf.Clamp(SRect.horizontalNormalizedPosition - movingFactor, 0f, 1), SRect.verticalNormalizedPosition);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            BackButton();
        }

        SRect.normalizedPosition = Vector2.Lerp(SRect.normalizedPosition, targetVector, Time.deltaTime * 5);
    }

    public void SetTargetVector(Vector2 v)
    {

//		Debug.Log (v);
        targetVector = SRect.normalizedPosition;
        targetVector.y = 0;

    }


    public void OnLevelBtnDown(int levelNum)
    {
//		PressedSprite.transform.position = LevelsArray[levelNum].transform.position;
//		PressedSprite.transform.parent = LevelsArray[levelNum].transform;
    }

    public void OnLevelBtnUp()
    {
//		PressedSprite.transform.position = Vector3.one * 5000;
//		PressedSprite.transform.parent = null;
    }
}
