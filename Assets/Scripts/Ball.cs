using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	Rigidbody2D ballVel;

	private float health;
	private float minSpeed = 50;
	private SpriteRenderer sr;

	void Awake () {
		ballVel = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		StartBall ();
	}
	
	public void StartBall() {
		transform.position = new Vector3(0,0,0);
		ballVel.AddForce (new Vector2 (Random.Range(-minSpeed,minSpeed), Random.Range(-minSpeed,minSpeed)));
	}
		


	void OnCollisionEnter2D(Collision2D col) {

		ballVel.MovePosition (ballVel.position + ballVel.velocity.normalized*0.001f);

		if (IsBallSpeedGreaterThanMinSpeed ()) {
			ballVel.velocity /= 1.25f;
		} 
			
		if(col.gameObject.tag == "Damaging Object") {
			StartCoroutine (Damage (5, .1f, .2f));
		}
	}

	bool IsBallSpeedGreaterThanMinSpeed () {
		return Mathf.Abs (ballVel.velocity.x) > minSpeed || Mathf.Abs (ballVel.velocity.y) > minSpeed;
	}


	//Immunity delay
	IEnumerator Damage(int nTimes,float timeOn,float timeOff) {
		health -= 10;
		//Turn off physics collision for the ball
		Physics2D.IgnoreLayerCollision (8, 11, true);
		while (nTimes > 0) {
			sr.enabled = true;
			yield return new WaitForSeconds (timeOn);
			sr.enabled = false;
			yield return new WaitForSeconds (timeOff);
			nTimes--;
		}
		//Turn off physics collision for the ball
		Physics2D.IgnoreLayerCollision (8, 11, false);
		sr.enabled = true;
	}

	public float GetBallHealth () {
		return health;
	}

	public void ResetHealth () {
		health = 100;
	}
}
