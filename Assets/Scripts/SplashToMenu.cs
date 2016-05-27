using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashToMenu : MonoBehaviour {
	private GUITexture guiTexture;
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public float delay = 2.5f;
	private Color normalColor;


	void Awake () {
		guiTexture = GetComponent<GUITexture> ();

	}

	void Start() {
		// Set the texture so that it is the the size of the screen and covers it.
		guiTexture.pixelInset = new Rect(0f, 0f, 0f, 0f);
		//Gets the normal color of the guiTexture
		normalColor = guiTexture.color; 
		//Sets the texture color to be black(so it can fade to normal when the scene starts)
		guiTexture.color = Color.black;
	}


	void Update () {
		if (delay > 0) {
			FadeToFull ();
		}
		if (delay >= 0) {
			delay -= Time.deltaTime;
		}
		if (delay <= 0) {
			EndScene ();
		}
	}


	void FadeToFull () {
		// Lerp the colour of the texture between itself and the normal state
		guiTexture.color = Color.Lerp(guiTexture.color, normalColor, fadeSpeed * Time.deltaTime);
	}


	void FadeToBlack () {
		// Lerp the colour of the texture between itself and black.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
		

	void EndScene () {
		// Fade the texture to clear.
		FadeToBlack();

		// If the texture is almost clear...
		if(guiTexture.color.a >= 0.99f) {
			// ... change the scene
			SceneManager.LoadScene(1);


		}
	}



}
