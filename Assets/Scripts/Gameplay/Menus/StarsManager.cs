using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StarsManager : MonoBehaviour {

	// Use this for initialization

	[HideInInspector]
	public int PerfectJumpCount;
	float result;

	[HideInInspector]
	public int totalStars;

	void Start () {
		//hurdles_Parent=GameObject.FindWithTag(Tags.HurdleParent).transform;
		//Constants.Hurdles_per_Level=hurdles_Parent.childCount;
	}

	public void ShowStars()
	{
		AssignStars(3);
		totalStars = 3;
	}//End of ShowStars

	void AssignStars(int Stars)
	{
//		int tempReward=Constants.LEVEL_COMPLETE_REWARD;
		//Constants.LEVEL_COMPLETE_REWARD=300;
		//Constants.TOTAL_LEVEL_COMPLETE_REWARD=(Stars*Constants.STARS_REWARD)+Constants.LEVEL_COMPLETE_REWARD;
		// if(Variables.currentModeType==Variables.ModeType.DoubleBetMode && UserPrefs.isDoubleRewardPurchased)
		// {
		// 	//Doublebet Mode with Double Reward
		// 	//first *2 is for DoubleBetMode and 2nd *2 is for Double Reward Booster
		// 	GameManager.Instance.AddCoins(Constants.TOTAL_LEVEL_COMPLETE_REWARD*2*2);
			
		// }
		// else if(Variables.currentModeType==Variables.ModeType.DoubleBetMode)
		// {
		// 	//Doublebet Mode with Double Reward
		// 	GameManager.Instance.AddCoins(Constants.TOTAL_LEVEL_COMPLETE_REWARD*2);
			
		// }
		// else if(Variables.currentModeType==Variables.ModeType.SurvivalMode && UserPrefs.isDoubleRewardPurchased)
		// {
		// 	//first *3 is for EliminationMode and 2nd *2 is for Double Reward Booster
		// 	GameManager.Instance.AddCoins(Constants.TOTAL_LEVEL_COMPLETE_REWARD*3*2);
		// }
		// else if(Variables.currentModeType==Variables.ModeType.SurvivalMode)
		// {
		// 	GameManager.Instance.AddCoins(Constants.TOTAL_LEVEL_COMPLETE_REWARD*3);
		// }
		// else if(UserPrefs.isDoubleRewardPurchased)
		// {
		// 	//if Double Reward Booster is Purchased
		// 	GameManager.Instance.AddCoins(Constants.TOTAL_LEVEL_COMPLETE_REWARD*2);
		// }
		// else
		// {
		// 	//Others Normal Reward
		// 	//Debug.Log("Constants.TOTAL_LEVEL_COMPLETE_REWARD="+Constants.TOTAL_LEVEL_COMPLETE_REWARD);
		// 	GameManager.Instance.AddCoins(/*(UserPrefs.currentLevel+1)**/Constants.TOTAL_LEVEL_COMPLETE_REWARD);
		// 	UserPrefs.Save();
		// }
		//Debug.Log("TOTAL_LEVEL_COMPLETE_REWARD"+Constants.TOTAL_LEVEL_COMPLETE_REWARD);
		//StartCoroutine(StarsAnimations(Stars));
		for(int i=0;i<Constants.levelsPerEpisode;i++)
		{
			if(i==Constants.selectedLevel)
			{
				// if(UserPrefs.LevelsStars[i]<Stars)
				// {
					UserPrefs.LevelsStars[i]=3;
					UserPrefs.Save();
				//}
				break;
			}
		}

	}//End of AssignStars


	IEnumerator StarsAnimations(int breakPointValue){

	yield return null;

	}

}
