using UnityEngine;
using System.Collections;

public class CameraScale : MonoBehaviour {
 	SpriteRenderer sr;
	[SerializeField]
	float worldScreenHeight;
	[SerializeField]
	float worldScreenWidth;
	void Awake() {
		sr = GetComponent<SpriteRenderer>();
		worldScreenHeight = Camera.main.orthographicSize * 2;
		worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

		transform.localScale = new Vector3 (worldScreenWidth / sr.sprite.bounds.size.x, worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}

	public float GetWorldScreenHeight () {
		return worldScreenHeight;
	}

	public float GetWorldScreenWidth () {
		return worldScreenWidth;
	}
}
