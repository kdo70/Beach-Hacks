using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite[] sprites;

	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
	}

	void Start () {
		//Spawn an asteroid with a random color
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
	}
	


	void OnCollisionEnter2D(Collision2D col) {
		//when hit, the asteroid changes color
		sr.sprite = sprites [Random.Range (0, sprites.Length)];
	}
}
