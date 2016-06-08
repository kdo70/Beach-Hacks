using UnityEngine;
using System.Collections;

public class RandomVelocity : MonoBehaviour {
	Rigidbody2D rb2d;
	SpriteRenderer sr;
	public float minSpeed;
	public float maxSpeed;
	public float rotateSpeed = 10f;
	private float xSpeed;
	private float ySpeed;
	public float xOffset = 10f;
	public float yOffset = 7f;
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start () {

		xSpeed = Random.Range (-maxSpeed, maxSpeed);
		ySpeed = Random.Range (-maxSpeed, maxSpeed);
		transform.position = new Vector3 (xSpeed > 0 ?  -xOffset: xOffset, ySpeed > 0 ?  -yOffset: yOffset,0);
		rb2d.AddForce (new Vector2 (xSpeed > 0 ? xSpeed + minSpeed : xSpeed - minSpeed, ySpeed > 0 ? ySpeed + minSpeed : ySpeed - minSpeed));
		if(xSpeed > 0) {
			sr.flipX = true;
		}
	}
	

	void FixedUpdate () {
		if (xSpeed > 0) {
			rb2d.MoveRotation (rb2d.rotation - rotateSpeed * Time.fixedDeltaTime);
		} else {
			rb2d.MoveRotation (rb2d.rotation + rotateSpeed * Time.fixedDeltaTime);
		}
	}
		
}
