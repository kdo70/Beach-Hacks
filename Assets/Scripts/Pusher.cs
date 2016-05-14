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

			if (isPlayer) {
				pe2d.forceMagnitude = force;
			} else {
				pe2d.forceMagnitude = 0;
			}
			//Spawn the vortex
			sr.enabled = true;
			//Grabs the position where the tap occurs
			Vector3 touchPosition = Input.GetTouch(0).position;
			touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
			//Sets the vortex position to the tap position
			transform.position = new Vector3(touchPosition.x,touchPosition.y,0);
		} else {
			pe2d.forceMagnitude = 0;
			sr.enabled = false;
		}


		#else
		if (Input.GetMouseButton(0)) {

			if (isPlayer) {
				pe2d.forceMagnitude = force;
			} else {
				pe2d.forceMagnitude = 0;
			}

			sr.enabled = true;
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3(mousePosition.x,mousePosition.y,0);
		} else {
			pe2d.forceMagnitude = 0;
			sr.enabled = false;
		}


		#endif

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
