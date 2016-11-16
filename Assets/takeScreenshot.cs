// Saves screenshot as PNG file.
using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class takeScreenshot : MonoBehaviour {
	public Camera screenshotCamera;

	// Take a shot immediately
	void Start () {
		screenshotCamera = GetComponent<Camera> ();
//		yield return UploadPNG();
	}

	void Update(){
		if (AppManager.clickTouchpad) {
			Debug.Log ("++ssCamera");
			AppManager.clickTouchpad = false;
			StartCoroutine(UploadPNG ());

		}
			
	}
		

	IEnumerator UploadPNG() {
		// We should only read the screen buffer after rendering is complete
		Debug.Log ("Enable screenshot camera");
		screenshotCamera.enabled = true;
		Debug.Log ("Enable screenshot camera");
		yield return new WaitForEndOfFrame();




		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();

		// Encode texture into PNG
		byte[] bytes = tex.EncodeToPNG();
		UnityEngine.Object.Destroy(tex);

        string imgBase64 = Convert.ToBase64String(bytes);

		CloudAPITest temp = new CloudAPITest ();
		temp.runOCR (imgBase64);
	

		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		Debug.Log ("Saved screenshot");
		screenshotCamera.enabled = false;


	}

}
