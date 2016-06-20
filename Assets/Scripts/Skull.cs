using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite[] sprites;
	private Animator anim;

	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}


	void OnEnable () {
		anim.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D col) {
		anim.enabled = false;
		sr.sprite = sprites [0];
	}
}
