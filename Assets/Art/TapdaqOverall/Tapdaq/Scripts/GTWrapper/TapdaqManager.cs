using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tapdaq;
using UnityEngine.UI;
using System.Configuration;



public class TapdaqManager :  SingeltonBase<TapdaqManager>
{

    //private static TapdaqManager _instance = null;

    //Tapdaq Tag Names
    private readonly string interstitialTag = "iadvert";
    private readonly string videoTag = "vadvert";
    private readonly string rewardeVideoTag = "radvert";
    private readonly string CrossPromotionTagOverall = "overall";
    private readonly string CrossPromotionTagCategory = "category";


    private static TDNativeAd nativeAdCategory;
    private  static TDNativeAd nativeAdOverall;
    [HideInInspector]
    private static Sprite OverallSpriteSmall;
    private static Sprite CategorySpriteSmall;
    private static Sprite OverallSpriteLarge;
    private static Sprite CategorySpriteLarge;

    //Delegate used to notify client when Ad is available
    private static TapdaqReadyDelegate NotifyTapdaqReady;
    private static AdAvailableDelegate NotifyAdAvailable;
    private static RewardUserDelegate NotifyReward;

    //private bool AutoLoadCPOverall = false;
    private AdSizes CPLoadingSize = AdSizes.Small;
    private bool IsInterstitialAdLoaded = false;
    private bool IsVideoAdLoaded = false;
    private bool IsRewardedAdLoaded = false;

    #region CrossPromotionAds

    /// <summary>
    /// Initializes the Tapdaq SDK.
    /// </summary>
    /// <param name="AutoLoadOverallCP">If set to <c>true</c> auto load Overall CP small.</param>
    /// <param name="_delegate">Delegate to notify client when ad is available.</param>
    public void InitTapdaq(AdAvailableDelegate _Delegate, TapdaqReadyDelegate _Ready)
    {
        Debug.Log("GT >> Initialize Tapdaq");
        TapdaqGAHelper.Log(TapdaqGAEvents.Initializing);

        NotifyAdAvailable = _Delegate;
        NotifyTapdaqReady = _Ready;

        AdManager.Init();
    }

    /// <summary>
    /// Loads the Cross Promotion of specific Type (i.e Overall,Category) and Size.
    /// </summary>
    /// <param name="AdType">Cross Promotion Type.</param>
    /// <param name="CPSize">Cross Promotion Size.</param>
    public void LoadCrossPromotion(AdTypes AdType, AdSizes CPSize)
    {

        if (IsCPLoaded(AdType, CPSize))
        {
            Debug.Log("GT >> CP Already Loaded");
            return;
        }

        CPLoadingSize = CPSize;
        Debug.Log("GT >> Load CP Type: " + AdType.ToString() + " Size:" + CPSize.ToString());

        if (AdType == AdTypes.CPOverAll)
        {
            AdManager.LoadNativeAdvertForTag(CrossPromotionTagOverall, getTDNativeAdType(CPSize));
            TapdaqGAHelper.Log(TapdaqGAEvents.LoadCPOverall);
        }
        else if (AdType == AdTypes.CPCategory)
        {			
            AdManager.LoadNativeAdvertForTag(CrossPromotionTagCategory, getTDNativeAdType(CPSize));
            TapdaqGAHelper.Log(TapdaqGAEvents.LoadCPCategory);
        }
    }

    //see if cross promotion is already loaded
    public bool IsCPLoaded(AdTypes AdType, AdSizes AdSize)
    {
        if (AdType == AdTypes.CPOverAll)
        {
            if (AdSize == AdSizes.Small && OverallSpriteSmall != null)
            {
                return true;
            }
            if (AdSize == AdSizes.Large && OverallSpriteLarge != null)
            {
                return true;
            }
        }
        else if (AdType == AdTypes.CPCategory)
        {
            if (AdSize == AdSizes.Small && CategorySpriteSmall != null)
            {
                return true;
            }
            if (AdSize == AdSizes.Large && CategorySpriteLarge != null)
            {
                return true;
            }
        }
        return false;
    }

    //Converts AdSizes to TDNativeAdType
    private TDNativeAdType getTDNativeAdType(AdSizes size)
    {
        switch (size)
        {
            case AdSizes.Small:
                return TDNativeAdType.TDNativeAdType1x1Small;
            case AdSizes.Medium:
                return TDNativeAdType.TDNativeAdType1x1Medium;
            case AdSizes.Large:
                return TDNativeAdType.TDNativeAdType1x1Large;
        }
        return TDNativeAdType.TDNativeAdType1x1Small;
    }

