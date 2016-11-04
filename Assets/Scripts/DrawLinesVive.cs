using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class DrawLinesVive : MonoBehaviour
{
	public Texture lineTex;
	public int maxPoints = 5000;
	float lineWidth = 4.0f;
	int minPixelMove = 5;
	bool useEndCap = false;
	Texture capLineTex;
	Texture capTex;
	float capLineWidth = 20.0f;
	float distanceFromCamera = 1.0f;
	public Vector3 controllerPos;

	private VectorLine line;
	private Vector3 previousPosition,previousPositionM;
	private int sqrMinPixelMove;
	private bool canDraw = false;
	bool line3d = false;
	bool VRInput=false;

	public bool triggerButtonDown = false;

//	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
//
//	private SteamVR_Controller.Device controller {
//
//		get { return SteamVR_Controller.Input((int)trackedObj.index);
//
//		}
//
//	}
//	private SteamVR_TrackedObject trackedObj;


	// Use this for initialization
	void Start ()
	{
//		trackedObj = GetComponent<SteamVR_TrackedObject>();

		if (line3d) {
			line = new VectorLine ("DrawnLine3D", new List<Vector3> (), capLineTex, capLineWidth, LineType.Continuous, Joins.Weld);

		} else {
			line = new VectorLine ("DrawnLine", new List<Vector2> (), capLineTex, capLineWidth, LineType.Continuous, Joins.Weld);
		}	

		line.endPointsUpdate = 1;
		sqrMinPixelMove = minPixelMove * minPixelMove;

	}
	
	// Update is called once per frame
	void Update ()
	{

		Vector2 newPointM = Input.mousePosition;
		int pointCount = 0;

		if(Input.GetMouseButtonDown(0)){
			if (line3d) {
				line.points3.Clear ();
			} else {
				line.points2.Clear ();
			}

			line.Draw ();

			previousPositionM = Input.mousePosition;

			if (line3d) {
				line.points3.Add (Input.mousePosition);
			} else {
				line.points2.Add (Input.mousePosition);
			}
			canDraw = true;

		}
		else if(Input.GetMouseButton(0) && (Input.mousePosition - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) {
			previousPosition = Input.mousePosition;
			if (line3d) {
				line.points3.Add (newPointM);
				pointCount = line.points3.Count;
				line.Draw3D();
			}
			else {
				line.points2.Add (newPointM);
				pointCount = line.points2.Count;
				line.Draw();
			}
			if (pointCount >= maxPoints) {
				canDraw = false;
			}
		}


//		if (controller == null) {
//
//			Debug.Log("Controller not initialized");
//
//			return;
//
//		}
//
//		//vive controller
//		if (VRInput) {
//			Vector2 newPoint = transform.position;
//
//			triggerButtonDown = controller.GetPressDown (triggerButton);
//			controllerPos = transform.position;
//
//			if (triggerButtonDown) {
//				Debug.Log ("Button Down. Start draw line");
//				line.points3.Clear ();
//				line.Draw ();
//				previousPosition = controllerPos;
//
//				line.points3.Add (newPoint);
//				canDraw = true;
//			
//			} else if (controller.GetPress (triggerButton)) {
//				Debug.Log ("Button held down");
//			} else if (controller.GetPress (triggerButton) && (controllerPos - previousPosition).sqrMagnitude > sqrMinPixelMove && canDraw) {
//				previousPosition = controllerPos;
//
//				line.points3.Add (newPoint);
//
//				pointCount = line.points3.Count;
//				line.Draw3D ();
//			}
//		}
//			

		if (pointCount >= maxPoints)
			canDraw = false;

	
	}

	Vector3 GetMousePos (){
		Vector3 p = Input.mousePosition;
		if (line3d) {
			p.z = distanceFromCamera;
			return Camera.main.ScreenToWorldPoint (p);
		}
		return p;
	}
}



