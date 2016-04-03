using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (Destroy (7));
	}
	
	IEnumerator Destroy(float time) {
		yield return new WaitForSeconds (time);
		OnOutOfBounds ();
	}

	public void OnOutOfBounds(){
		Destroy (gameObject);

	}
}
