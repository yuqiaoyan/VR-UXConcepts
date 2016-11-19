// Saves screenshot as PNG file.
using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class takeScreenshot : MonoBehaviour {
	private Camera screenshotCamera;
	public Camera head, eye;
	public GameObject rigT;

	// Take a shot immediately
	void Start () {
		screenshotCamera = GetComponent<Camera> ();
		screenshotCamera.enabled = false;
//		yield return UploadPNG();
	}

	void Update(){
		if (AppManager.clickTouchpad) {
			Debug.Log ("++ssCamera");
			AppManager.clickTouchpad = false;
			head.enabled = false;
			eye.enabled = false;
			//rigT.SetActive (false);
			StartCoroutine(UploadPNG ());

		}
			
	}
		

	IEnumerator UploadPNG() {
		// We should only read the screen buffer after rendering is complete
		Debug.Log ("Enable screenshot camera");
		screenshotCamera.enabled = true;

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

		if(GameObject.Find("OCR") !=null){
			CloudAPITest temp = GameObject.Find("OCR").GetComponent<CloudAPITest> ();
			temp.runOCR (imgBase64);
		}
			else{
				AppManager.debugText.text = "can't find OCR object";
			}
	

		// For testing purposes, also write to a file in the project folder
		System.IO.File.WriteAllText(Application.dataPath + "/../img64.txt",imgBase64);
		File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		Debug.Log ("Saved screenshot");
		screenshotCamera.enabled = false;
		head.enabled = true;
		eye.enabled = true;
		//rigT.SetActive (true);

	}

}
