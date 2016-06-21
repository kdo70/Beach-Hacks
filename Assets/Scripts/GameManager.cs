using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager> {
	public Text title;
	public Text subtitle;
	public Text timeText;
	public Text healthText;
	public Text creditsButtonText;
	public GameObject creditsText;
	public GameObject subtitleButton;
	public GameObject resetBestTimeButton;
	public GameObject creditsButton;
	public GameObject backButton;

	private float blinkTime = 0f;
	private bool blink;

	public static float timeElapsed = 0f;
	private float localHealth;

	public GameObject playerPrefab;
	private Ball ball;

	public static bool gameStarted;
	private GameObject player;
	private TimeManager timeManager;
	private Spawner spawner;

	public AudioClip[] clips;
	private AudioSource audioManager;

	new void Awake() {
		audioManager = GetComponent<AudioSource> ();
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
		ball = playerPrefab.GetComponent<Ball> ();
	}

	void Start () {
		Application.targetFrameRate = 60;
		spawner.active = false;
		Time.timeScale = 0;
		creditsText.SetActive (false);
		creditsButtonText.text = "Credits";
		subtitle.text = "Press here to Continue";
		title.text = "Skulls and Asteroids";
		MenuUI ();
		PlayMusic (0);
	}
	

	void Update () {
		if(!gameStarted){
			TextBlink (subtitle);

		} else {
			timeElapsed += Time.deltaTime;
			if (localHealth != ball.GetBallHealth ()) {
				localHealth = ball.GetBallHealth ();
				healthText.text = "Health: " + localHealth;
			}
			timeText.text = "Time: " + FormatTime(timeElapsed);
			if(ball.GetBallHealth () <= 0) {
				OnPlayerKilled ();
			}
		}
	}

	public void ResetBestTime () {
		PlayerPrefs.SetFloat ("BestTime", 0);
		timeText.text = "Best Time: " + FormatTime (PlayerPrefs.GetFloat("BestTime"));
	}

	void OnPlayerKilled  () {
		timeManager.ManipulateTime (0, 5.5f);
		spawner.active = false;
		PlayMusic (0);
		gameStarted = false;
		MenuUI ();
	}

	void BeatBestTime () {
		if (timeElapsed > PlayerPrefs.GetFloat("BestTime")) {
			healthText.canvasRenderer.SetAlpha (1);
			healthText.text = "Old Best Time:" + FormatTime (PlayerPrefs.GetFloat ("BestTime"));
			timeText.text = "New Best Time:" + FormatTime (timeElapsed);
			PlayerPrefs.SetFloat ("BestTime", timeElapsed);
		} else {
			timeText.text = "Best Time: " + FormatTime (PlayerPrefs.GetFloat("BestTime"));
		}
		timeElapsed = 0;
	}		

	public void ResetGame() {
		timeManager.ManipulateTime (1, 1f);
		spawner.active = true;
		PlayMusic (1);
		gameStarted = true;
		InGameUI ();
		ball.ResetHealth ();
	}

	void InGameUI () {
		healthText.canvasRenderer.SetAlpha (1);
		title.canvasRenderer.SetAlpha (0);
		subtitle.canvasRenderer.SetAlpha (0);
		subtitleButton.SetActive(false);
		resetBestTimeButton.SetActive(false);
		creditsButton.SetActive (false);
	}

	public void MenuUI () {
		subtitleButton.SetActive (true);
		healthText.canvasRenderer.SetAlpha (0);
		title.canvasRenderer.SetAlpha (1);
		BeatBestTime ();
		subtitleButton.SetActive (true);
		resetBestTimeButton.SetActive(true);
		creditsButton.SetActive (true);
		creditsText.SetActive (false);
		backButton.SetActive (false);
	}

	public void CreditsUI () {
		title.canvasRenderer.SetAlpha (0);
		healthText.canvasRenderer.SetAlpha (0);
		subtitleButton.SetActive (false);
		timeText.text = "";
		resetBestTimeButton.SetActive(false);
		creditsText.SetActive (true);
		backButton.SetActive (true);
		creditsButton.SetActive (false);
	}

	void PlayMusic (int song) {
		audioManager.clip = clips [song];
		audioManager.Play ();
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
