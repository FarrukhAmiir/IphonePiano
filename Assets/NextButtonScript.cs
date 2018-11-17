using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour {
	public GameObject nextButton;
	public GameObject backButton;
	public int back, next;
	public int NumberOfImages;


	void Start () {
//		nextButton.SetActive(true);
//		nextButton.SetActive(true);


	}
	
	// Update is called once per frame
	void Update () {
        if (back >= NumberOfImages)
        {
            backButton.GetComponent<Button>().enabled = false;
            backButton.GetComponent<Image>().color = new Color(100, 100, 100);
        }
        else backButton.GetComponent<Button>().enabled = true;

        if (next >=NumberOfImages-1) 
		{
            nextButton.GetComponent<Button>().enabled = false;
            nextButton.GetComponent<Image>().color = new Color(100, 100, 100);
        }
        else nextButton.GetComponent<Button>().enabled = true;

	}
	public void onClickN()
	{
		next++;
		back--;
		Debug.Log ("next"+next);
	}

	public void onClickB()
	{
		back++;
		next--;
		Debug.Log ("back"+back);
	}
}
