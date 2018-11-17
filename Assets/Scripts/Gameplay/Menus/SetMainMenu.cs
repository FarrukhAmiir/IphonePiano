using UnityEngine;
using System.Collections;

public class SetMainMenu : MonoBehaviour
{


    public GameObject Target;
    bool hit;
    // Use this for initialization
    void Start()
    {


        Invoke("SlowDownTimeScale", 1);
	
    }

    void SlowDownTimeScale()
    {
        Time.timeScale = 0.2f;    
    }
	
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag(Tags.MainMenu))
        {
            if (Time.timeScale >= 1)
            {
                Invoke("SlowDownTimeScale", 1);
            }
        }
    }


    void AttackComboEndEvent()
    {
        
    }

    void AttackStartEvent()
    {
        
    }

    void AttackHitEvent()
    {
        
        if (hit)
        {
            Target.GetComponent<Animator>().SetTrigger("hit");
            Instantiate(Resources.Load("Effects/HitEffect1"), Target.transform.position + new Vector3(-4, 6, 0), Quaternion.identity);
        }
        hit = !hit;
        hitsound();
    }

    void hitsound()
    {
       //SoundManager.Instance.PlaySound(GameManager.SoundState.ALIENBULLET1);
    }
}
