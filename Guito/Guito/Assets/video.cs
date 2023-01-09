using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playbackVideoHandHeld : MonoBehaviour {

	private string movie = "final.ogv";
	public int sceneNumber;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(streamVideo(movie));
	}
	private IEnumerator streamVideo(string video)
	{
		Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
		yield return new WaitForEndOfFrame();
		Debug.Log("The Video playback is now completed.");
		SceneManager.LoadScene ("CenaMenuIntro");
	}}