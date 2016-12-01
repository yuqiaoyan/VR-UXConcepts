﻿using UnityEngine;
using System.Collections;

public class eraser : MonoBehaviour {

    private bool triggerButtonDown = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private SteamVR_TrackedObject trackedObj;

    public GameObject writingCollection;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }

    void clearWriting()
    /* Clears all writing on the whiteboard */
    {
        //get all children from the writing collection
        int numDots = writingCollection.transform.childCount;

        for(int i = 0; i < numDots; i++)
        {
            
            Transform aDot = writingCollection.transform.GetChild(i);
            aDot.GetComponent<Renderer> ().enabled = false;
            Destroy(aDot.gameObject);

        }

        //disable the render component
        //destroy all the cube objects

    }
	
	// Update is called once per frame
	void Update () {

        if(controller == null) {

            Debug.Log("Controller not initialized");

            return;

        }

        triggerButtonDown = controller.GetPressDown(triggerButton);

        if (triggerButtonDown)
        {
            AppManager.debugText.text = "clear whiteboard";
            clearWriting();
        }

    }
}