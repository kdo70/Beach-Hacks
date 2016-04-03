using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite[] sprites;
	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
	}
	


	void OnCollisionEnter2D(Collision2D col) {
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
	}
}
