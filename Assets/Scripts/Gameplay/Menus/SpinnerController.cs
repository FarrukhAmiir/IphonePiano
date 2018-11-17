using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpinnerController : MonoBehaviour
{
	RectTransform spinner;
	AsyncOperation operation = null;
	// Use this for initialization
	void Start ()
	{
		spinner = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (operation != null)
		//iTween.RotateBy (gameObject, iTween.Hash ("z", -180.0f, "time", 300f, "easetype", "linear", "looptype", iTween.LoopType.loop));
//		iTween.RotateUpdate (gameObject, new Vector3 (0, 0, 360), 2);
//			spinner.localRotation = Quaternion.Euler (new Vector3 (0, 0, Time.deltaTime * ));
	}


	public void SetOperation (AsyncOperation operation)
	{
		this.operation = operation;
	}
}
