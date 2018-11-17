using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*Notes- Zulqarnain*/
/*
	- Level Manager :
	3 Types of Animators: Easy , Medium , High_NPC_Animators
	
	NPCs will be randomly chosen from NPCs List and given Random Animators depending upon the CurrentLevel
	Boss will enter in 7th and 23rd level.

*/
public class LevelManager : MonoBehaviour
{
    public enum LevelType
    {
        killType}

    ;

    public GameObject spawnManagerParent;

    [System.Serializable]
    public class AllLevels
    {
        public GameObject Levels;
        public Transform LevelStartPosition;
        public LevelType _CurrentLevelType;
        public DayType _dayType;
        public int KillAmount = 0;
        public bool isBossLevel;
        public bool FindObject = false;
        public bool isSurvival = false;
    }

    public bool isLevelComplete;
    public bool isLevelFailed;
    public int CurrentLevel;
    public GameObject[] NPC_list;
    public AnimatorOverrideController[] Easy_NPC_Animators;
    public AnimatorOverrideController[] Med_NPC_Animators;
    public AnimatorOverrideController[] High_NPC_Animators;
    public GameObject[] Boss_list;
    public AnimatorOverrideController[] Boss_Animators;
    public AllLevels[] CompleteAllLevels;

    int EnemiesCount;

    References refObj;

    void Start()
    {
        //Debug.LogError("request");

        if (UserPrefs.isSound)
        {
            SoundManager.Instance.UnMuteSound();
        }

        if (UserPrefs.isMusic)
        {
            SoundManager.Instance.UnMuteMusic();
        }

        // For Device fps Issue 

//        if (SystemInfo.systemMemorySize < 1500)
//        {
//            if (GameManager.Instance.currentGameMode == GameManager.GameMode.SurvivalMode)
//                CompleteAllLevels[Constants.selectedLevel].KillAmount = (CompleteAllLevels[Constants.selectedLevel].KillAmount / 2);
//        }

//
//		if(Constants.selectedLevel == 14)
//		{
//			CompleteAllLevels [Constants.selectedLevel].KillAmount = (CompleteAllLevels [Constants.selectedLevel].KillAmount/2) - 2;
//		}

        EnemiesCount = CompleteAllLevels[Constants.selectedLevel].KillAmount;

        AdsManager.Instance.RequestForAd();

        GAManager.Instance.LogDesignEvent("GamePlay:LevelStart:" + (Constants.selectedLevel + 1));

        LoadLevel(Constants.selectedLevel);
        refObj = GameObject.FindWithTag(Tags.References).GetComponent<References>();


    }

    public void CheckLevelComplete()
    {
        if (!isLevelComplete)
        {

            if (GameManager.Instance.currentGameMode == GameManager.GameMode.CampaignMode)
            {
                if (CompleteAllLevels[Constants.selectedLevel]._CurrentLevelType == LevelType.killType)
                {
                    if (CompleteAllLevels[Constants.selectedLevel].KillAmount <= 0)
                    {
//						AdsManager.Instance.ShowMopubAdOnLevelEnd ();
                        isLevelComplete = true;
                        Constants.isLevelComplete = true;

                        GameManager.Instance.SaveNextLevel(Constants.selectedLevel);
                        if (!GameObject.FindWithTag(Tags.LevelComplete))
                        {
							
                            Invoke("LevelCompleteEffectSound", 2f);
                            //SoundManager.Instance.PlaySound(GameManager.SoundState.DOOR_OPENING_SOUND);
                            StartCoroutine(DisableFence());
                            GetComponent<MissionBriefing>().OnMissionEnd();

                            if (!UserPrefs.isRatesUS && (Constants.selectedLevel == 2 || Constants.selectedLevel == 5 || Constants.selectedLevel == 8))
                            {
                                Invoke("ShowRateUs", 3);
                            }
                            else
                                Invoke("ShowLevelCompletePopup", 5f); 
							
                        }
                    }
                }
            }
            else if (GameManager.Instance.currentGameMode == GameManager.GameMode.SurvivalMode)
            {
                if (CompleteAllLevels[24]._CurrentLevelType == LevelType.killType)
                {
                    if (CompleteAllLevels[24].KillAmount <= 0)
                    {
                        isLevelComplete = true;
                        Constants.isLevelComplete = true;
                        if (!GameObject.FindWithTag(Tags.LevelComplete))
                        {
							
                            Invoke("LevelCompleteEffectSound", 2f);
                           // SoundManager.Instance.PlaySound(GameManager.SoundState.DOOR_OPENING_SOUND);
                            StartCoroutine(DisableFence());
                            GetComponent<MissionBriefing>().OnMissionEnd();
                            Invoke("ShowLevelCompletePopup", 5f);
                        }
                    }
                }
            }
        }
    }

