using UnityEngine;
using System.Collections;

public class ControllerManagerCube : MonoBehaviour {
	public bool triggerButtonDown = false;
	public bool gripButtonDown = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip ;


	public Transform orb;

//	public Color c1 = Color.yellow;
//	public Color c2 = Color.red;
//	public int lengthOfLineRenderer = 1;
//
//	private LineRenderer lineRenderer;
//	public int lineIndex = 0;

	private Vector3 previousPosition = Vector3.zero;
	private float sqrMinMove = .001f;
	public float distanceBTWNPoint = 0f;
	//private bool canWrite = false;


	private SteamVR_Controller.Device controller {

		get { return SteamVR_Controller.Input((int)trackedObj.index);

		}

	}

	private SteamVR_TrackedObject trackedObj;

	void Start() {

		trackedObj = GetComponent<SteamVR_TrackedObject>();



	}

	void Update() {

		Vector3 newPoint = transform.position;
		Vector3 boardPoint = transform.position;
		boardPoint.z = -0.483f;

		if (controller == null) {

			Debug.Log("Controller not initialized");

			return;

		}

		triggerButtonDown = controller.GetPressDown(triggerButton);
		gripButtonDown = controller.GetPressDown (gripButton); 
		distanceBTWNPoint = (newPoint - previousPosition).sqrMagnitude;



		if (triggerButtonDown) {
			//lineRenderer.SetVertexCount(lengthOfLineRenderer);
			Debug.Log ("this is triggerButtonDown");
			//Debug.Log ("test1");
			Instantiate (orb, boardPoint, Quaternion.identity);

			
		}else if (controller.GetPress(triggerButton) && (distanceBTWNPoint > sqrMinMove)) {

			Debug.Log("Trigger button held");
			//Debug.Log ("test");



			Instantiate (orb, boardPoint, Quaternion.identity);
			previousPosition = newPoint;




		}








	}

	void OnCollisionEnter(Collision Col){
		Debug.Log ("controller hit board");
	}

}

//		if (gripButtonDown) {
//
//			Debug.Log("Grip button pressed");
//
//		}
//
//		//Check for Top Left TouchpadClick
//		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x < 0.0f && controller.GetAxis ().y > 0.0f) {
//			Debug.Log ("Touchpad top left " + controller.GetAxis ());
//		}
//
//		//Check for Bottom Left TouchpadClick
//		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x < 0.0f && controller.GetAxis ().y < 0.0f) {
//			Debug.Log ("Touchpad bottom left " + controller.GetAxis ());
//		}
//
//		//Check for Top Right TouchpadClick
//		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x > 0.0f && controller.GetAxis ().y > 0.0f) {
//			Debug.Log ("Touchpad top RIGHT " + controller.GetAxis ());
//		}
//
//		//Check for Bottom Right TouchpadClick
//		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && controller.GetAxis ().x > 0.0f && controller.GetAxis ().y < 0.0f) {
//			Debug.Log ("Touchpad bottom RIGHT " + controller.GetAxis ());
//		}

