using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Security.Policy;

public class UserPrefs : MonoBehaviour
{
    //Encryption Keys


    // constants

    public static bool firstTimeGameLaunch = false;

    public static bool isRestoreTransaction = false;
    public static bool isAmazonBuild = false;
    public static bool isIgnoreAds = false;
    public static bool isBannerAdsCreated = false;
	
    public static int currentLevel = 0;
    // level starts from 1
    public static int currentPlayer = 0;
    public static bool UnlockPlayer = false;
    public static bool RevivePlayer = false;
    public static bool UnlockSurvivalMode = false;

    public static bool IsSurvivalModeUnlocked = false;


    public static bool[] playerUnlockArray = { true, false, false, false, false, false };
    public static bool[] episodeUnlockArray = { true, true, true };
	
    public static bool isApplicationResume = false;
    public static int interstitialAdCount = 0;
    public static bool isPlayHavenAdsShowFirstTime = false;
    public static bool isFirstLaunchGoogle = true;
    public static bool signedInGoogle = true;
    public static bool isACHCall = false;
    public static bool isLBCall = false;
    public static bool isGooglePlayCenterAsGuest = true;
    public static int currentCameraControl = 1;

    public static bool accessoriesCallOut = false;
    // Accessories Callout on Player Selection Menu

    //Player
    public static int playerID = 0;

    public static int totalGems = 2;
    //2

    public static bool isTutorialFinished = false;

    public static bool isSound = true;
    public static bool isMusic = true;

    public static int tutorialLevel = 1;

    public static bool isRatesUS = false;

    public static bool isGoogleSignedIn = true;


    public static int[] LevelsStars = new int[]
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };


    public static void Save()
    {	
        LevelsPrefsSave();
        PreviewLabs.PlayerPrefs.SetBool("isIgnoreAds", isIgnoreAds);

        for (int i = 0; i < playerUnlockArray.Length; i++)
        {
            PreviewLabs.PlayerPrefs.SetBool("playerUnlockArray" + i, playerUnlockArray[i]);
        }
	
        //Umar:Encryption
        PreviewLabs.PlayerPrefs.SetInt("totalGems", totalGems);
        PreviewLabs.PlayerPrefs.SetInt("currentPlayer", currentPlayer);
        PreviewLabs.PlayerPrefs.SetBool("isMusic", isMusic);
        PreviewLabs.PlayerPrefs.SetBool("isSound", isSound);

        PreviewLabs.PlayerPrefs.SetBool("IsSurvivalModeUnlocked", IsSurvivalModeUnlocked);

        PreviewLabs.PlayerPrefs.SetInt("currentCameraControl", currentCameraControl);
        PreviewLabs.PlayerPrefs.SetBool("isRatesUS", isRatesUS);
	
        PreviewLabs.PlayerPrefs.SetInt("playerID", playerID);
        PreviewLabs.PlayerPrefs.SetInt("tutorialLevel", tutorialLevel);

        PreviewLabs.PlayerPrefs.SetInt("currentLevel", currentLevel);

        PreviewLabs.PlayerPrefs.SetBool("isTutorialFinished", isTutorialFinished);

        PreviewLabs.PlayerPrefs.SetBool("firstTimeGameLaunch", true);

        PreviewLabs.PlayerPrefs.Flush();
    }

    public static void Load()
    {
        LevelsPrefsLoad();
		
        accessoriesCallOut = PreviewLabs.PlayerPrefs.GetBool("accessoriesCallOut", accessoriesCallOut);
        currentPlayer = PreviewLabs.PlayerPrefs.GetInt("currentPlayer", currentPlayer);
        isIgnoreAds = PreviewLabs.PlayerPrefs.GetBool("isIgnoreAds", isIgnoreAds);
        for (int i = 0; i < playerUnlockArray.Length; i++)
        {
            playerUnlockArray[i] = PreviewLabs.PlayerPrefs.GetBool("playerUnlockArray" + i, playerUnlockArray[i]);	
        }

        totalGems = PreviewLabs.PlayerPrefs.GetInt("totalGems", totalGems);

        isSound = PreviewLabs.PlayerPrefs.GetBool("isSound", isSound);
        isMusic = PreviewLabs.PlayerPrefs.GetBool("isMusic", isMusic);

        IsSurvivalModeUnlocked = PreviewLabs.PlayerPrefs.GetBool("IsSurvivalModeUnlocked", IsSurvivalModeUnlocked);

        currentCameraControl = PreviewLabs.PlayerPrefs.GetInt("currentCameraControl", currentCameraControl);
        isRatesUS = PreviewLabs.PlayerPrefs.GetBool("isRatesUS", isRatesUS);

        tutorialLevel = PreviewLabs.PlayerPrefs.GetInt("tutorialLevel", tutorialLevel);
        playerID = PreviewLabs.PlayerPrefs.GetInt("playerID", playerID);

        isTutorialFinished = PreviewLabs.PlayerPrefs.GetBool("isTutorialFinished", isTutorialFinished);

        currentLevel = PreviewLabs.PlayerPrefs.GetInt("currentLevel", currentLevel);

        firstTimeGameLaunch = PreviewLabs.PlayerPrefs.GetBool("firstTimeGameLaunch", firstTimeGameLaunch);
    }

    public static void LevelsPrefsSave()
    {
        for (int i = 0; i < Constants.levelsPerEpisode; i++)
        {
            PreviewLabs.PlayerPrefs.SetInt("Level" + (i + 1) + "Stars", LevelsStars[i]);
        }
    }

    public static void LevelsPrefsLoad()
    {
        for (int i = 0; i < Constants.levelsPerEpisode; i++)
        {
            LevelsStars[i] = PreviewLabs.PlayerPrefs.GetInt("Level" + (i + 1) + "Stars", LevelsStars[i]);
        }
    }
}