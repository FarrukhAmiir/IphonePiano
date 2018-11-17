using UnityEngine;
using System.Collections;

public class RateUsMenu : MonoBehaviour {

	public GameObject[] Stars;
	int clicked = 0;



	public void StarsClick(int ID)
	{
		clicked = ID;
		for(int i = 0 ; i < ID ; i++)
		{
			Stars[i].transform.GetChild(0).gameObject.SetActive(true);
		}
		Invoke("GotoRateUsPage",1.5f);
	}

	public void GotoRateUsPage()
	{
//		if(clicked>3 || clicked == 0)
//		{
			Application.OpenURL(Constants.ANDROID_RATEUS_LINK);
	//	}

		UserPrefs.isRatesUS = true;
		UserPrefs.Save();

		ShowLevelCompletePopup();
		Destroy(gameObject);

	}

	public void Later()
	{
		ShowLevelCompletePopup();
		Destroy(gameObject);
	}

	void ShowLevelCompletePopup()
	{
		if(!GameObject.FindWithTag(Tags.LevelExit))
			Time.timeScale = 1;

		GAManager.Instance.LogDesignEvent("GamePlay:LevelComplete:"+ (Constants.selectedLevel+1));

		Instantiate(Resources.Load(Constants.LevelComplete));
	}
}
