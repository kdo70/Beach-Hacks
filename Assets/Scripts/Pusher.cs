using UnityEngine;
using System.Collections;

public class Pusher : MonoBehaviour {
	PointEffector2D pe2d;
	public float force;
	// Use this for initialization
	void Awake () {
		pe2d = GetComponent<PointEffector2D>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			pe2d.forceMagnitude = force;
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = mousePosition;
		} else {
			pe2d.forceMagnitude = 0;
		}
	}
}
