using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	private Vector3 mousePosition;
	public float moveSpeed = 0.0f;
	private bool canMove = true;
	private Rigidbody2D rb2d;
	public Vector2 currentVelocity;
	private Rigidbody2D ball;
	private bool rotated = false;
	private float force = 250f;

	[SerializeField]
	private float sideX;
	[SerializeField]
	private float sideY;

	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		ball = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Rigidbody2D> ();
	}

	void Start () {
	}

	void FixedUpdate () {
		//velo = rb2d.velocity;

	}

	void Update () {
		if (Input.GetMouseButton(1) && canMove) {
			canMove = false;
			StartCoroutine (Rotate ());
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		


	}

	IEnumerator Rotate () {
		
		if (!rotated) {
			transform.eulerAngles = new Vector3 (0, 0, 90);
		} else {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		}
		rotated = !rotated;
		yield return new WaitForSeconds (.25f);
		canMove = true;
	}
}
