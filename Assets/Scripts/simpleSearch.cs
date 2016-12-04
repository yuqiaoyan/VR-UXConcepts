using System;
using UnityEngine;
using System.Collections;

public class simpleSearch : MonoBehaviour {

    // Use this for initialization
    //public GameObject arcLayout;
    bool isGo = true;
    public float spacingDistance = .75f;
    public float resultX = -1f, resultY = .4f;
    public Transform searchResultsEmpty;
    private Transform[] searchResultsArray;
    private int resultsNum;
    public TextMesh debugT;

    public arcLayout currMenu;

    public Transform[] Find(string query)
    {
        int resultsIDX = 0;

        //clean query
        query = query.Replace("\t", String.Empty);
        query = query.Replace("\n", String.Empty);

        for (int i = 0;i < transform.childCount; i++)
        {
            Transform item = transform.GetChild(i);
            

            String itemNameString = item.name.ToUpper();
            
            bool isMatch = itemNameString.Contains(query.ToUpper());

            //Debug.Log("name is " + itemNameString);
            //Debug.Log("query is " + query);
            //Debug.Log("isMatch returned " + isMatch.ToString());

            if (isMatch)
            {
                Debug.Log("Found " + item.name);
                searchResultsArray[resultsIDX] = item;
                resultsIDX += 1;
                
            }
        }

        if(resultsIDX == 0)
        {
            Debug.Log("results array " + searchResultsArray.ToString());
            Debug.Log("No matches");
            debugT.text = "No Matches Found";

        }
        else
        {
            resultsNum = resultsIDX;
            showResults(searchResultsArray);
            return searchResultsArray;
        }

        
        
        return null;
    
    }

    void setResult(Transform aResult, float resultZ)
    {
        aResult.localPosition = new Vector3(resultX, resultY, resultZ);
        aResult.SetParent(searchResultsEmpty);

    }

    public void showResults(Transform[] results)
    {    
        int resultIDX = 0;



        //assumes even number of results
        spacingDistance = spacingDistance / 2; //test case for small number of items
        
        for (int i = 1; i <= (resultsNum / 2); i++)
        {
            setResult(results[resultIDX], spacingDistance * i);
            resultIDX += 1;

            setResult(results[resultIDX], spacingDistance * i *-1);
            resultIDX +=1;

        }

        currMenu.hideMenu();

    }

    void Start () {
        searchResultsArray = new Transform[20];


    }
	
	// Update is called once per frame
	void Update () {
        //if (transform.childCount > 4 && isGo)
        //{
        //    showResults();
        //    isGo = false;
        //    currMenu.hideMenu();
            
            
        //}

    }
}
