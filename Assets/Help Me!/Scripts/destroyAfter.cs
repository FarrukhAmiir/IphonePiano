using UnityEngine;
using System.Collections;

public class destroyAfter : MonoBehaviour {

	public float secondsToDestroy;

	// Use this for initialization
	void Start () {
	
		Destroy (this.gameObject, secondsToDestroy);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
