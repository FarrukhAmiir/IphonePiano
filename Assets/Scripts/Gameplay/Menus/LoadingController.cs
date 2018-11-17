using UnityEngine;
using System.Collections;

public class LoadingController : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(UserPrefs.isSound)
		{
			SoundManager.Instance.MuteSound();
		}

		if(UserPrefs.isMusic)
		{
			SoundManager.Instance.MuteMusic();
		}
	}
}
