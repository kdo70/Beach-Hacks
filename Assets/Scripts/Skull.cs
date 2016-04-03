using UnityEngine;
using System.Collections;

public class Skull : MonoBehaviour {
	private SpriteRenderer sr;
	public Sprite[] sprites;
	private Animator anim;
	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}




	void OnCollisionEnter2D(Collision2D col) {
		anim.enabled = false;
		sr.sprite = sprites [0];
	}
}
