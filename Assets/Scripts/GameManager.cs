using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager> {
	public Text title;
	public Text subtitle;
	public Text timeText;
	public Text healthText;

	private float blinkTime = 0f;
	private bool blink;

	public static float timeElapsed = 0f;

	public GameObject playerPrefab;
	private Ball ball;

	public static bool gameStarted;
	private GameObject player;
	private TimeManager timeManager;
	private Spawner spawner;

	public AudioClip[] clips;
	private AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource> ();
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
		ball = playerPrefab.GetComponent<Ball> ();
	}

	void Start () {
		spawner.active = false;
		Time.timeScale = 0;

		subtitle.text = "Click To Start";
		title.text = "Skulls and Asteroids";

		PlayMusic (0);
	}
	

	void Update () {
		
		if(!gameStarted && Time.timeScale == 0){
			if(Input.anyKeyDown){
				timeManager.ManipulateTime (1, 1f);
				ResetGame ();
			}
		}

		if(!gameStarted){
			TextBlink (subtitle);
			MenuText ();

		} else {
			timeElapsed += Time.deltaTime;
			InGameText ();
			if(ball.health <= 0) {
				OnPlayerKilled ();
			}
		}
	}

	void OnPlayerKilled(){
		spawner.active = false;

		PlayMusic (0);

		timeManager.ManipulateTime (0, 5.5f);
		gameStarted = false;

		BeatBestTime ();
	}

	void BeatBestTime () {
		if (timeElapsed > PlayerPrefs.GetFloat("BestTime")) {
			PlayerPrefs.SetFloat ("BestTime", timeElapsed);
		}
	}		

	void ResetGame() {
		ball.health = 100;
		spawner.active = true;
		PlayMusic (1);
		gameStarted = true;

		timeElapsed = 0;
	}

	void InGameText () {
		healthText.canvasRenderer.SetAlpha (1);
		healthText.text = "Health: " + ball.health;
		title.canvasRenderer.SetAlpha (0);
		subtitle.canvasRenderer.SetAlpha (0);
		timeText.text = "Time: " + FormatTime(timeElapsed);
	}

	void MenuText () {
		healthText.canvasRenderer.SetAlpha (0);
		title.canvasRenderer.SetAlpha (1);
		timeText.text = "Best Time: " + FormatTime (PlayerPrefs.GetFloat("BestTime"));
	}

	void PlayMusic (int song) {
		audio.clip = clips [song];
		audio.Play ();
	}

	void TextBlink (Text text) {
		blinkTime++;
		if (blinkTime % 40 == 0) {
			blink = !blink;
		}
		text.canvasRenderer.SetAlpha (blink ? 0 : 1);
	}

	string FormatTime(float value) {
		TimeSpan t = TimeSpan.FromSeconds (value);

		return string.Format("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
	}

}
