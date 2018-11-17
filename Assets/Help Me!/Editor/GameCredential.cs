//C# Example
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{


	int selectedUnityVersion = 0;

	int selectedAd = 0;
	string adsVersion = "";
	bool intersitail=false;
	bool VideoAd=false;
	bool RewardedAd=false;
	bool CategoryCrossPromotion=false;
	bool OverallCrossPromotion=false;

	string keystorePassword = "";

	int count =0;



	string[] unityVersions = new string[]
	{
		"NONE","Unity 4.1","Unity 4.2","Unity 4.3","Unity 4.4","Unity 4.5","Unity 4.6","Unity 4.6.1","Unity 4.6.2",
		"Unity 4.6.3","Unity 4.6.4","Unity 4.6.5","Unity 4.6.6","Unity 5.0","Unity 5.1.0","Unity 5.2.0","Unity 5.3.0",
		"Unity 5.3.2","Unity 5.3.3","Unity 5.3.4","Unity 5.4.0","Unity 5.5.0","Unity 5.6.0"
	};

	string[] Ads = new string[]
	{
		"NONE","Tapdaq","Mopub","Tapdaq+Mopub"
	};




	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("HelpMe!/GameCredential")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(MyWindow));
	}
	
	void OnGUI()
	{
		GUILayout.Label ("GameCredential", EditorStyles.boldLabel);




	


		selectedUnityVersion = EditorGUILayout.Popup("Unity Version", selectedUnityVersion, unityVersions); 


		selectedAd = EditorGUILayout.Popup("Ads", selectedAd, Ads); 
		if(selectedAd!=0)
		{
			GUILayout.BeginVertical("box");
			adsVersion =   EditorGUILayout.TextField ("Ads Version", adsVersion);
			intersitail = EditorGUILayout.Toggle ("intersitailAd", intersitail);
			VideoAd = EditorGUILayout.Toggle ("VideoAd", VideoAd);
			RewardedAd = EditorGUILayout.Toggle ("RewardedAd", RewardedAd);
			CategoryCrossPromotion = EditorGUILayout.Toggle ("CategoryCrossPromotion", CategoryCrossPromotion);
			OverallCrossPromotion = EditorGUILayout.Toggle ("OverallCrossPromotion", OverallCrossPromotion);
			GUILayout.EndVertical();

		}

		keystorePassword =   EditorGUILayout.TextField ("Keystore Password", keystorePassword);
	
	}
}