using UnityEngine;
using System.Collections;

public class playMovie : MonoBehaviour
{


    public string FileName = "";
    public GameObject FakeSplash;
    AsyncOperation async;

    // Use this for initialization
    void Start()
    {
        GAManager.Instance.LogDesignEvent("GameLaunch:Splash");		

        StartCoroutine("load");

        if (!FileName.Contains(".mp4"))
        {
            FileName = FileName + ".mp4";
        }

        StartCoroutine(PlayStreamingVideo(FileName));
        ActivateScene();
    }

    private IEnumerator PlayStreamingVideo(string url)
    {

        Handheld.PlayFullScreenMovie(url, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFill);
        yield return new WaitForEndOfFrame();


        FakeSplash.SetActive(true);
        GAManager.Instance.LogDesignEvent("GameLaunch:SplashShown");		
        Destroy(this.gameObject);
    }

    IEnumerator load()
    {
        async = Application.LoadLevelAsync(1);
        async.allowSceneActivation = false;
        yield return async;


    }

    public void ActivateScene()
    {
        async.allowSceneActivation = true;
    }

}
