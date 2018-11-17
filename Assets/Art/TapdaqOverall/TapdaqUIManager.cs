using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TapdaqUIManager : MonoBehaviour
{



    public Image overallImg, categoryImg;

    public GameObject Overall, Category;

    bool CanShowOverall;
  
    TapdaqManager tapdaqManager;
    bool HiddenOverall;

    void Awake()
    {
        if (GameObject.FindObjectsOfType<TapdaqManager>().Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

        }
        tapdaqManager = TapdaqManager.Instance;
    }

    void Start()
    {
        if (AdsManager.Instance.ShowOverallAd)
            tapdaqManager.InitTapdaq(OnAdAvailableDelegate, TapdaqReady);
    }

    void Update()
    {
        if (!GameObject.FindWithTag(Tags.MainMenu) && AdsManager.Instance.ShowOverallAd)// && !HiddenOverall)
        {
            HideOverAll();
        }
    }

    public void TapdaqReady()
    {
        TapdaqManager.Instance.LoadCrossPromotion(AdTypes.CPOverAll, AdSizes.Small);
        Invoke("RequestForCatogryAd", 2f);
        Invoke("RequestRewardedAd", 3f);
    }

    public void OnAdAvailableDelegate(AdTypes adtype, AdSizes adsize)
    {
       
    }

    void RequestForCatogryAd()
    {
        TapdaqManager.Instance.LoadCrossPromotion(AdTypes.CPCategory, AdSizes.Large);
    }

    void RequestRewardedAd()
    {
        TapdaqManager.Instance.LoadAd(AdTypes.Rewarded); 
    }
        

    //    #region Requests
    //
    //    public void RequestoverAll()
    //    {
    //        Debug.Log("Tapdaq : Request Overall");
    //        tapdaqManager.LoadNativeCpOverall();
    //    }
    //
    //    public void RequestCategory()
    //    {
    //        Debug.Log("Tapdaq : Request Category");
    //        tapdaqManager.LoadNativeCPCategory();
    //    }
    //
    //    #endregion


    #region Show

    public void ShowOverAll()
    {
        if (TapdaqManager.Instance.IsCPLoaded(AdTypes.CPOverAll, AdSizes.Small))
        {
            Debug.Log("ShowOverall");
            tapdaqManager.ShowCrosspromotionAd(overallImg, AdTypes.CPOverAll, AdSizes.Small);
            Overall.SetActive(true);
            HiddenOverall = false;

        }
    }

    public void HideOverAll()
    {
        if (Overall.activeSelf)
        {
            Overall.SetActive(false);
            HiddenOverall = true;
        }
    }

    public void ShowCategory()
    {
        if ((Constants.selectedLevel + 1) % 3 == 2)
        {
            Debug.Log("Tapdaq : Show Category");
            tapdaqManager.ShowCrosspromotionAd(categoryImg, AdTypes.CPCategory, AdSizes.Large);
            Category.SetActive(true);
        }
    }

    public void ShowOverallLarge()
    {
        if ((Constants.selectedLevel + 1) % 3 == 2)
        {
            if ((Constants.selectedLevel + 1) % 2 == 1)
            {
                Debug.Log("Tapdaq : Show Overall large");
                tapdaqManager.ShowCrosspromotionAd(categoryImg, AdTypes.CPOverAll, AdSizes.Large);
                Category.SetActive(true);
            }
        }
    }

    #endregion


    #region Clicks

    public void GetitNowOverall()
    {
        tapdaqManager.CrosspromotionAction("GetItNow", AdTypes.CPOverAll);
    }

    public void GetitNowCategory()
    {
        if ((Constants.selectedLevel + 1) % 2 == 1)
            tapdaqManager.CrosspromotionAction("GetItNow", AdTypes.CPOverAll);
        else
            tapdaqManager.CrosspromotionAction("GetItNow", AdTypes.CPCategory);
      
        Category.SetActive(false);
        if (AdsManager.isLevelComplete)
        {
            AdsManager.Instance.ShowLevelEnd();
        }
        else if (AdsManager.isLevelFail)
        {
            AdsManager.Instance.ShowLevelEndOnFail();
        } 

    }

    public void CancelCategory()
    {
        if ((Constants.selectedLevel + 1) % 2 == 1)
            tapdaqManager.CrosspromotionAction("Cancel", AdTypes.CPOverAll);
        else
            tapdaqManager.CrosspromotionAction("Cancel", AdTypes.CPCategory);

        Category.SetActive(false);
        if (AdsManager.isLevelComplete)
        {
            AdsManager.Instance.ShowLevelEnd();
        }
        else if (AdsManager.isLevelFail)
        {
            AdsManager.Instance.ShowLevelEndOnFail();
        }
    }

    #endregion
}
