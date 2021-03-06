using UnityEngine;
using System.Collections;

public class ControllerManagerCube : MonoBehaviour {
	private bool triggerButtonDown = false;
	//private bool gripButtonDown = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	//private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip ;

	private SteamVR_Controller.Device controller {
		get { return SteamVR_Controller.Input((int)trackedObj.index);}
	}

	private SteamVR_TrackedObject trackedObj;

	public GameObject dot;
    public GameObject dotShadow;
	public float distanceBTWNPoint = 0f;
    public GameObject writingCollection;
    public Renderer wbOutline;

	private Vector3 previousPosition = Vector3.zero;
	private float sqrMinMove = .0001f;
	private bool canDraw = false;
	private GameObject debugT;
    private Vector3 fwd;
    public SteamVR_LaserPointer lp;
 


    void Awake(){
		debugT = GameObject.Find ("debugText");
	}

	void Start() {

		if (debugT != null) {
			AppManager.debugText = debugT.GetComponent<TextMesh> ();
			AppManager.debugText.text = "Debug Text";
		}

		trackedObj = GetComponent<SteamVR_TrackedObject>();

        lp.PointerIn += new PointerEventHandler(pointerInListener);
        lp.PointerOut += new PointerEventHandler(pointerOutListener);
    }


	void Update() {

		if (controller == null) {

			Debug.Log("Controller not initialized");

			return;

		}

		Vector3 newPoint = transform.position;
		Vector3 boardPoint = transform.position;
		boardPoint.x = -.93f;

		triggerButtonDown = controller.GetPressDown(triggerButton);
		//gripButtonDown = controller.GetPressDown (gripButton); 
		distanceBTWNPoint = (newPoint - previousPosition).sqrMagnitude;

		if (canDraw) {


            if (triggerButtonDown || (controller.GetPress(triggerButton) && (distanceBTWNPoint > sqrMinMove))) { 

                //Debug.Log("TriggerButton held");

                GameObject cube = Instantiate<GameObject> (dot);
                cube.transform.position = boardPoint;
                cube.transform.SetParent(writingCollection.transform);
				previousPosition = newPoint;             

			}
            else 
            {
                dotShadow.GetComponent<Renderer>().enabled = true;
                dotShadow.transform.localPosition = boardPoint;

            }


        }
        else
        {
            dotShadow.GetComponent<Renderer>().enabled = false;
            
        }


		if (controller.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
			AppManager.clickTouchpad = true;
			Debug.Log ("Click Touchpad");
		}
        
    }

    void pointerInListener(object sender, PointerEventArgs e)
    {
        AppManager.debugText.text = "target is: " + e.target.name;
        if(e.target.tag == "noPointer")
        {
            //do nothing
            lp.active = false;
        }
        else if (e.target.childCount > 0)
        {
            e.target.GetChild(0).GetComponent<Light>().enabled = true;
        }

    }

    void pointerOutListener(object sender, PointerEventArgs e)
    {
        AppManager.debugText.text = "OUT is: " + e.target.name;
        if (e.target.tag == "noPointer")
        {
            lp.active = true;
            //do nothing
        }
        else if (e.target.childCount > 0)
        {
            e.target.GetChild(0).GetComponent<Light>().enabled = false;
        }

    }


    void OnTriggerEnter(Collider Col){
		AppManager.debugText.text="hit board";
		canDraw = true;
        lp.active = false;
        lp.thickness = 0;
        wbOutline.enabled = true;
    }

	void OnTriggerExit(Collider col){
		AppManager.debugText.text = "away from board";
		canDraw = false;
        lp.active = true;
        lp.thickness = 0.01f;
        wbOutline.enabled = false;
    }

}
	
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

