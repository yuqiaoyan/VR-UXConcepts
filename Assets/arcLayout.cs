using UnityEngine;
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
        Debug.Log(newYRotation.ToString());
        //Debug.Log("euler angel of " + transform.name + " is " + transform.localEulerAngles.ToString());
        menuItem.SetParent(transform);
        arcHelper.localEulerAngles = new Vector3(0, 0, 0);

    }

    void createArcMenu()
    {
        //NOTE - ONLY HANDLES ODD NUMBER OF OBJECTS RIGHT NOW
        if (numItems <= catalog.transform.childCount)
        {
            //Debug.Log("number of children is " + catalog.transform.childCount.ToString());
            int numLoops = (numItems - 1) / 2;

            //Debug.Log("number of loops is " + numLoops.ToString());
            int itemIDX = 0;
            float newYRotation = 0;


            for (int i = 1; i <= numLoops; i++)
            {
                //rotate right
                newYRotation = i * rotationDegrees;
                insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,i);
                itemIDX += 1;

                //rotate left
                newYRotation = i * rotationDegrees * -1;
                insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,i);
                itemIDX = 0;


            }

            //assume there are an odd number of menuItems. place the last object in the center     
            newYRotation = 0;
            insertToArc(catalog.transform.GetChild(itemIDX), newYRotation,0);

        }
        else
        {
            Debug.Log("There are not enough items in the catalog.");
        }

        Debug.Log("-- CreateArcMenu");
    }

    public void hideMenu()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
        createArcMenu();
        //simpleSearch test = new simpleSearch();
        //test.showResults();
        //hideMenu();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