    public void LoadLevel(int LevelID)
    {

        if (GameManager.Instance.currentGameMode == GameManager.GameMode.SurvivalMode)
        {
		
            CompleteAllLevels[24].Levels.SetActive(true);
            Camera.main.transform.position = CompleteAllLevels[24].Levels.transform.Find("StartCameraPosition").transform.position;
            Camera.main.transform.rotation = CompleteAllLevels[24].Levels.transform.Find("StartCameraPosition").transform.rotation;

            loadPlayer();

            InstantiateNPCs(24);
            isLevelComplete = false;
            Constants.isLevelComplete = isLevelComplete;
            isLevelFailed = false;
            DayAndNightSystem._dayType = CompleteAllLevels[24]._dayType;
        }
        else if (GameManager.Instance.currentGameMode == GameManager.GameMode.CampaignMode)
        {
           
            CompleteAllLevels[LevelID].Levels.SetActive(true);
            Camera.main.transform.position = CompleteAllLevels[LevelID].Levels.transform.Find("StartCameraPosition").transform.position;
            Camera.main.transform.rotation = CompleteAllLevels[LevelID].Levels.transform.Find("StartCameraPosition").transform.rotation;

            loadPlayer();

            InstantiateNPCs(LevelID);
            isLevelComplete = false;
            Constants.isLevelComplete = isLevelComplete;
            isLevelFailed = false;
            DayAndNightSystem._dayType = CompleteAllLevels[LevelID]._dayType;
        }
    }

    IEnumerator DisableFence()
    {
        yield return new WaitForSeconds(1);
    }

    void InstantiateNPCs(int LevelID)  /*Zulqarnain */
    {
        int BossLevelAmount = 0;
//		
        #region Boss_Instantiation 
        if (Constants.selectedLevel.Equals(3) || Constants.selectedLevel.Equals(7) || Constants.selectedLevel.Equals(15) || Constants.selectedLevel.Equals(23))
        {
            BossLevelAmount = 1;
            for (int i = 0; i < BossLevelAmount; i++)
            {
                if (transform.GetChild(Constants.selectedLevel).Find("BossPosition"))
                {
                    int rndm = Random.Range(0, Boss_list.Length);

                    GameObject Cheeta_Boss = (GameObject)Instantiate(Boss_list[rndm], transform.GetChild(Constants.selectedLevel).Find("BossPosition").transform.position, transform.GetChild(Constants.selectedLevel).Find("BossPosition").transform.rotation);
                    rndm = Random.Range(0, Boss_Animators.Length);
                    Cheeta_Boss.GetComponent<Animator>().runtimeAnimatorController = Boss_Animators[rndm];

                   

                }
            }
        }
        #endregion

        #region NPC_Instantiation
			
        int total_NPC_positions_Available = spawnManagerParent.transform.GetChild(LevelID).transform.childCount;
        int[] usedPositions = new int[total_NPC_positions_Available];
        foreach (var pos in usedPositions)
        {
            usedPositions[pos] = 0;
        }
        if (CompleteAllLevels[Constants.selectedLevel].KillAmount <= total_NPC_positions_Available)
        {
            for (int i = 0; i < (CompleteAllLevels[Constants.selectedLevel].KillAmount) - BossLevelAmount; i++)
            {
                int rndm = Random.Range(0, total_NPC_positions_Available);
                while (!searchIfPositionExist(usedPositions, rndm))
                {
                    rndm = Random.Range(0, total_NPC_positions_Available);

                }
                usedPositions[rndm]++;
                DecideNpcSpawning(rndm, LevelID);

            }
		
        }
        else
        {
            Debug.LogError("Level Manager --> Not Enough Positions available to instantiate NPCs");
        }

        #endregion
    }

