using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
	//Force Effect
	PointEffector2D pe2d;
	SpriteRenderer sr;
	Rigidbody2D rb2d;
	public float force;
	public float rotateSpeed = 15f;
	private bool isPlayer;

	void Awake () {
		pe2d = GetComponent<PointEffector2D>();
		sr = GetComponent<SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();

	}

	void FixedUpdate () {
		//Creates the spinning effect on vortex
		rb2d.MoveRotation (rb2d.rotation + rotateSpeed * Time.fixedDeltaTime);
	}


	void Update () {
		#if UNITY_IOS || UNITY_ANDROID 
		if (Input.touchCount > 0) {

			MakeTheForceOnlyAffectBall ();
			//Spawn the vortex
			sr.enabled = true;

			MoveVortexToPosition (Input.GetTouch(0).position);
		} else {
			pe2d.forceMagnitude = 0;
			sr.enabled = false;
		}


		#else
		if (Input.GetMouseButton(0)) {

			MakeTheVortexOnlyAffectBall ();

			sr.enabled = true;
			MoveVortexToPosition (Input.mousePosition);
		} else {
			pe2d.forceMagnitude = 0;
			sr.enabled = false;
		}


		#endif

	}

	void MoveVortexToPosition (Vector3 controlPosition) {
		Vector3 newControlPosition = Camera.main.ScreenToWorldPoint (controlPosition);
		transform.position = new Vector3(newControlPosition.x, newControlPosition.y,0);
	}

	void MakeTheVortexOnlyAffectBall () {
		if (isPlayer) {
			pe2d.forceMagnitude = force;
		} else {
			pe2d.forceMagnitude = 0;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		//tests if the object in side the vortex is the ball
		if(col.gameObject.tag == "Ball") {
			isPlayer = true;
		} else {
			isPlayer = false;
		}
	}

}
