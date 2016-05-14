using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	Rigidbody2D ballVel;

	public float health = 100;
	private float minSpeed = 50;
	private SpriteRenderer sr;
	private CircleCollider2D cc2d;

	void Awake () {
		ballVel = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		cc2d = GetComponent<CircleCollider2D> ();
	}

	void Start () {
		transform.position = new Vector3(0,0,0);
		ballVel.AddForce (new Vector2 (Random.Range(-minSpeed,minSpeed), Random.Range(-minSpeed,minSpeed)));
	}
	
	public void StartBall() {
		transform.position = new Vector3(0,0,0);
		ballVel.AddForce (new Vector2 (Random.Range(-minSpeed,minSpeed), Random.Range(-minSpeed,minSpeed)));
	}
		


	void OnCollisionEnter2D(Collision2D col) {
		//Bouncing collision
		float rotation;
		float cosAngle;
		Vector2 norm;
		float reflectAngle;
		rotation = (col.transform.eulerAngles.z)*(Mathf.PI/180);
		norm = new Vector2(Mathf.Cos(rotation),Mathf.Sin(rotation));

		ballVel.MovePosition (ballVel.position + ballVel.velocity.normalized*0.001f);

		//Slows the speed down to the minspeed
		if (Mathf.Abs(ballVel.velocity.x) > minSpeed || Mathf.Abs(ballVel.velocity.y) > minSpeed) {
			ballVel.velocity /= 1.25f;
		} 

		//when the ball hits a asteroid or skull
		if(col.gameObject.tag == "Damaging Object") {
			StartCoroutine (Damage (5, .1f, .2f));
		}
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
}
