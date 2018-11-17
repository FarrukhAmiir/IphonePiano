using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	void OnEnable()
	{
		Debug.Log ("Im Enable");
	}
	void OnDisable()
	{
		Debug.Log ("Im OnDisable");
	}

	void Awake()
	{
		Debug.Log ("Im Awake");
	}


	// Use this for initialization
	void Start () {
		Debug.Log ("Im Start");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Im Upate");
		transform.Translate (1, 0, 0);
	}

	void LateUpdate()
	{
		Debug.Log ("Im Late");
	}
	void OnDestroy()
	{
		Debug.Log ("Im OnDestroy");
	}



}
