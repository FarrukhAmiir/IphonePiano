using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tapdaq;
using UnityEngine.UI;
using System.Configuration;

public class TapdaqGAHelper
{
		
	public static void Log (TapdaqGAEvents EventArg)
	{
		switch (EventArg) {
		case TapdaqGAEvents.Initializing:
			LogGAEvent ("Tapdaq:Init");
			break;
		case TapdaqGAEvents.Initialized:
			LogGAEvent ("Tapdaq:Initialized");
			break;
		//Request
		case TapdaqGAEvents.LoadInterstitialAd:
			LogGAEvent ("Tapdaq:iAd:Request");
			break;
		case TapdaqGAEvents.LoadVideoAd:
			LogGAEvent ("Tapdaq:vAd:Request");
			break;
		case TapdaqGAEvents.LoadRewardedAd:
			LogGAEvent ("Tapdaq:rAd:Request");
			break;
		case TapdaqGAEvents.LoadCPOverall:
			LogGAEvent ("Tapdaq:Overall:Request");
			break;	
		case TapdaqGAEvents.LoadCPCategory:
			LogGAEvent ("Tapdaq:Category:Request");
			break;	
		//LOADED
		case TapdaqGAEvents.InterstitialAdLoaded:
			LogGAEvent ("Tapdaq:iAd:Loaded");
			break;
		case TapdaqGAEvents.VideoAdLoaded:
			LogGAEvent ("Tapdaq:vAd:Loaded");
			break;
		case TapdaqGAEvents.RewardedAdLoaded:
			LogGAEvent ("Tapdaq:rAd:Loaded");
			break;
		case TapdaqGAEvents.CPOverallLoaded:
			LogGAEvent ("Tapdaq:Overall:Loaded");
			break;
		case TapdaqGAEvents.CPCategoryLoaded:
			LogGAEvent ("Tapdaq:Category:Loaded");
			break;
		//Show Call
		case TapdaqGAEvents.ShowInterstitialAd:
			LogGAEvent ("Tapdaq:iAd:ShowCall");
			break;
		case TapdaqGAEvents.ShowVideoAd:
			LogGAEvent ("Tapdaq:vAd:ShowCall");
			break;
		case TapdaqGAEvents.ShowRewardedAd:
			LogGAEvent ("Tapdaq:rAd:ShowCall");
			break;
		case TapdaqGAEvents.CPOverallShow:
			LogGAEvent ("Tapdaq:Overall:ShowCall");
			break;
		case TapdaqGAEvents.CPCategoryShow:
			LogGAEvent ("Tapdaq:Category:ShowCall");
			break;	
		//Will Display
		case TapdaqGAEvents.InterstitialAdWillDisplay:
			LogGAEvent ("Tapdaq:iAd:WillDisplay");
			break;
		case TapdaqGAEvents.VideoAdWillDisplay:
			LogGAEvent ("Tapdaq:vAd:WillDisplay");
			break;
		case TapdaqGAEvents.RewardedAdWillDisplay:
			LogGAEvent ("Tapdaq:rAd:WillDisplay");
			break;
		//Displayed
		case TapdaqGAEvents.InterstitialAdDisplayed:
			LogGAEvent ("Tapdaq:iAd:Displayed");
			break;
		case TapdaqGAEvents.VideoAdDisplayed:
			LogGAEvent ("Tapdaq:vAd:Displayed");
			break;
		case TapdaqGAEvents.RewardedAdDisplayed:
			LogGAEvent ("Tapdaq:rAd:Displayed");
			break;
		//No Inventory
		case TapdaqGAEvents.RewardedAdNoInventory:
			LogGAEvent ("Tapdaq:rAd:NoInventory");
			break;		
		case TapdaqGAEvents.InterstitialAdNoInventory:
			LogGAEvent ("Tapdaq:iAd:NoInventory");
			break;	
		case TapdaqGAEvents.VideoAdNoInventory:
			LogGAEvent ("Tapdaq:vAd:NoInventory");
			break;	
		case TapdaqGAEvents.CPCategoryNoInventory:
			LogGAEvent ("Tapdaq:Category:NoInventory");
			break;	
		case TapdaqGAEvents.CPOverallNoInventory:
			LogGAEvent ("Tapdaq:Overall:NoInventory");
			break;	
		//Click
		case TapdaqGAEvents.CPCategoryClick:
			LogGAEvent ("Tapdaq:Category:Click");
			break;	
		case TapdaqGAEvents.CPCategoryClose:
			LogGAEvent ("Tapdaq:Category:Close");
			break;	
		case TapdaqGAEvents.CPOverallClick:
			LogGAEvent ("Tapdaq:Overall:Click");
			break;	
		case TapdaqGAEvents.CPOverallClose:
			LogGAEvent ("Tapdaq:Overall:Close");
			break;	

		}
	}

	private static void LogGAEvent (string EventType)
	{
		//TODO: Review this if there's a problem logging events to GA
		#if UNITY_ANDROID || UNITY_IPHONE
			GAManager.Instance.LogDesignEvent (EventType);			
		#endif

		doLog(EventType);



	}

	private static void doLog(string LogText){
		Debug.Log (LogText);
	}
}