    bool searchIfPositionExist(int[] array1, int pos)
    {
        if (array1[pos] != 0)
        {
            return false;
        }
        else
            return true;
    }

    void DecideNpcSpawning(int pos, int LevelID)
    {
       
    }

    void DecideAnimator(string anim, GameObject npc)
    {
        int rndm = 0;
        switch (anim)
        {
            case "easy":
                rndm = Random.Range(0, Easy_NPC_Animators.Length);
                npc.GetComponent<Animator>().runtimeAnimatorController = Easy_NPC_Animators[rndm];
                break;

            case "med":
                rndm = Random.Range(0, Med_NPC_Animators.Length);
                npc.GetComponent<Animator>().runtimeAnimatorController = Med_NPC_Animators[rndm];
                break;

            case "high":
                rndm = Random.Range(0, High_NPC_Animators.Length);
                npc.GetComponent<Animator>().runtimeAnimatorController = High_NPC_Animators[rndm];
                break;
            case "boss":
                rndm = Random.Range(0, Boss_Animators.Length);
                npc.GetComponent<Animator>().runtimeAnimatorController = Boss_Animators[rndm];
                break;

	

            default:
                break;
        }
    }

    void ShowLevelCompletePopup()
    {

//		if(Constants.selectedLevel == 23)
//		{
//			Constants.selectedLevel = 24;
//			GameManager.Instance.LoadLoadingScreen(Constants.CutScene);
//		}

        if (!GameObject.FindWithTag(Tags.LevelExit))
        {	
            GAManager.Instance.LogDesignEvent("GamePlay:LevelComplete:" + (Constants.selectedLevel + 1));
            Time.timeScale = 1;
            if ((Constants.selectedLevel + 1) % 3 == 2)
            {
                Variables.isLevelComplete = true;
                AdsManager.Instance.ShowLevelEndOrCategoryAd();
                //TapdaqManager.Instance.ShowCrosspromotionAdCategory();
		
            }
            else
                Instantiate(Resources.Load(Constants.LevelComplete));

        }

    }




    // void FixedUpdate()
    // {
    // 	CheckLevelComplete();
    // }

    public void EnemyKilled()
    {
       
    }

    void loadPlayer()
    {
        GameObject tempPlayer =	GameObject.FindWithTag(Tags.player);

        if (GameManager.Instance.currentGameMode == GameManager.GameMode.CampaignMode)
        {
            tempPlayer.transform.localPosition = CompleteAllLevels[Constants.selectedLevel].LevelStartPosition.localPosition;
            tempPlayer.transform.localEulerAngles = CompleteAllLevels[Constants.selectedLevel].LevelStartPosition.localEulerAngles;
        }
        else if (GameManager.Instance.currentGameMode == GameManager.GameMode.SurvivalMode)
        {
            tempPlayer.transform.localPosition = CompleteAllLevels[24].LevelStartPosition.localPosition;
            tempPlayer.transform.localEulerAngles = CompleteAllLevels[24].LevelStartPosition.localEulerAngles;
        }

    }

    void ShowRateUs()
    {
        if (!UserPrefs.isRatesUS)
        {
            if (!GameObject.FindWithTag(Tags.RateUs))
                Instantiate(Resources.Load(Constants.Enjoying));
        }
    }

    void LevelCompleteEffectSound()
    {
        if (UserPrefs.isMusic)
        {
            //SoundManager.Instance.muteMusicExceptEffectSource();
        }
        SoundManager.Instance.PlaySound(GameManager.SoundState.LEVELCOMPLETIONSOUND);
    }
}