    //pull CP image from nativeAd object and cache
    private void cacheCPImage(TDNativeAd myNativeAd, AdTypes nativeAdType)
    {
        Debug.Log("GT >> Cache CP Image");
        Texture2D texture = myNativeAd.texture;
        Debug.Log("GT >> Got Texture");

        if (texture == null)
        {
            Debug.LogError("GT >> Texture not loaded");
            return;
        }

        var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        if (nativeAdType == AdTypes.CPOverAll)
        {
            if (CPLoadingSize == AdSizes.Small)
                OverallSpriteSmall = sprite;
            else if (CPLoadingSize == AdSizes.Large)
                OverallSpriteLarge = sprite;
			
            NotifyAdAvailable(AdTypes.CPOverAll, CPLoadingSize);
            TapdaqGAHelper.Log(TapdaqGAEvents.CPOverallLoaded);

        }
        else if (nativeAdType == AdTypes.CPCategory)
        {
            if (CPLoadingSize == AdSizes.Small)
                CategorySpriteSmall = sprite;
            else if (CPLoadingSize == AdSizes.Large)
                CategorySpriteLarge = sprite;
			
            NotifyAdAvailable(AdTypes.CPCategory, CPLoadingSize);
            TapdaqGAHelper.Log(TapdaqGAEvents.CPCategoryLoaded);
        }
    }

    /// <summary>
    /// Displays the Cross Promotion Ad.
    /// </summary>
    /// <param name="TargetImage">Target image.</param>
    /// <param name="TargetType">Target type.</param>
    /// <param name="TargetSize">Target size.</param>
    public void ShowCrosspromotionAd(Image TargetImage, AdTypes TargetType, AdSizes TargetSize)
    {
        if (TargetType == AdTypes.CPOverAll)
        {
            ShowCrosspromotionAdOverall(TargetImage, TargetSize);
        }
        else if (TargetType == AdTypes.CPCategory)
        {
            ShowCrosspromotionAdCategory(TargetImage, TargetSize);
        }
        else
        {
            Debug.Log("GT >> Unknown CP Type");
        }
	
    }

    private void ShowCrosspromotionAdOverall(Image CPImage, AdSizes TargetSize)
    {
        Debug.Log("GT >> Show CP Overall " + TargetSize);

        if (TargetSize == AdSizes.Small && OverallSpriteSmall != null)
        {
            CPImage.sprite = OverallSpriteSmall;

        }
        else if (TargetSize == AdSizes.Large && OverallSpriteLarge != null)
        {
            CPImage.sprite = OverallSpriteLarge;

        }
        else
        {
            Debug.Log("GT >> Tapdaq:Overall CP (" + TargetSize + ") Not Avallibale to show as image is not assigned");
            return;
        }

        AdManager.SendNativeImpression(nativeAdOverall);
        TapdaqGAHelper.Log(TapdaqGAEvents.CPOverallShow);

    }

    private void ShowCrosspromotionAdCategory(Image CPImage, AdSizes TargetSize)
    {
        Debug.Log("GT >> Show CP Category " + TargetSize);
        if (TargetSize == AdSizes.Small && CategorySpriteSmall != null)
        { // if internet is not available then don't show crosspromotion

            CPImage.sprite = CategorySpriteSmall;

        }
        else if ((TargetSize == AdSizes.Large && CategorySpriteLarge != null))
        {

            CPImage.sprite = CategorySpriteLarge;

        }
        else
        {
            Debug.Log("GT >> Tapdaq:Category CP (" + TargetSize + ") Not Avallibale to show as image is not assigned");
            return;
        }
			
        AdManager.SendNativeImpression(nativeAdCategory);
        TapdaqGAHelper.Log(TapdaqGAEvents.CPCategoryShow);
    }

    #endregion


    #region CallBacksFunctions

    private void OnEnable()
    {
        TDCallbacks.TapdaqConfigLoaded += ConfigLoaded;
        TDCallbacks.AdAvailable += OnAdAvailable;
        TDCallbacks.AdNotAvailable += OnAdNotAvailable;
        TDCallbacks.AdWillDisplay += OnAdWillDisplay;
        TDCallbacks.AdError += OnAdError;
        TDCallbacks.RewardVideoValidated += OnRewardVideoValidated;
        TDCallbacks.AdDidDisplay += OnAdDidDisplay;
        TDCallbacks.AdClicked += OnAdClicked;
        TDCallbacks.AdClosed += OnAdClosed;
    }
		
