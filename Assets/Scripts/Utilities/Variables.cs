using UnityEngine;
using System.Collections;

public class Variables
{
	
	// Player Outfit Selection
	public static int currSelectionIndex = 0;
	public static int totalCats = 3;
	public static int totalHats = 5;
	public static int totalGlasses = 5;
	public static int totalShoes = 5;
	
	public static bool questSuccessful = true;

	// Game Manager Status
	public static int currentPackageCoins = 0;
	public static int currentPackageGems = 0;
	public static bool challengeState = false;
	public static bool isLevelComplete = false;

	public static void Reset ()
	{
		//	playerID = 0;
		currSelectionIndex = 0;
		totalCats = 3;
		totalHats = 5;
		totalGlasses = 5;
		totalShoes = 5;

		questSuccessful = true;

		// Game Manager Status
		currentPackageCoins = 0;
		currentPackageGems = 0;

		challengeState = false;
	
	}
}