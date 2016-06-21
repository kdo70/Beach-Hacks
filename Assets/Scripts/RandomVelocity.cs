using UnityEngine;
using System.Collections;

public class RandomVelocity : MonoBehaviour {
	Rigidbody2D rb2d;
	SpriteRenderer sr;
	CameraScale cs;
	public float minSpeed;
	public float maxSpeed;
	public float rotateSpeed = 10f;
	private float xSpeed;
	private float ySpeed;
	private float xOffset;
	private float yOffset;
	[SerializeField]
	private int random;
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		cs = GameObject.Find ("Background").GetComponent<CameraScale> ();
	}

	void Start () {
		PlaceObjectAtPosition ();
	}

	void OnEnable () {
		random = Random.Range (0, 3);
		GetOffsets ();
		GetSpeeds ();
		PlaceObjectAtPosition ();
		rb2d.AddForce (new Vector2 (xSpeed > 0 ? xSpeed + minSpeed : xSpeed - minSpeed, ySpeed > 0 ? ySpeed + minSpeed : ySpeed - minSpeed));
		if(xSpeed > 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
	}

	void OnDisable () {
		rb2d.velocity = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}
	

	void FixedUpdate () {
		if (xSpeed > 0) {
			rb2d.MoveRotation (rb2d.rotation - rotateSpeed * Time.fixedDeltaTime);
		} else {
			rb2d.MoveRotation (rb2d.rotation + rotateSpeed * Time.fixedDeltaTime);
		}
	}

	void PlaceObjectAtPosition () {
		transform.position = new Vector3 (xSpeed > 0 ? -xOffset : xOffset, ySpeed > 0 ? -yOffset : yOffset, 0);

	}
		
	void GetOffsets () {
		if (random == 0) {
			xOffset = cs.GetWorldScreenWidth () / 2 * Random.Range (.6f, .9f);
			yOffset = cs.GetWorldScreenHeight () / 2;
		} else {
			xOffset = cs.GetWorldScreenWidth () / 2;
			yOffset = cs.GetWorldScreenHeight () / 2 * Random.Range (.1f, .9f);
		}
	}

	void GetSpeeds () {
		if (random == 0) {
			xSpeed = Random.Range (-maxSpeed, maxSpeed);
			ySpeed = Random.Range (-maxSpeed, maxSpeed);
		} else {
			xSpeed = Random.Range (-maxSpeed, maxSpeed);
			ySpeed = Random.Range (-maxSpeed, maxSpeed) / 5;
		}
	}
}
