﻿using UnityEngine;
using System.Collections;

public class arcLayout : MonoBehaviour {

    public int numItems = 5;
    public float radius = 4.5f;
    public int rotationDegrees = 5;
    public GameObject catalog;
    public Transform arcHelper;

    void insertToArc(Transform menuItem, float newYRotation, int positionIDX)
    {
        //add an item into our arc menu given the Rotation of where it should be on the arc
        float newX = 0;
        

        menuItem.SetParent(arcHelper);
        newX = menuItem.localPosition.x - (radius-positionIDX*.5f);
        menuItem.localPosition = new Vector3(newX, menuItem.localPosition.y, menuItem.localPosition.z);
        
        arcHelper.localEulerAngles = new Vector3(0, newYRotation, 0);
        //Debug.Log(newYRotation.ToString());
        //Debug.Log("euler angel of " + transform.name + " is " + transform.localEulerAngles.ToString());
        menuItem.SetParent(transform);
        arcHelper.localEulerAngles = new Vector3(0, 0, 0);

    }

    public void createArcMenu()
    {
        //NOTE - ONLY HANDLES ODD NUMBER OF OBJECTS RIGHT NOW

            //Debug.Log("number of children is " + catalog.transform.childCount.ToString());
            int numLoops = (catalog.transform.childCount - 1) / 2;

            //Debug.Log("number of loops is " + numLoops.ToString());
            int itemIDX = 0;
            float newYRotation = 0;

            
            
            for (int i = 1; i <= numLoops; i++)
            {

                //reset child to initial position
                catalog.transform.GetChild(itemIDX).localPosition = new Vector3(0, -.2f, 0);

                //rotate right
                newYRotation = i * rotationDegrees;
            
                insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,i);
                itemIDX += 1;

                //rotate left
                newYRotation = i * rotationDegrees * -1;
                catalog.transform.GetChild(itemIDX).localPosition = new Vector3(0, -.2f, 0);
                insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,i);
                itemIDX = 0;


            }

            //assume there are an odd number of menuItems. place the last object in the center     
            newYRotation = 0;
            catalog.transform.GetChild(itemIDX).localPosition = new Vector3(0, -.2f, 0);
            insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,0);

            transform.gameObject.SetActive(true);

            //all labels should be rotated 
            GameObject[] labels = GameObject.FindGameObjectsWithTag("itemLabel");

            for (int j = 0; j < labels.Length; j++)
            {
                labels[j].transform.localEulerAngles = new Vector3(0, -115, 0);
            }

            Debug.Log("-- CreateArcMenu");
    }

    public void hideMenu()
    {
        transform.gameObject.SetActive(false);

    }

	// Use this for initialization
	void Start () {
        createArcMenu();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
