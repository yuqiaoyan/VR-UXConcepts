using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {
	public bool triggerButtonDown = false;
	public bool gripButtonDown = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip ;

	public static ControllerManager Instance;

	void Awake(){
		Instance = this;
	}


	private SteamVR_Controller.Device controller {

		get { return SteamVR_Controller.Input((int)trackedObj.index);

		}

	}

	private SteamVR_TrackedObject trackedObj;

	void Start() {

		trackedObj = GetComponent<SteamVR_TrackedObject>();

	}

	void Update() {

		if (controller == null) {

			Debug.Log("Controller not initialized");

			return;

		}

		triggerButtonDown = controller.GetPressDown(triggerButton);
		gripButtonDown = controller.GetPressDown (gripButton); 


		if (triggerButtonDown) {

			Debug.Log("Trigger button pressed");

		}

		if (gripButtonDown) {

			Debug.Log("Grip button pressed");

		}

		//Check for Top Left TouchpadClick
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x < 0.0f && controller.GetAxis ().y > 0.0f) {
			Debug.Log ("Touchpad top left " + controller.GetAxis ());
		}

		//Check for Bottom Left TouchpadClick
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x < 0.0f && controller.GetAxis ().y < 0.0f) {
			Debug.Log ("Touchpad bottom left " + controller.GetAxis ());
		}

		//Check for Top Right TouchpadClick
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x > 0.0f && controller.GetAxis ().y > 0.0f) {
			Debug.Log ("Touchpad top RIGHT " + controller.GetAxis ());
		}

		//Check for Bottom Right TouchpadClick
		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x > 0.0f && controller.GetAxis ().y < 0.0f) {
			Debug.Log ("Touchpad bottom RIGHT " + controller.GetAxis ());
		}





	}

}
