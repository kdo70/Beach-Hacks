using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	Rigidbody2D ballVel;
	public float speed;
	[SerializeField]
	private float rotation;
	[SerializeField]
	float cosAngle;
	[SerializeField]
	Vector2 norm;
	[SerializeField]
	float reflectAngle;
	[SerializeField]
	Vector2 velocity;
	public float health = 100;
	public float minSpeed = 10;

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

	void Update () {
		velocity = ballVel.velocity;


	}

	void OnCollisionEnter2D(Collision2D col) {
		rotation = (col.transform.eulerAngles.z)*(Mathf.PI/180);
		norm = new Vector2(Mathf.Cos(rotation),Mathf.Sin(rotation));

//		Vector2 incidentVector = ballVel.velocity.normalized;
//		Vector2 realVector = incidentVector - 2 * norm * (Vector2.Dot (incidentVector, norm));

		//ballVel.velocity = ballVel.velocity.magnitude * Vector2.Reflect(ballVel.velocity.normalized,norm);
		ballVel.MovePosition (ballVel.position + ballVel.velocity.normalized*0.001f);

		if (Mathf.Abs(ballVel.velocity.x) > minSpeed || Mathf.Abs(ballVel.velocity.y) > minSpeed) {
			ballVel.velocity /= 1.25f;
		} 
		if(col.gameObject.tag == "Damaging Object") {
			StartCoroutine (Damage (5, .1f, .2f));
		}
	}



	IEnumerator Damage(int nTimes,float timeOn,float timeOff) {
		health -= 10;
		Physics2D.IgnoreLayerCollision (8, 11, true);
		while (nTimes > 0) {
			sr.enabled = true;
			yield return new WaitForSeconds (timeOn);
			sr.enabled = false;
			yield return new WaitForSeconds (timeOff);
			nTimes--;
		}
		Physics2D.IgnoreLayerCollision (8, 11, false);
		sr.enabled = true;
	}
}
