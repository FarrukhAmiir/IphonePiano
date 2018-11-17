using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    // The target we are following
    [SerializeField]
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target

    public float height = 5.0f;
		
    // The distance in the x-z plane to the target
    public float RotationY;
	
    public float rotationDamping;
    public float heightDamping;
		
    public bool Free;
    public bool isTutorial = false;

    public Text Height, Distance;

    public GameObject firstPerson, thirdPerson;

    Transform currentLevelFinalCameraPosition;
    Transform cameraFailTransform;

    // Use this for initialization
    void Start()
    {
     
    }
    // Update is called once per frame
    void LateUpdate()
    {
        //RotateCamera();
        // Early out if we don't have a target
        if (!target)
            return;
	
        resetY();
        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        // Damp the rotation around the y-axis

        if (!Free)
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, (wantedRotationAngle + RotationY), rotationDamping * Time.deltaTime);
//			if(Free)
//				currentRotationAngle = currentRotationAngle;
//			// Damp the height
        if (!Free)
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
//			if(Free)
//				currentHeight = currentHeight;
//
        // Convert the angle into a rotation
        var currentRotation = new Quaternion();
			
        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        if (!Free)
        {
            transform.position = target.position;
            transform.position -= currentRotation * Vector3.forward * distance;
			
            // Set the height of the camera
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        }


        if (!Free)
        {
            // Always look at the target
            transform.LookAt(target);
        }

        if (Free)
        {
            
            transform.position = cameraFailTransform.position;
            transform.rotation = cameraFailTransform.rotation;
            // transform.position = new Vector3(transform.position.x,5,transform.position.z);
            // transform.localEulerAngles = new Vector3(25,0,0);
             
					
        }

//			Height.text = "H : " + height;
//			Distance.text = "D : " + distance;
    }

    void resetY()
    {
        if (RotationY > 360)
            RotationY = 0;
        else if (RotationY < -360)
            RotationY = 0;
        if (RotationY > 180)
            RotationY = 180 - 360;
        if (RotationY < -180)
            RotationY = 360 - 180;
    }

    public void HeightChange(bool Change)
    {
        if (Change)
        {
            height = height + 0.3f;
        }

        if (!Change)
        {
            height = height - 0.3f;
        }
    }

    public void DistanceChange(bool Change)
    {
        if (Change)
        {
            distance = distance + 0.3f;
        }

        if (!Change)
        {
            distance = distance - 0.3f;
        }
    }

    public void ThirdPerson()
    {
        rotationDamping = 0;
        thirdPerson.SetActive(true);
        firstPerson.SetActive(false);
        target = thirdPerson.transform;
    }

    public void FirstPerson()
    {
        rotationDamping = 15;
        thirdPerson.SetActive(false);
        firstPerson.SetActive(true);
        target = firstPerson.transform;
    }

    bool end = false;
    float StartMousePoint = 0;
    float EndMousePoint = 0;

    public void RotateCamera()
    {
        
    }

}