    // Unsubscribe from Tapdaq events
    private void OnDisable()
    {
        TDCallbacks.TapdaqConfigLoaded -= ConfigLoaded;
        TDCallbacks.AdAvailable -= OnAdAvailable;
        TDCallbacks.AdNotAvailable -= OnAdNotAvailable;
        TDCallbacks.AdWillDisplay -= OnAdWillDisplay;
        TDCallbacks.AdError -= OnAdError;
        TDCallbacks.RewardVideoValidated -= OnRewardVideoValidated;
        TDCallbacks.AdDidDisplay -= OnAdDidDisplay;
        TDCallbacks.AdClicked -= OnAdClicked;
        TDCallbacks.AdClosed -= OnAdClosed;
    }
    // ================= callBacks

    private void ConfigLoaded()
    {
        Debug.Log("GT >> Tapdaq Initialized");
        TapdaqGAHelper.Log(TapdaqGAEvents.Initialized);

        NotifyTapdaqReady();
    }


    private void OnAdAvailable(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Available: AdType=" + e.adType + ", Tag:" + e.tag);


        if (e.adType.Equals("NATIVE_AD") && e.tag.Equals(CrossPromotionTagOverall))
        {
			
            nativeAdOverall = AdManager.GetNativeAd(getTDNativeAdType(CPLoadingSize), CrossPromotionTagOverall);
            nativeAdOverall.LoadTexture((TDNativeAd obj) =>
                {
                    cacheCPImage(obj, AdTypes.CPOverAll);
                });
        }
        else if (e.adType.Equals("NATIVE_AD") && e.tag.Equals(CrossPromotionTagCategory))
        {

            nativeAdCategory = AdManager.GetNativeAd(getTDNativeAdType(CPLoadingSize), CrossPromotionTagCategory);
            nativeAdCategory.LoadTexture((TDNativeAd obj) =>
                {
                    cacheCPImage(obj, AdTypes.CPCategory);
                });
        }
        else
        {
            if (e.adType.Equals("VIDEO"))
            {
                TapdaqGAHelper.Log(TapdaqGAEvents.VideoAdLoaded);
                IsVideoAdLoaded = true;
            }
            else if (e.adType.Equals("REWARD_AD"))
            {
                TapdaqGAHelper.Log(TapdaqGAEvents.RewardedAdLoaded);
                IsRewardedAdLoaded = true;
            }
            else if (e.adType.Equals("INTERSTITIAL"))
            {
                TapdaqGAHelper.Log(TapdaqGAEvents.InterstitialAdLoaded);	
                IsInterstitialAdLoaded = true;
            }
        }
    }

