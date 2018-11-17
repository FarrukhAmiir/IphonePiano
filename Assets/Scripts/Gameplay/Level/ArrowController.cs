using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    public GameObject arrow_renderer;
    public Transform target;
    float distance = 10000;

    // Use this for initialization
    void Start()
    {
        // DisableArrow();
        Invoke("getTarget", 1.0f);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (target && target.gameObject.activeInHierarchy)
        {
            Vector3 pos = target.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(pos);
            rot.z = 0;

            transform.rotation = rot;
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        else
        {
            getTarget();
        }
    }

    public void getTarget()
    {
        if (GameObject.FindWithTag(Tags.NPC))
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag(Tags.NPC))
            {
                if (Vector3.Distance(GameObject.FindGameObjectWithTag(Tags.player).transform.position, g.transform.position) < distance)
                {
                  
                    
                }
            }
        }
        else
            target = GameObject.FindGameObjectWithTag(Tags.Fence).transform;

        Hashtable table = new Hashtable();

        table["scale"] = new Vector3(transform.lossyScale.x - 0.02f, transform.lossyScale.y, transform.lossyScale.z);
        table["time"] = 0.3f;
        table["looptype"] = "pingPong";
        table["easetype"] = "easeInOutBack";
        table["delay"] = 0.1f;


        // iTween.ScaleTo(gameObject,table);
    }


    public void EnableArrow()
    {
        arrow_renderer.SetActive(true);
        //arrow_renderer.GetComponent<Animator>().enabled = true;
    }

    public void DisableArrow()
    {
        arrow_renderer.SetActive(false);
        //arrow_renderer.enabled = false;
    }
}
