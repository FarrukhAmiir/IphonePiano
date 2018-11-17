using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


/// <summary>
/// Unity VideoPlayer Script for Unity 5.6 (currently in beta 0b11 as of March 15, 2017)
/// Blog URL: http://justcode.me/unity2d/how-to-play-videos-on-unity-using-new-videoplayer/
/// YouTube Video Link: https://www.youtube.com/watch?v=nGA3jMBDjHk
/// StackOverflow Disscussion: http://stackoverflow.com/questions/41144054/using-new-unity-videoplayer-and-videoclip-api-to-play-video/
/// Code Contiburation: StackOverflow - Programmer
/// </summary>


public class StreamVideo : MonoBehaviour {

	public RawImage image;

	public VideoClip videoToPlay;
	public VideoClip[] Videos;
	private VideoPlayer videoPlayer;
	private VideoSource videoSource;

	private AudioSource audioSource;

	// Use this for initialization
//	void Start () {
//		Application.runInBackground = true;
//		StartCoroutine(playVideo());
//	}

	void OnEnable()
	{
		//VideoSelector ();
		Application.runInBackground = true;
		StartCoroutine(playVideo());
	}

	void OnDisable ()
	{
		Destroy (GetComponent<VideoPlayer> ());
		Destroy (GetComponent<AudioSource> ());
	}


//	void VideoSelector()
//	{
//		Activity a = Variables.subjects [Variables.SubjectKey - 1].days [Variables.DayKey - 1].activities [Variables.ActivityKey];
//
//
//		for (int i = 0; i < Videos.Length; i++) 
//		{
//
//			if (Videos [i].name == a.type) {
//				videoToPlay = Videos [i];
//				break;
//			} else
//				videoToPlay = Videos [0];
//		}
//
//
//	}

	IEnumerator playVideo()
	{

		//Add VideoPlayer to the GameObject
		videoPlayer = gameObject.AddComponent<VideoPlayer>();
		videoPlayer.waitForFirstFrame = false;
		//Add AudioSource
		audioSource = gameObject.AddComponent<AudioSource>();

		//Disable Play on Awake for both Video and Audio
		videoPlayer.playOnAwake = true;
		audioSource.playOnAwake = false;
		audioSource.Pause();

		//We want to play from video clip not from url

		videoPlayer.source = VideoSource.VideoClip;

		// Vide clip from Url
		//videoPlayer.source = VideoSource.Url;
		//videoPlayer.url = "http://www.quirksmode.org/html5/videos/big_buck_bunny.mp4";


		//Set Audio Output to AudioSource
		videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

		//Assign the Audio from Video to AudioSource to be played
		videoPlayer.EnableAudioTrack(0, true);
		videoPlayer.SetTargetAudioSource(0, audioSource);

		//Set video To Play then prepare Audio to prevent Buffering
		videoPlayer.clip = videoToPlay;
		videoPlayer.Prepare();

		//Wait until video is prepared
		while (!videoPlayer.isPrepared)
		{
			yield return null;
		}

		Debug.Log("Done Preparing Video");

		//Assign the Texture from Video to RawImage to be displayed
		image.texture = videoPlayer.texture;

		//Play Video
		videoPlayer.Play();

		//Play Sound
		audioSource.Play();

		Debug.Log("Playing Video");
		while (videoPlayer.isPlaying)
		{
			Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
			yield return null;
		}

		Debug.Log("Done Playing Video");
	}

	// Update is called once per frame
	void Update () {

	}




}