using UnityEngine;
using System.Collections;

public class simpleSearch : MonoBehaviour {

    // Use this for initialization
    //public GameObject arcLayout;
    bool isGo = true;
    public float spacingDistance = .75f;
    public float resultX = -1f, resultY = .4f;
    public Transform searchResults;

    public arcLayout currMenu;

    Transform Find(string query)
    {
        query = query.ToUpper();

        for(int i = 0;i < transform.childCount; i++)
        {
            Transform item = transform.GetChild(i);
            //Debug.Log("name is " + item.name);

            item.name = item.name.ToUpper();

            if (item.name.Contains(query))
            {
                Debug.Log("Found " + item.name);
                return item;
            }
        }

        Debug.Log("No matches");
        return null;
    
    }

    void setResult(Transform aResult, float resultZ)
    {
        aResult.localPosition = new Vector3(resultX, resultY, resultZ);
        aResult.SetParent(searchResults);

    }

    public void showResults()
    {    
        int resultIDX = 1;



        //assumes even number of results
        spacingDistance = spacingDistance / 2; //test case for small number of items
        
        //assume arcHelper is at 0th IDX
        for (int i = 1; i < 2; i++)
        {
            setResult(transform.GetChild(resultIDX), spacingDistance * i);
            resultIDX += 1;

            setResult(transform.GetChild(resultIDX), spacingDistance * i *-1);
            resultIDX = 1;

        }

    }

    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount > 4 && isGo)
        {
            showResults();
            isGo = false;
            currMenu.hideMenu();
            
            
        }

    }
}
