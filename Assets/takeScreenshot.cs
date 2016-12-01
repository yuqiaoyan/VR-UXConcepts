// Saves screenshot as PNG file.
using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class takeScreenshot : MonoBehaviour {
	private Camera screenshotCamera;
	public Camera head, eye;
	public GameObject rigT, leftControl,rightControl;

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
            leftControl.SetActive(false);
            rightControl.SetActive(false);
			StartCoroutine(UploadPNG ());

		}
			
	}
		

	IEnumerator UploadPNG() {
        // We should only read the screen buffer after rendering is complete
        Debug.Log ("Enable screenshot camera");
        DateTime enableCamTime = System.DateTime.Now;
        screenshotCamera.enabled = true;

        Debug.Log(enableCamTime.ToLongTimeString() + ":" + enableCamTime.Millisecond.ToString());
		yield return new WaitForEndOfFrame();

        Debug.Log("Rendering is complete");
        DateTime renderDoneTime = System.DateTime.Now;
        Debug.Log(renderDoneTime.ToLongTimeString() + ":" + renderDoneTime.Millisecond.ToString());

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();

        Debug.Log("Turn vive camera on");
        DateTime viveOffTime = System.DateTime.Now;
        Debug.Log(viveOffTime.ToLongTimeString() + ":" + viveOffTime.Millisecond.ToString());
        screenshotCamera.enabled = false;
        head.enabled = true;
        eye.enabled = true;
        //rigT.SetActive (true);
        leftControl.SetActive(true);
        rightControl.SetActive(true);

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
		UnityEngine.Object.Destroy(tex);

        string imgBase64 = Convert.ToBase64String(bytes);

        

        if (GameObject.Find("OCR") !=null){
			CloudAPITest temp = GameObject.Find("OCR").GetComponent<CloudAPITest> ();
			temp.runOCR (imgBase64);
		}
			else{
				AppManager.debugText.text = "can't find OCR object";
			}
	

		// For testing purposes, also write to a file in the project folder
		//System.IO.File.WriteAllText(Application.dataPath + "/../img64.txt",imgBase64);
		//File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		

    }

}