    private void OnAdNotAvailable(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Not Available: " + e.tag + "[" + e.adType + "]");

        if (e.adType.Equals("NATIVE_AD") && e.tag.Equals(CrossPromotionTagCategory))
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.CPCategoryNoInventory);
        }
        else if (e.adType.Equals("NATIVE_AD") && e.tag.Equals(CrossPromotionTagOverall))
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.CPOverallNoInventory);
        }
        else if (e.adType.Equals("VIDEO"))
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.VideoAdNoInventory);	
            IsVideoAdLoaded = false;
        }
        else if (e.adType.Equals("REWARD_AD"))
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.RewardedAdNoInventory);
            IsRewardedAdLoaded = false;
        }
        else if (e.adType.Equals("INTERSTITIAL"))
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.InterstitialAdNoInventory);
            IsInterstitialAdLoaded = false;
        }
    }

    private void OnAdError(TDAdEvent e)
    {
        Debug.LogError("GT >> On Ad Error: " + e.message);
    }

    private void OnAdWillDisplay(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Will Display");

        if (e.adType.Equals("VIDEO") /*&& e.tag == interstitialTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.VideoAdWillDisplay);			
        }
        if (e.adType.Equals("REWARD_AD") /*&& e.tag == rewardeVideoTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.RewardedAdWillDisplay);			
        }
        if (e.adType.Equals("INTERSTITIAL") /*&& e.tag == interstitialAdTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.InterstitialAdWillDisplay);			
        }
    }

    private void OnAdDidDisplay(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Did Display : " + e.adType);

        if (e.adType.Equals("VIDEO") /*&& e.tag == interstitialTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.VideoAdDisplayed);			
            IsVideoAdLoaded = false;
        }
        if (e.adType.Equals("REWARD_AD") /*&& e.tag == rewardeVideoTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.RewardedAdDisplayed);			
            IsRewardedAdLoaded = false;
        }
        if (e.adType.Equals("INTERSTITIAL") /*&& e.tag == interstitialAdTag*/)
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.InterstitialAdDisplayed);			
            IsInterstitialAdLoaded = false;
        }


    }

    private void OnAdClicked(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Clicked");
    }

    private void OnAdClosed(TDAdEvent e)
    {
        Debug.Log("GT >> On Ad Clicked");
    }

    /// <summary>
    /// Register Cross Promotion Action
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="adType">Ad type.</param>
    public void CrosspromotionAction(string action, AdTypes adType)
    {
        if (adType == AdTypes.CPOverAll)
        {
            switch (action)
            {
                case "GetItNow":
                    TapdaqGAHelper.Log(TapdaqGAEvents.CPOverallClick);
                    AdManager.SendNativeClick(nativeAdOverall);
                    break;
                case "Cancel":
                    TapdaqGAHelper.Log(TapdaqGAEvents.CPOverallClose);
                    break;
            }
        }
        else if (adType == AdTypes.CPCategory)
        {
            switch (action)
            {
                case "GetItNow":
                    TapdaqGAHelper.Log(TapdaqGAEvents.CPCategoryClick);
                    AdManager.SendNativeClick(nativeAdCategory);
                    break;
                case "Cancel":
                    TapdaqGAHelper.Log(TapdaqGAEvents.CPCategoryClose);
                    break;
            }
        }			
    }

    #endregion

    #region Mediation

    /// <summary>
    /// Loads the Ad of specified Type.
    /// </summary>
    /// <param name="AdType">Ad type.</param>
    public void LoadAd(AdTypes AdType)
    {
        switch (AdType)
        {
            case AdTypes.Interstitial:
                LoadInterstitial();
                break;
            case AdTypes.Video:
                LoadVideo();
                break;
            case AdTypes.Rewarded:	
                LoadRewardedVideo();
                break;
        }
    }

    private void LoadInterstitial()
    {
        Debug.Log("GT >> LoadInterstitial");
        if (IsInterstitialAdLoaded)
        {
            Debug.Log("Interstitial Ad is Ready. Call Show");
            return;
        }
        else
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.LoadInterstitialAd);
            AdManager.LoadInterstitialWithTag(interstitialTag);	
        }
    }

    private void LoadVideo()
    {
        Debug.Log("GT >> LoadVideo");
        if (IsVideoAdLoaded)
        {
            Debug.Log("Video Ad is Ready. Call Show");
            return;
        }
        else
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.LoadVideoAd);
            AdManager.LoadVideoWithTag(videoTag);
        }
    }

    private void LoadRewardedVideo()
    {
        Debug.Log("GT >> LoadRewardedVideo");
        if (IsRewardedAdLoaded)
        {
            Debug.Log("Rewarded Ad is Ready. Call Show");
            return;
        }
        else
        {
            TapdaqGAHelper.Log(TapdaqGAEvents.LoadRewardedAd);
            AdManager.LoadRewardedVideoWithTag(rewardeVideoTag);	
        }
    }

    public void ShowInterstitial()
    {
        Debug.Log("GT >> ShowInterstitial: " + IsInterstitialAdLoaded);
        TapdaqGAHelper.Log(TapdaqGAEvents.ShowInterstitialAd);
        if (IsInterstitialAdLoaded)
        {
            AdManager.ShowInterstitial(interstitialTag);
        } 
        IsInterstitialAdLoaded = false;
    }

    public void ShowVideo()
    {		
        Debug.Log("GT >> ShowVideo: " + IsVideoAdLoaded);
        TapdaqGAHelper.Log(TapdaqGAEvents.ShowVideoAd);
        if (IsVideoAdLoaded)
        {
            AdManager.ShowVideo(videoTag);
        } 
        IsVideoAdLoaded = false;
    }

    public void ShowRewardedVideo(RewardUserDelegate _delegate)
    {	
        Debug.Log("GT >> ShowRewardedVideo: " + IsRewardedAdLoaded);
        NotifyReward = _delegate;
        TapdaqGAHelper.Log(TapdaqGAEvents.ShowRewardedAd);
        if (IsRewardedAdLoaded)
        {
            AdManager.ShowRewardVideo(rewardeVideoTag);
        }
        else
        {
            Debug.Log("GT >> Rewarded Video With Tag:" + rewardeVideoTag + " Not Ready");
        }
        IsRewardedAdLoaded = false;
    }

    private void OnRewardVideoValidated(TDVideoReward videoReward)
    {
        Debug.Log("GT >> OnRewardVideoValidated");
        NotifyReward(videoReward);
    }

    public bool IsRewardedAdReady()
    {	
        Debug.Log("GT >> isRewardedAdready : " + IsRewardedAdLoaded);
        return IsRewardedAdLoaded;
    }

    #endregion
}
