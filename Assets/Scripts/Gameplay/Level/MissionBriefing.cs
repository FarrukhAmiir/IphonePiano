using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionBriefing : MonoBehaviour
{


	public GameObject ControllerHud, MenuHud, TutorialHud, MissionBriefingHud, MapRadar, Joystick;
	public Camera MainGameCamera, MissionCamera;
	public GameObject[] MissionBriefingLevel;
	public Text MissionDiscriptionTxt;

	void Start ()
	{
//		if(!GameObject.FindWithTag(Tags.LevelExit))
//			Time.timeScale = 1;
//
//		MissionCamera.enabled = true;
//		if(MapRadar)
//		{
//			MapRadar.GetComponent<CanvasGroup>().alpha = 0;
//			MapRadar.GetComponent<CanvasGroup>().interactable = false;
//		}
//		MainGameCamera.enabled = false;
//		if(ControllerHud)
//		{
//			ControllerHud.GetComponent<CanvasGroup>().alpha = 0;
//			ControllerHud.GetComponent<CanvasGroup>().interactable = false;
//		}
//		if(MenuHud)
//			MenuHud.SetActive(false);
//		if(TutorialHud)
//			TutorialHud.SetActive(false);
//
//		MissionBriefingHud.SetActive(true);
//
//		LevelDiscription();
	}

	void LevelDiscription ()
	{
		MissionBriefingLevel [Constants.selectedLevel].SetActive (true);
		StartCoroutine (MissionDiscription ());
	}

	IEnumerator MissionDiscription ()
	{
		MissionCamera.transform.position = MissionBriefingLevel [Constants.selectedLevel].transform.GetChild (0).position;
		MissionCamera.transform.localEulerAngles = MissionBriefingLevel [Constants.selectedLevel].transform.GetChild (0).localEulerAngles;
		StartCoroutine (TypeWriter (Constants.MissionBriefing [Constants.selectedLevel])); 
		yield return new WaitForSeconds (7);
		if (MissionBriefingLevel [Constants.selectedLevel].transform.childCount >= 2) {
			MissionCamera.transform.position = MissionBriefingLevel [Constants.selectedLevel].transform.GetChild (1).position;
			MissionCamera.transform.localEulerAngles = MissionBriefingLevel [Constants.selectedLevel].transform.GetChild (1).localEulerAngles;
			StartCoroutine (TypeWriter (Constants.MissionBriefing [Constants.selectedLevel])); 
			yield return new WaitForSeconds (5);
		}
		//ActiveAll();
	}

	public void ActiveAll ()
	{
		MissionCamera.enabled = false;
		MainGameCamera.enabled = true;
		if (ControllerHud) {
			ControllerHud.GetComponent<CanvasGroup> ().alpha = 1;
			ControllerHud.GetComponent<CanvasGroup> ().interactable = true;
		}
		if (MenuHud)
			MenuHud.SetActive (true);
		if (TutorialHud)
			TutorialHud.SetActive (true);

		MissionBriefingHud.SetActive (false);
		if (MapRadar) {
			MapRadar.GetComponent<CanvasGroup> ().alpha = 1;
			MapRadar.GetComponent<CanvasGroup> ().interactable = true;
		}
	}


	public void OnMissionEnd ()
	{
		//MissionCamera.enabled = true;
		if (MapRadar != null) {
			MapRadar.GetComponent<CanvasGroup> ().alpha = 0;
			MapRadar.GetComponent<CanvasGroup> ().interactable = false;
		}
		Time.timeScale = 0;
		if (ControllerHud != null) {
			ControllerHud.GetComponent<CanvasGroup> ().alpha = 0;
			ControllerHud.GetComponent<CanvasGroup> ().interactable = false;
		}
		if (MenuHud != null)
			MenuHud.SetActive (false);
		if (TutorialHud != null)
			TutorialHud.SetActive (false);
		if (MissionBriefingHud != null)
			MissionBriefingHud.SetActive (false);
		if (Joystick != null)
			Joystick.SetActive (false);

	}

	public void ActiveOnRevive ()
	{
		//MissionCamera.enabled = true;
		if (MapRadar != null) {
			MapRadar.GetComponent<CanvasGroup> ().alpha = 1;
			MapRadar.GetComponent<CanvasGroup> ().interactable = true;
		}
		Time.timeScale = 0;
		if (ControllerHud != null) {
			ControllerHud.GetComponent<CanvasGroup> ().alpha = 1;
			ControllerHud.GetComponent<CanvasGroup> ().interactable = true;
		}
		if (MenuHud != null)
			MenuHud.SetActive (true);
		if (TutorialHud != null)
			TutorialHud.SetActive (true);
		if (MissionBriefingHud != null)
			MissionBriefingHud.SetActive (true);
		if (Joystick != null)
			Joystick.SetActive (true);

	}

	public void Skip ()
	{
//		AdsManager.Instance.RequestForMopubAd();
		ActiveAll ();
	}

	private IEnumerator TypeWriter (string stringlevelText)
	{	
		int index = 0;
		string str = string.Empty;
		
		while (index < stringlevelText.Length) {
			str += stringlevelText [index++];
			MissionDiscriptionTxt.text = str;
			yield return new WaitForSeconds (0.02f);
		}

		yield return new WaitForSeconds (2f);
		//Destroy (gameObject);
		
	}
}
