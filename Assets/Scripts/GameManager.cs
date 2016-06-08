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
	public Button creditsButtonOnClick;

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
			healthText.text = "Health: " + ball.health;
			timeText.text = "Time: " + FormatTime(timeElapsed);
			if(ball.health <= 0) {
				OnPlayerKilled ();
			}
		}
	}

	public void ResetBestTime () {
		PlayerPrefs.SetFloat ("BestTime", 0);
	}

	void OnPlayerKilled  () {
		timeManager.ManipulateTime (0, 5.5f);
		spawner.active = false;
		PlayMusic (0);
		gameStarted = false;
		MenuUI ();
		BeatBestTime ();
	}

	void BeatBestTime () {
		if (timeElapsed > PlayerPrefs.GetFloat("BestTime")) {
			PlayerPrefs.SetFloat ("BestTime", timeElapsed);
		}
	}		

	public void ResetGame() {
		timeManager.ManipulateTime (1, 1f);
		spawner.active = true;
		PlayMusic (1);
		gameStarted = true;
		InGameUI ();
		ball.health = 100;
		timeElapsed = 0;

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
		timeText.text = "Best Time: " + FormatTime (PlayerPrefs.GetFloat("BestTime"));
		subtitleButton.SetActive (true);
		resetBestTimeButton.SetActive(true);
		creditsButton.SetActive (true);
		creditsText.SetActive (false);
		creditsButtonOnClick.onClick.RemoveListener (() => MenuUI ());
		creditsButtonOnClick.onClick.AddListener (() => CreditsUI ());
	}

	public void CreditsUI () {
		title.canvasRenderer.SetAlpha (0);
		subtitleButton.SetActive (false);
		timeText.text = "";
		creditsButtonText.text = "Back";
		resetBestTimeButton.SetActive(false);
		creditsText.SetActive (true);
		creditsButtonOnClick.onClick.RemoveListener (() => CreditsUI ());
		creditsButtonOnClick.onClick.AddListener (() => MenuUI ());
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
