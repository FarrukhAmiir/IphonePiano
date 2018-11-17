using UnityEngine;
using System.Collections;

public class DisableAfter : MonoBehaviour {

	public float secondsToDisable;

	// Use this for initialization
	void OnEnable () {

        Invoke("Disable", secondsToDisable);
		
	}
	
	// Update is called once per frame
	public void Disable()
    {
       gameObject.SetActive(false);
    }
}
