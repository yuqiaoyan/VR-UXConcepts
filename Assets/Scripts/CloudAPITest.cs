using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class CloudAPITest : MonoBehaviour {


	private string results;
    public TextMesh resultText;

	public WWW POST(string url, Dictionary<string,string> posthead, byte[] postData, System.Action onComplete) {



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
        JSONNode JSONResults = JSON.Parse(results);
        string searchResult = JSONResults["responses"][0]["textAnnotations"][0]["description"];
        resultText.text = "Searching for " + searchResult;
        Debug.Log(searchResult);
        Debug.Log(JSONResults["responses"][0].ToString());
        System.IO.File.WriteAllText(Application.dataPath + "/../cloudResult.txt", results);
    }


	// Use this for initialization
	public void runOCR (string imgBase64) {

        //string imgText = System.IO.File.ReadAllText("jsonTest.txt");
        //Debug.Log("this is imgText" + imgText);

        string OCRAPI = @"{""requests"":[ {""image"":{""content"":""";
        string JSONString = OCRAPI + imgBase64 + @"""},""features"":[ {""type"":""TEXT_DETECTION"", ""maxResults"":3}]}]}";



        string baseURL = "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyCw2MioNRybAhrLbZuNDw6JSjRgsQVpywo";

       
        Debug.Log(JSONString);

        byte[] JSONimg = System.Text.Encoding.ASCII.GetBytes(JSONString);

        Debug.Log("Posting JSONString to Google Cloud OCR API");
		WWWForm form = new WWWForm ();
		Dictionary<string,string> postHeader = form.headers;
		if (postHeader.ContainsKey ("Content-Type"))
			postHeader ["Content-Type"] = "application/json";
	    else
			postHeader.Add("Content-Type","application/json");

		POST (baseURL, postHeader, JSONimg, LogResults);

		
	
	}

}
