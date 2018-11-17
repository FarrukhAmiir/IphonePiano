using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Sound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


   

    AudioSource audio;
    // Use this for initialization
    
    public Sprite UnPressed,Pressed;
    public void OnMouseOver()
    {
        //OnClick();
        ////do stuff
    }
    void Start () {
        //CreateInstances();
       
    }
	
	// Update is called once per frame
	void Update () {

       
    }
    public void OnClick()
    {
        audio = transform.GetComponent<AudioSource>();
        audio.enabled = true;
        audio.Play();
        
    }



    public void OnButtonClicked(int buttonIndex)
    {

        transform.GetComponent<Image>().sprite = UnPressed;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        transform.GetComponent<Image>().sprite = Pressed;
        OnClick();

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        transform.GetComponent<Image>().sprite = UnPressed;

    }

}
