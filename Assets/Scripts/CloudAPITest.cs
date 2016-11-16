using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;

public class CloudAPITest : MonoBehaviour {


	private string results;

	public WWW POST(string url, Dictionary<string,string> posthead, byte[] postData, System.Action onComplete) {
		WWWForm form = new WWWForm();


		WWW www = new WWW(url, postData, posthead);

		StartCoroutine(WaitForRequest(www, onComplete));
		return www;
	}

	private IEnumerator WaitForRequest(WWW www, System.Action onComplete) {
		yield return www;
		// check for errors
		if (www.error == null) {
			results = www.text;
			onComplete();
		} else {
			Debug.Log (www.error);
		}
	}

	private void LogResults(){
		Debug.Log ("Success");
		Debug.Log (results);
	}


	// Use this for initialization
	public void runOCR (string imgBase64) {

		string imgText = System.IO.File.ReadAllText("jsonTest.txt");
		Debug.Log("this is imgText" + imgText);

		string baseURL = "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyCw2MioNRybAhrLbZuNDw6JSjRgsQVpywo";

		string JSONString = imgBase64;

		byte[] JSONimg = System.Text.Encoding.ASCII.GetBytes(JSONString);
			
		WWWForm form = new WWWForm ();
		Dictionary<string,string> postHeader = form.headers;
		if (postHeader.ContainsKey ("Content-Type"))
			postHeader ["Content-Type"] = "application/json";
		else
			postHeader.Add("Content-Type","application/json");

		POST (baseURL, postHeader, JSONimg, LogResults);

		
	
	}

}
