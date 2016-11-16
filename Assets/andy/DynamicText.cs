using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class DynamicText : MonoBehaviour {

	private static int TIMER_DURATION_SECONDS = 15;
	private static int CLICKS_TO_DEFUSE = 10;

	public TextMesh textTimer;
	public GameObject explosion;
	public GameObject explosionSphere;
	private DateTime startTime;
	private int clickCount = 0;
	private bool defused = false;

	// Use this for initialization
	void Start () {
		startTime = DateTime.Now;
	}
	
	// Update is called once per frame
	void Update () {
		int elapsedSeconds = (int)(DateTime.Now - startTime).TotalSeconds;
		int remainingSeconds = TIMER_DURATION_SECONDS - elapsedSeconds;

		if (ControllerManager.Instance.triggerButtonDown) {
			if (++clickCount >= CLICKS_TO_DEFUSE) {
				if (remainingSeconds > 0) {
					textTimer.text = "Bomb defused!\nYou win!\n\nScore: " + (remainingSeconds * 1000);
					defused = true;
				}
			}
		}

		if (!defused) {
			if (remainingSeconds <= 0) {
				textTimer.text = "BOOM!";
				textTimer.color = Color.white;
				explosion.active = true;
				if (remainingSeconds > -1) {
					explosionSphere.transform.localScale += new Vector3 (0.03F, 0.03F, 0.03F);
				}
			} else {
				string defuseProgress = "";
				for (int i = 0; i < clickCount; i++) {
					defuseProgress += ".";
				}
				textTimer.text = "Defuse the BOMB.\n\nYou have\n" + remainingSeconds +
					" seconds\nto comply.\n\n" + defuseProgress;
				if (remainingSeconds <= 3) {
					textTimer.color = Color.red;
				} else if (remainingSeconds <= 5) {
					textTimer.color = Color.yellow;
				}
			}
		}
	}
}
