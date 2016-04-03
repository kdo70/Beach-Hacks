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
	Vector2 currentVelocity;
	void Awake () {
		ballVel = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		transform.position = new Vector3(0,0,0);
		ballVel.AddForce (new Vector2 (Random.Range(-speed,speed), Random.Range(-speed,speed)));
		//ballVel.AddForce (new Vector2 (-500, 0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentVelocity = ballVel.velocity;


	}

	void OnCollisionEnter2D(Collision2D col) {
		rotation = (col.transform.eulerAngles.z)*(Mathf.PI/180);
		norm = new Vector2(Mathf.Cos(rotation),Mathf.Sin(rotation));

//		Vector2 incidentVector = ballVel.velocity.normalized;
//		Vector2 realVector = incidentVector - 2 * norm * (Vector2.Dot (incidentVector, norm));

		//ballVel.velocity = ballVel.velocity.magnitude * Vector2.Reflect(ballVel.velocity.normalized,norm);
		ballVel.MovePosition (ballVel.position + ballVel.velocity.normalized*0.001f);
	}

}
