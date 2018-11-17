using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class DragUI : MonoBehaviour {

	public Vector2 intialPositon;  
	ITweenMagic iTween;
	private GameObject DragObject;
	private GameObject DropingArea;
	public GameObject collider;
	private bool inArea;

    public GameObject tutorialHand;
   
	void Start ()
	{
		inArea = true;
		intialPositon = this.gameObject.transform.localPosition;
		iTween = this.gameObject.GetComponent<ITweenMagic> ();
	}

	public void Drag ()
	{
		//For Module Drag to correct shape //
		//this.gameObject.transform.position = Input.mousePosition;

		//For Module car draging
//		if (collider.transform.localPosition.y <= transform.localPosition.y + 60f && collider.transform.localPosition.y >= transform.localPosition.y - 60f )
//		{
//			this.gameObject.transform.position = new Vector3 (Input.mousePosition.x, transform.position.y, 0f);
//			collider.transform.position = Input.mousePosition;
//		}
       
        tutorialHand.SetActive(false);

		if (Input.mousePosition.y <= transform.position.y + 27f && Input.mousePosition.y >= transform.position.y - 27f && inArea) {
			this.gameObject.transform.position = new Vector3 (Input.mousePosition.x, transform.position.y, 0f);
			GameObject.Find ("Wheel1").transform.Rotate (0, 0, -25);
			GameObject.Find ("Wheel2").transform.Rotate (0, 0, -25);

			//collider.transform.position = Input.mousePosition;
		} else 
		{
			inArea = false;
		}


		Destroy (this.gameObject.GetComponent<iTween> ());
		DragObject = gameObject;

	}


	public void PointerUp()
	{

       
      
      
		inArea = true;
        if (iTween)
        {
            iTween.enabled = false;
            iTween.initialPosition2D = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y);
            iTween.targetPosition2D = new Vector2(intialPositon.x, intialPositon.y);
            iTween.enabled = true;
        }
        else
        {
			this.transform.position = intialPositon;

//            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200f, ForceMode2D.Impulse);
//            CarsOnRoad.tutorial = true;
		}

     ///   tutorialHand.SetActive(!CarsOnRoad.tutorial);
		//collider.transform.position = this.gameObject.transform.position;

	}

	public void SnapPointerUp()
	{
		//DropingArea = GameObject.Find (DragObject.GetComponent<BookShape> ().name);
		Debug.Log (DropingArea.name);
		float Distance = Vector3.Distance (DropingArea.transform.position, DragObject.transform.position);
	
		if (Distance < 20) {
			DragObject.transform.position = DropingArea.transform.position;
			Destroy(DragObject.GetComponent<EventTrigger>());
			           
		} else
		{
            
			PointerUp ();
		}

	}


}