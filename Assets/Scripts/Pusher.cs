using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
	PointEffector2D pe2d;
	SpriteRenderer sr;
	Rigidbody2D rb2d;
	public float force;
	public float rotateSpeed = 15f;
	private bool isPlayer
	// Use this for initialization
	void Awake () {
		pe2d = GetComponent<PointEffector2D>();
		sr = GetComponent<SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		rb2d.MoveRotation (rb2d.rotation + rotateSpeed * Time.fixedDeltaTime);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			
			if (true) {
				pe2d.forceMagnitude = force;
			}

			sr.enabled = true;
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = new Vector3(mousePosition.x,mousePosition.y,0);
		} else {
			pe2d.forceMagnitude = 0;
			sr.enabled = false;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		
	}
}
