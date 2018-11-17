using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Tapdaq;

public class AdsManager : SingeltonBase<AdsManager>
{

    private bool gameLaunch = true;
    private bool Ads_status;

    public static bool isLevelComplete;
    public static bool isLevelFail;

    public bool ShowOverallAd = true, ShowCategoryAd = true;


    void Start()
    {   
        // for tapdaq Category
        isLevelComplete = false;
        isLevelFail = false;
    }

    public void DisplayAd()
    {
        if (UserPrefs.isAmazonBuild)
        {
            if (gameLaunch)
            {
                gameLaunch = false;
                UserPrefs.Load();
            }
            return;
        }

        switch (GameManager.Instance.GetCurrentGameState())
        {
            case GameManager.GameState.MAINMENU:
                if (gameLaunch)
                {
                    UserPrefs.Load();
                }
            
                break;
        }
    }

    #region GameAds

    public void RequestForAd()
    {
        if (!UserPrefs.isIgnoreAds)
        {

            if ((Constants.selectedLevel + 1) % 3 == 1) // Interstitial
            {
                TapdaqManager.Instance.LoadAd(AdTypes.Interstitial);
            }
            else if ((Constants.selectedLevel + 1) % 3 == 2)
            {
                if ((Constants.selectedLevel + 1) % 2 == 1)
                {
                    if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPOverAll, AdSizes.Large))
                        TapdaqManager.Instance.LoadCrossPromotion(AdTypes.CPOverAll, AdSizes.Large);
                }
                else
                {
                    if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPCategory, AdSizes.Large))
                        TapdaqManager.Instance.LoadCrossPromotion(AdTypes.CPCategory, AdSizes.Large);
                }
                
            }
            else if ((Constants.selectedLevel + 1) % 3 == 0) // Video
            {
                TapdaqManager.Instance.LoadAd(AdTypes.Video);
            }         
        }
    }




    public void ShowAdOnLevelEnd()
    {
        if (!UserPrefs.isIgnoreAds)
        {   
            if ((Constants.selectedLevel + 1) % 3 == 1) // Interstitial
            {
                TapdaqManager.Instance.ShowInterstitial();
            }
            else if ((Constants.selectedLevel + 1) % 3 == 2)
            {
               
            }
            else if ((Constants.selectedLevel + 1) % 3 == 0) // Video
            {
                TapdaqManager.Instance.ShowVideo();
            } 
        }
    }

    public void ShowLevelEndOrCategoryAd()
    {
        
        if ((Constants.selectedLevel + 1) % 2 == 1)
        {
            if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPOverAll, AdSizes.Large) || !ShowCategoryAd)
            {
                ShowLevelEnd();
                return;
            }
        }
        else
        {
            if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPCategory, AdSizes.Large) || !ShowCategoryAd)
            {
                ShowLevelEnd();
                return;
            }   
        }

        isLevelFail = false;
        isLevelComplete = true;


        if ((Constants.selectedLevel + 1) % 3 == 2)
        {
            if ((Constants.selectedLevel + 1) % 2 == 1)
            {
                GameObject.FindObjectOfType<TapdaqUIManager>().ShowOverallLarge();
            }
            else
                GameObject.FindObjectOfType<TapdaqUIManager>().ShowCategory();
        }
        else
        {
            if (!GameObject.FindWithTag(Tags.LevelComplete))
                Instantiate(Resources.Load("uGUIMenus/LevelComplete"));
        }
    }

    public void ShowLevelEnd()
    {
        isLevelComplete = false;
        if (!GameObject.FindWithTag(Tags.LevelComplete))
            Instantiate(Resources.Load("uGUIMenus/LevelComplete"));
    }

    public void ShowLevelEndOrCategoryAdOnFail()
    {


        if ((Constants.selectedLevel + 1) % 2 == 1)
        {
            if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPOverAll, AdSizes.Large) == null || !ShowCategoryAd)
            {
                ShowLevelEndOnFail();
                return;
            }
        }
        else
        {
            if (!TapdaqManager.Instance.IsCPLoaded(AdTypes.CPCategory, AdSizes.Large) || !ShowCategoryAd)
            {
                ShowLevelEndOnFail();
                return;
            }   
        }

        isLevelComplete = false;
        isLevelFail = true;

        if ((Constants.selectedLevel + 1) % 3 == 2)
        {
            if ((Constants.selectedLevel + 1) % 2 == 1)
            {
                GameObject.FindObjectOfType<TapdaqUIManager>().ShowOverallLarge();
            }
            else
                GameObject.FindObjectOfType<TapdaqUIManager>().ShowCategory();
        }
        else
        {
            if (!GameObject.FindWithTag(Tags.LevelFail))
                Instantiate(Resources.Load("uGUIMenus/LevelFail"));
        }
    }

    public void ShowLevelEndOnFail()
    {
        isLevelFail = false;
        if (!GameObject.FindWithTag(Tags.LevelFail))
            Instantiate(Resources.Load("uGUIMenus/LevelFail"));
    }

    #endregion

    #region RemoveAds

    public void removeAds()
    {
        if (!UserPrefs.isIgnoreAds)
        {
            UserPrefs.isIgnoreAds = true;
            UserPrefs.Save();
        }
    }

    public void LoadRewardedVideo()
    {
        if (!UserPrefs.isIgnoreAds && !TapdaqManager.Instance.IsRewardedAdReady())
        {  
            Debug.Log("Load Rewarded Video");
            TapdaqManager.Instance.LoadAd(AdTypes.Rewarded);
        }
    }

    public void ShowRewardedVideo()
    {
        if (!UserPrefs.isIgnoreAds)
        {  
            if (TapdaqManager.Instance.IsRewardedAdReady())
            {
                TapdaqManager.Instance.ShowRewardedVideo(RewardUser);    
            }
            else
            {
//              
                if (!GameObject.FindGameObjectWithTag(Tags.NoMoreAd))
                    Instantiate(Resources.Load("uGUIMenus/NoMoreAds"));
                
                LoadRewardedVideo();
            }

        }
    }

    public void RewardUser(TDVideoReward videoReward)
    {
        if (!UserPrefs.isIgnoreAds)
        {  
            Invoke("LoadRewardedVideo", 2);
        }
    }

    #endregion

   
}


