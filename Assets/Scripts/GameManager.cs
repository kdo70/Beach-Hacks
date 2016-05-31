using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : Singleton<GameManager> {

	public Text continueText;
	private float blinkTime = 0f;
	private bool blink;
	private GameObject resetBestScoreButton;

	public Text scoreText;
	public Text healthText;
	public static float timeElapsed = 0f;
	private float bestTime = 0f;
	private bool bestBestTime;

	public GameObject playerPrefab;
	private Ball ball;
	public static float points;
	private float bestPoints;


	public static bool gameStarted;
	private GameObject player;
	private TimeManager timeManager;
	private GameObject floor;
	private Spawner spawner;

	public Text title;

	public AudioClip[] clips;
	private AudioSource audio;

	void Awake(){
		audio = GetComponent<AudioSource> ();
//		resetBestScoreButton = GameObject.Find ("ResetBestScoreButton");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
		floor = GameObject.Find ("Background");
		ball = playerPrefab.GetComponent<Ball> ();
	}

	void Start () {

		spawner.active = false;
		Time.timeScale = 0;

		continueText.text = "Click To Start";
		title.text = "Skulls and Asteroids";

		bestTime = PlayerPrefs.GetFloat("BestTime");
		bestPoints = PlayerPrefs.GetFloat("BestPoints");
		audio.clip = clips [0];
		audio.Play ();
	}
	

	void Update () {
		if(!gameStarted && Time.timeScale == 0){
			if(Input.anyKeyDown){
				timeManager.ManipulateTime (1, 1f);
				ResetGame ();
			}
		}
		if(!gameStarted){
			blinkTime++;
			if(blinkTime% 40 == 0){
				blink = !blink;
			}
			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);

			var textColor = bestBestTime ? "#FF0" : "#FFF";


			scoreText.text = "Best: " + FormatTime (bestTime);
		} else {
			
			timeElapsed += Time.deltaTime;
			healthText.text = "Health: " + ball.health;
			scoreText.text = "Time: " + FormatTime (timeElapsed);
			if(ball.health <= 0) {
				OnPlayerKilled ();
			}
		}
	}

	void OnPlayerKilled(){
		spawner.active = false;

		audio.clip = clips [0];
		audio.Play ();
		timeManager.ManipulateTime (0, 5.5f);
		gameStarted = false;

		continueText.text = "Click To Start";
		title.canvasRenderer.SetAlpha (1);
//		resetBestScoreButton.SetActive (true);

		if(timeElapsed > bestTime){
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat ("BestTime", bestTime);
			bestBestTime = true;
		}
	}

	public void ResetBestScore(){
		PlayerPrefs.SetFloat ("BestTime", 0);
		PlayerPrefs.SetFloat ("BestPoints", 0);
		bestTime = PlayerPrefs.GetFloat("BestTime");
	}

	void ResetGame() {
		ball.health = 100;
		spawner.active = true;
		audio.clip = clips [1];
		audio.Play ();

//		resetBestScoreButton.SetActive (false);

//		spawner.ResetSpawner();

	//	player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0,0,0));



		gameStarted = true;

		continueText.canvasRenderer.SetAlpha (0);
		title.canvasRenderer.SetAlpha (0);
		points = 0;
		timeElapsed = 0;

		bestBestTime = false;
	}

	string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);

		return string.Format("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
	}

}
